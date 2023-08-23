using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundSizer : MonoBehaviour
{
    public Camera mainCamera;
    public SpriteRenderer backgroundSpriteRenderer;

    private void Start()
    {
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (backgroundSpriteRenderer == null)
        {
            backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        FitBackground();
    }

    private void FitBackground()
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spriteHeight = backgroundSpriteRenderer.sprite.bounds.size.y;
        float spriteWidth = backgroundSpriteRenderer.sprite.bounds.size.x;

        float scaleX = cameraWidth / spriteWidth;
        float scaleY = cameraHeight / spriteHeight;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}