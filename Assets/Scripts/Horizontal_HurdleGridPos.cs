using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_HurdleGridPos : MonoBehaviour
{
    public GridManager gridManager;

    private void Awake()
    {
        transform.localScale = gridManager.hurdleSize;
    }
}
