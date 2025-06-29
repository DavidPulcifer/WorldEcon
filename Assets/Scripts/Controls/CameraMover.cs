using Unity.Mathematics;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] float groundSpeed = 10.0f;
    [SerializeField] float rotationSpeed = 20.0f;
    [SerializeField] float verticalSpeed = 10.0f;
    [SerializeField] float minVertical = 0f;
    [SerializeField] float maxVertical = 100f;
    [SerializeField] float viewSpeed = 10.0f;
    [SerializeField] float viewDirection = -1f;
    [SerializeField] float baseCameraRotationX = 45;
    [SerializeField] float maxCameraRotationX = 90;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float forwardTranslation = Input.GetAxis("Vertical") * groundSpeed * Time.deltaTime;
        float sideTranslation = Input.GetAxis("Horizontal") * groundSpeed * Time.deltaTime;
        float viewTranslation = Input.mouseScrollDelta.y * viewSpeed * viewDirection * Time.deltaTime;

        float rotationDirection;
        float verticalDirection;

        if (Input.GetKey(KeyCode.Q)) rotationDirection = -1f;
        else if (Input.GetKey(KeyCode.E)) rotationDirection = 1f;
        else rotationDirection = 0f;

        if (Input.GetKey(KeyCode.Z) && transform.position.y > minVertical) verticalDirection = -1f;
        else if (Input.GetKey(KeyCode.C) && transform.position.y < maxVertical) verticalDirection = 1f;
        else verticalDirection = 0f;

        float rotationTranslation = rotationDirection * rotationSpeed * Time.deltaTime;
        float verticalTranslation = verticalDirection * verticalSpeed * Time.deltaTime;

        transform.Translate(sideTranslation, verticalTranslation, forwardTranslation);
        transform.Rotate(0, rotationTranslation, 0);

        mainCamera.transform.Rotate(viewTranslation, 0, 0);
        Vector3 cameraRotation = mainCamera.transform.eulerAngles;
        float cameraRotationX = cameraRotation.x;        
        cameraRotationX = Mathf.Clamp(cameraRotationX, baseCameraRotationX, maxCameraRotationX);
        mainCamera.transform.eulerAngles = new Vector3(cameraRotationX, cameraRotation.y, cameraRotation.z);        
    }
}
