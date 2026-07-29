using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidad")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Salto y gravedad")]
    [SerializeField] private float jumpHeigt = 1.2f;
    [SerializeField] private float gravity = 9.8f;

    private CharacterController controller;
    private Vector2 moveInput;
    private bool jumpRequest;
    private bool sprintHeld;

    private float velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }




    // Update is called once per frame
    void Update()
    {
        
    }
    private void ReadInput()
    {
        Keyboard keyboard = Keyboard.current;
        float x = 0f;
        float z = 0f;
if (keyboard.wKey.isPressed)
        {
            z += 1f;
        }
   if (keyboard.sKey.isPressed)
        { z -= 1f; }
   if (keyboard.dKey.isPressed)
        { x += 1f;}
   if (keyboard.aKey.isPressed)
        { x -= 1f; }
        moveInput = new Vector2(x, z);
        sprintHeld = keyboard.leftShiftKey.isPressed;
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpRequest = true;
        }






}
    private void ApplyMovement()
    {
        Vector3 moveDirection = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        float currentspeed = walkSpeed;
        if (sprintHeld == true)
        {
            currentspeed = sprintSpeed;
        }
    
    }


}



