using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    List<GameObject> grid_lines;
    [SerializeField] GameObject h_line;
    [SerializeField] GameObject v_line;



    // Start is called before the first frame update
    void Start()
    {
        DrawVerticalLines();
    }

    void DrawVerticalLines()
    {
        for(float i=-10.5f; i<=10.5f; i++)
        {
            GameObject line = Instantiate(v_line);
            line.transform.position = new Vector3(i, 0, 0);
        }
        for (float i = -10.5f; i <= 10.5f; i++)
        {
            GameObject line = Instantiate(h_line);
            line.transform.position = new Vector3(0, i, 0);
        }
    }
}
