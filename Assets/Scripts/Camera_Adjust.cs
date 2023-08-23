using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Adjust : MonoBehaviour
{
   /*public Camera mainCamera;
   
       void Start()
       {
           // Check if the current platform is a mobile device
           if (Application.isMobilePlatform)
           {
               AdjustCameraForMobile();
           }
       }
   
       void AdjustCameraForMobile()
       {
           // Get the screen width and height
           float screenWidth = Screen.width;
           float screenHeight = Screen.height;
   
           // Calculate the aspect ratio
           float targetAspectRatio = 9f / 16f; // You can adjust this ratio based on your game's requirements
           float currentAspectRatio = screenWidth / screenHeight;
   
           // Calculate the difference between the target and current aspect ratios
           float ratioDifference = targetAspectRatio / currentAspectRatio;
   
           // Adjust the camera's viewport rect based on the difference in aspect ratios
           Rect cameraRect = mainCamera.rect;
           cameraRect.width = Mathf.Clamp01(cameraRect.width * ratioDifference);
           cameraRect.height = Mathf.Clamp01(cameraRect.height * ratioDifference);
           cameraRect.x = (1f - cameraRect.width) / 2f;
           cameraRect.y = (1f - cameraRect.height) / 2f;
           mainCamera.rect = cameraRect;
       }*/
   
   
   
   /*public float targetScreenWidth = 1080f; // The target screen width you want to design for

   void Start()
   {
       // Get the current screen size
       float currentScreenWidth = Screen.width;

       // Calculate the desired orthographic size
       float targetOrthoSize = GetComponent<Camera>().orthographicSize * (targetScreenWidth / currentScreenWidth);

       // Adjust the orthographic size of the camera
       GetComponent<Camera>().orthographicSize = targetOrthoSize;
   }*/
   
   /*[SerializeField] private float targetOrthographicSize = 5f; // The desired orthographic size for the camera
   [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, -10f); // The desired position for the camera
   [SerializeField] private Quaternion targetRotation = Quaternion.identity; // The desired rotation for the camera

   private void Awake()
   {
       // Set the orthographic size, position, and rotation of the camera
       Camera.main.orthographicSize = targetOrthographicSize;
       Camera.main.transform.position = targetPosition;
       Camera.main.transform.rotation = targetRotation;
   }*/
   
   
   /*public float targetScreenWidth = 1080f; // The target screen width you want to design for
   public float targetScreenHeight = 1920f; // The target screen height you want to design for

   void Start()
   {
       // Calculate the aspect ratio of the target screen size
       float targetAspectRatio = targetScreenWidth / targetScreenHeight;

       // Get the current screen size
       float currentAspectRatio = (float)Screen.width / Screen.height;

       // Calculate the desired orthographic size
       float targetOrthoSize = GetComponent<Camera>().orthographicSize * (targetAspectRatio / currentAspectRatio);

       // Adjust the orthographic size of the camera
       GetComponent<Camera>().orthographicSize = targetOrthoSize;

       // Calculate the position offset based on the difference in screen aspect ratios
       float positionOffset = (targetOrthoSize - GetComponent<Camera>().orthographicSize) * GetComponent<Camera>().orthographicSize;

       // Adjust the position of the camera
       transform.position = new Vector3(transform.position.x, transform.position.y - positionOffset, transform.position.z);
   }*/
   
   
   
   
   
   
}
