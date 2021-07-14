using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private CharacterController character_controller;

    public float sprint_speed = 10f;
    public float move_speed = 5f;
    public float crouch_speed = 2f;

    private Transform look_root;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;
    private bool is_crouching;

    private PlayerFootsteps player_footsteps;
    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_volume_min = 0.2f;
    private float walk_volume_max = 0.6f;

    private float walk_step_distance = 0.4f;
    private float sprint_step_distance = 0.25f;
    private float crouch_step_distance = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        character_controller = GetComponentInParent<CharacterController>();
        look_root = transform.GetChild(0);

        player_footsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    void Start()
    {
        player_footsteps.volume_max = walk_volume_max;
        player_footsteps.volume_min = walk_volume_min;
        player_footsteps.step_distance = walk_step_distance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(character_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
            {
                playerMovement.speed = sprint_speed;

                player_footsteps.step_distance = sprint_step_distance;
                player_footsteps.volume_min = sprint_volume;
                player_footsteps.volume_max = sprint_volume;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
            {
                playerMovement.speed = move_speed;

                player_footsteps.step_distance = walk_step_distance;
                player_footsteps.volume_min = walk_volume_min;
                player_footsteps.volume_max = walk_volume_max;
            }
        }
    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C) && character_controller.isGrounded)
        {
            if(is_crouching)
            {
                look_root.localPosition = new Vector3(0f, stand_height, 0f);
                playerMovement.speed = move_speed;

                player_footsteps.step_distance = walk_step_distance;
                player_footsteps.volume_min = walk_volume_min;
                player_footsteps.volume_max = walk_volume_max;

                is_crouching = false;
            } else
            {
                look_root.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;

                player_footsteps.step_distance = crouch_step_distance;
                player_footsteps.volume_min = crouch_volume;
                player_footsteps.volume_max = crouch_volume;

                is_crouching = true;

            }
        }
    }
}
