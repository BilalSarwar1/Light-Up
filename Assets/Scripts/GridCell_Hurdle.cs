using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell_Hurdle : MonoBehaviour
{
    private int PosX;
    private int PosY;
    public GameObject Object;

    public bool isOccupied = false;
    public void SetPosition(int x, int y)
    {
        PosX = x;
        PosY = y;
    }
    public void SetPositionX(int x)
    {
        PosX = x;
    }
    public void SetPositionY(int y)
    {
        PosY = y;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(PosX, PosY);

    }    
    
    public int GetPositionX()
    {
        return PosX;

    }    
    
    public int GetPositionY()
    {
        return PosY;

    }

    public void SetGameObject(GameObject obj)
    {
        Object = obj;
        if(Object != null)
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }

    }
    
    public GameObject GetGameObject()
    {
        return Object;
    }
}
