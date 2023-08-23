using UnityEngine;

public class CenterBoxOnCamera : MonoBehaviour
{
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
        CenterObject();
    }

    private void CenterObject()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 objectPosition = transform.position;

        Vector3 newPosition = new Vector3(cameraPosition.x, objectPosition.y) + new Vector3(-3.25f, 0f);
        transform.position = newPosition;
    }
}