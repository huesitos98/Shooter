using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidad")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Salto y gravedad")]
    [SerializeField] private float jumpHeigt = 1.2f;
    [SerializeField] private float gravity = -9.8f;

    [SerializeField]private CharacterController controller;
    private Vector2 moveInput;
    [SerializeField] private bool jumpRequest;
    [SerializeField] private bool sprintHeld;

    [SerializeField] private float velocity;

    public event Action OnSalto;
    public float MoveX => moveInput.x;
    public float MoveY => moveInput.y;
    public bool estaCorriendo => sprintHeld;


    private void Awake()
    {
        //controller = GetComponent<CharacterController>();
    }




    // Update is called once per frame
    void Update()
    {
        ReadInput();
        ApplyMovement();
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
            Debug.Log("JumpRequestTrue");
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
        if (controller.isGrounded)
        {
            Debug.Log("isGrounded" + controller.isGrounded);

            velocity = -2f;

            if (jumpRequest)
            {
                velocity = Mathf.Sqrt(-2 * jumpHeigt * gravity);
                OnSalto?.Invoke();
            }

        }
        else
        {
            velocity += gravity * Time.deltaTime;
        }
        jumpRequest = false;
        Vector3 finalMove = moveDirection * currentspeed;
        finalMove.y = velocity;
        controller.Move(finalMove * Time.deltaTime);    
    
    
    
    
    }


}



