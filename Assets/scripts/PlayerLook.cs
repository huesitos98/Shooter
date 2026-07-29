using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform cameraTransform;
    
    [Header("Sensibilidad")]
    [SerializeField] private float sensitivityX = 2f;
    [SerializeField] private float sensitivityY = 2f;

    [Header("Limites")]
    [SerializeField] private float maxX = 85f;
    [SerializeField] private float minY= -85f;

    private float currentRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  
    if(cameraTransform == null)
    {
            Debug.LogError("Aun no se asigno la camara");
        }
    
    }


    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
   if (mouse == null || cameraTransform == null)
        {
            return;
        }
        Vector2 mouseDelta = mouse.delta.ReadValue();

        float mouseX = mouseDelta.x * sensitivityX;
        float mouseY = mouseDelta.y * sensitivityY;

        transform.Rotate(Vector3.up, mouseX);
        currentRotation -= mouseY;
        currentRotation = Mathf.Clamp(currentRotation, minY, maxX);
        cameraTransform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
    }
}
