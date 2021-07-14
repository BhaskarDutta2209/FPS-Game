using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_unlock = true;

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private int smooth_steps = 10;

    [SerializeField]
    private float smooth_weight = 0.4f;

    [SerializeField]
    private float roll_angle = 10f;

    [SerializeField]
    private float roll_speed = 3f;

    [SerializeField]
    private Vector2 default_look_limits = new Vector2(-70f, 80f);

    private Vector2 look_angles;
    private Vector2 current_mouse_look;
    private Vector2 smooth_move;
    //private float current_roll_angle;
    private int last_look_frame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        current_mouse_look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        look_angles.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f);
        look_angles.y += current_mouse_look.y * sensitivity;

        look_angles.x = Mathf.Clamp(look_angles.x, default_look_limits.x, default_look_limits.y);

        //current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_angle, Time.deltaTime * roll_speed);
        // GetAxisRaw is like GetAxis but it will go from 0 to 1 directly

        lookRoot.localRotation = Quaternion.Euler(look_angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_angles.y, 0f);

    }
}
