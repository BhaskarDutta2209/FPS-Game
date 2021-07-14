using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController character_controller;
    private Vector3 move_direction;
    private float gravity = 20f;
    private float vertical_velocity;

    public float speed = 5f;
    public float jump_force = 10f;

    private void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;

        ApplyGravity();

        character_controller.Move(move_direction);
    }

    void ApplyGravity()
    {
        vertical_velocity -= gravity * Time.deltaTime;

        PlayerJump();

        move_direction.y = vertical_velocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_velocity = jump_force;
        }
    }
}
