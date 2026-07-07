using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonRotation : MonoBehaviour
{
    [Header("MovementSettings")]
    [SerializeField] float angularSpeed = 360f;


    [Header("PlayerActions")]
    [SerializeField] InputActionReference rotateLeft;
    [SerializeField] InputActionReference rotateRight;


    private Rigidbody2D rd2D;

    private void Awake()
    {
        this.rd2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rotateLeft.action.Enable();
        rotateRight.action.Enable();

        //rotateLeft.action.started += OnRotateLeft;
        //rotateLeft.action.performed += OnRotateLeft;
        //rotateRight.action.started += OnRotateRight;

    }


    private void OnRotateLeft(InputAction.CallbackContext context)
    {
        this.rd2D.angularVelocity = this.angularSpeed;
    }

    private void OnRotateRight(InputAction.CallbackContext context)
    {
        this.rd2D.angularVelocity = -this.angularSpeed;
    }

    private void Update()
    {
        if (rotateLeft.action.IsPressed())
        {
            this.rd2D.angularVelocity = this.angularSpeed;
        }
        else if (rotateRight.action.IsPressed())
        {
            this.rd2D.angularVelocity = -this.angularSpeed;
        }
        else
        {
            this.rd2D.angularVelocity = 0f;
        }

        //if (Keyboard.current.aKey.isPressed)
        //{
        //    this.rd2D.angularVelocity = this.angularSpeed;
        //}
        //else if (Keyboard.current.dKey.isPressed)
        //{
        //    this.rd2D.angularVelocity = -this.angularSpeed;
        //}
        //else
        //{
        //    this.rd2D.angularVelocity = 0f;
        //}
    }

    private void OnDisable()
    {
        rotateLeft.action.Disable();
        rotateRight.action.Disable();

        //rotateLeft.action.started -= OnRotateLeft;
        //rotateLeft.action.performed -= OnRotateLeft;
        //rotateRight.action.started -= OnRotateRight;
    }
}
