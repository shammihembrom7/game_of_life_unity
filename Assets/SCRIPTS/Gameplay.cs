using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using GameOfLife;
using Vector2 = System.Numerics.Vector2;

public class Gameplay : MonoBehaviour
{
    public static Gameplay instance;

    CellEvaluations cell_evaluation_methods;

    List<int> current_cell_coords;
    List<Cell> alive_cells;
    List<Cell> dead_cells;

    [SerializeField] GameObject alive_light;
    List<GameObject> lights_list = new List<GameObject>();

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        camera = Camera.main;

        cell_evaluation_methods = new CellEvaluations();
        current_cell_coords = new List<int>();
        alive_cells = new List<Cell>();
        dead_cells = new List<Cell>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            UnityEngine.Vector2 mouseCLickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UnityEngine.Vector2 point = Vector2Int.RoundToInt(mouseCLickPoint);

            if(point.y > -6)  ProcessClick(new Vector2(point.x,point.y));
        }
    }

    void ProcessClick(Vector2 point)
    {
        bool point_exist = false;
        foreach(Cell cell in alive_cells)
        {
            if (cell.coords == point)
            {
                alive_cells.Remove(cell);
                point_exist = true;
                break;
            }
        }

        if (!point_exist) cell_evaluation_methods.AddCorrespondingAliveCell(point, alive_cells);
        ProcessLights();
    }

    public void RunSimulation()
    {
        cell_evaluation_methods.EvaluateNextGenCells(alive_cells, dead_cells);
        cell_evaluation_methods.AssignNextGenCells(alive_cells, dead_cells);

        ProcessLights();
    }

    public void ResetCells()
    {
        alive_cells.Clear();
        dead_cells.Clear();

        ProcessLights();
    }

    void ProcessLights()
    {
        foreach (GameObject obj in lights_list) Destroy(obj);
        lights_list.Clear();

        foreach(Cell cell in alive_cells)
        {
            GameObject light = Instantiate(alive_light);
            light.transform.position = new UnityEngine.Vector2(cell.coords.X, cell.coords.Y);
            lights_list.Add(light);
        }
    }
}
