using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    public int height;
    [SerializeField]
    public int width;

    [SerializeField]
    private float gridSpace = 2f;

    public Vector3 GridSize;
    
    [SerializeField]
    private GameObject gridCellPrefab;

    public GameObject[,] gameGrid;
    public GameObject[,] hurdleGrid;
    public GameObject[,] VerticalhurdleGrid;

    public Vector2 hurdleSize;
    
    
    public GameObject HurdleGridCell;
    public GameObject Hurdle_GridManager;
    public GameObject VerticalHurdle_GridManager;

    public float AdjustValue;

    public float offset;

    public GameObject VerticalHurdleGridCell;
    
    // Start is called before the first frame update
    void Awake()
    {
        AdjustValue = gridSpace / 2;
        CreateObjectGrid();

        CreateHurdleGrid();
    
    }
    

    void CreateObjectGrid()
    {
        gameGrid = new GameObject[height, width];


        for(int row = 0; row < height; row++)
        {
            for(int column = 0; column < width; column++)
            {
                gameGrid[row, column] = Instantiate(gridCellPrefab, new Vector3(column * gridSpace, row * gridSpace), Quaternion.identity);
                gameGrid[row, column].gameObject.GetComponent<GridCell>().SetPosition(row, column);



                gameGrid[row, column].transform.parent = transform;
                gameGrid[row,column].gameObject.name = "Grid Space: (Row: "+row.ToString() + " , Column: "+column.ToString() +" )";

                gameGrid[row, column].transform.localScale = GridSize;

            }
        }

        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                if(row==0 || row == height - 1)
                {
                    //Debug.Log("Hello");

                    gameGrid[row, column].gameObject.GetComponent<GridCell>().boundary = true;
                }
                else
                {
                    if(column == 0 || column == width - 1)
                    {
                        gameGrid[row, column].gameObject.GetComponent<GridCell>().boundary = true;
                    }
                }


            }
        }
    }

    void CreateHurdleGrid()
    {
        hurdleGrid = new GameObject[height, width];
        VerticalhurdleGrid = new GameObject[height, width];
        
        
        for(int row = 0; row < height; row++)
        {
            for(int column = 0; column < width -1; column++)
            {
                hurdleGrid[row, column] = Instantiate(HurdleGridCell, new Vector3((column) * offset + AdjustValue, row * offset), Quaternion.identity);
                hurdleGrid[row, column].gameObject.GetComponent<GridCell_Hurdle>().SetPosition(row, column);



                hurdleGrid[row, column].transform.parent = Hurdle_GridManager.transform;
                hurdleGrid[row,column].gameObject.name = "Hurdle: (Row: "+row.ToString() + " , Column: "+column.ToString() +" )";

                hurdleGrid[row, column].gameObject.transform.localScale = new Vector3(1,1);

            }
        }
        
        for(int column = 0; column < width; column++)
        {
            for(int row = 0; row < height-1; row++)
            {
                VerticalhurdleGrid[row, column] = Instantiate(VerticalHurdleGridCell, new Vector3(column * offset, (row) * offset + AdjustValue), Quaternion.identity);
                VerticalhurdleGrid[row, column].gameObject.GetComponent<GridCell_Hurdle>().SetPosition(row, column);



                
                VerticalhurdleGrid[row, column].transform.parent = VerticalHurdle_GridManager.transform;
                //VerticalhurdleGrid[row, column].transform.Rotate(0, 0, 90);
                VerticalhurdleGrid[row,column].gameObject.name = "Vertical Hurdle: (Row: "+row.ToString() + " , Column: "+column.ToString() +" )";

                VerticalhurdleGrid[row, column].gameObject.transform.localScale = new Vector3(1, 1);

            }
        }
        
    }
    
}
