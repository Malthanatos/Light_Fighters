using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Two_Controller : MonoBehaviour
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
        if (Input.GetAxis("Move2X") < -0.1f || Input.GetAxis("Move2X") > 0.1f ||
            Input.GetAxis("Move2Y") < -0.1f || Input.GetAxis("Move2Y") > 0.1f)
        {
            move_vector = new Vector3(Input.GetAxisRaw("Move2Y"), 0.0f, Input.GetAxisRaw("Move2X"));
            move = true;
        }

        if (Input.GetAxis("Aim2X") < -0.1f || Input.GetAxis("Aim2X") > 0.1f ||
            Input.GetAxis("Aim2Y") < -0.1f || Input.GetAxis("Aim2Y") > 0.1f)
        {
            aim_vector = new Vector3(-Input.GetAxisRaw("Aim2Y"), 0.0f, -Input.GetAxisRaw("Aim2X"));
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
