using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical_HurdleGridPos : MonoBehaviour
{
    public GridManager gridManager;

    private void Awake()
    {
        transform.localScale = new Vector3(gridManager.hurdleSize.y, gridManager.hurdleSize.x);
    }
}
