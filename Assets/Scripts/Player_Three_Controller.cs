using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Three_Controller : MonoBehaviour
{
    public bool move;
    public bool aim;

    public float rotation_speed;
    public float movement_speed;

    Vector3 aim_vector;
    Quaternion target_rotation;

    Vector3 move_vector;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Move3X") < -0.1f || Input.GetAxis("Move3X") > 0.1f ||
            Input.GetAxis("Move3Y") < -0.1f || Input.GetAxis("Move3Y") > 0.1f)
        {
            move_vector = new Vector3(Input.GetAxisRaw("Move3Y"), 0.0f, Input.GetAxisRaw("Move3X"));
            move = true;
        }

        if (Input.GetAxis("Aim3X") < -0.1f || Input.GetAxis("Aim3X") > 0.1f ||
            Input.GetAxis("Aim3Y") < -0.1f || Input.GetAxis("Aim3Y") > 0.1f)
        {
            aim_vector = new Vector3(-Input.GetAxisRaw("Aim3Y"), 0.0f, -Input.GetAxisRaw("Aim3X"));
            target_rotation = Quaternion.LookRotation(aim_vector, transform.up);
            aim = true;
        }
    }

    void FixedUpdate()
    {
        if (move == true)
        {
            transform.position += move_vector * movement_speed;
            move = false;
        }

        if (aim == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, rotation_speed);
            aim = false;
        }
    }
}
