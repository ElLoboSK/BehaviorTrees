using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Internal references
    private CharacterController characterController;
    public TextMeshPro text;

    //Settings
    public float walkSpeed;
    public float jumpHeight;
    public float rotationSpeed;

    //Variables
    private Vector2 moveHorizontalDirection;
    private float moveVerticalDirection;
    private Vector2 lookHorizontalDirection;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(moveHorizontalDirection.x, 0f, moveHorizontalDirection.y);
        Vector3 lookDirection = new Vector3(lookHorizontalDirection.x, 0f, lookHorizontalDirection.y);

        Vector3 movement = new Vector3(moveDirection.x * walkSpeed, moveVerticalDirection, moveDirection.z * walkSpeed);

        characterController.Move(movement * Time.deltaTime);

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void setMessage(String message)
    {
        text.text = message;
    }

    public void setMoveHorizontalDirection(Vector2 horizontalDirection)
    {
        this.moveHorizontalDirection = horizontalDirection;
    }

    public void setMoveVerticalDirection(float verticalDirection)
    {
        this.moveVerticalDirection = verticalDirection;
    }

    public void setLookHorizontalDirection(Vector2 lookHorizontalDirection)
    {
        this.lookHorizontalDirection = lookHorizontalDirection;
    }
}
