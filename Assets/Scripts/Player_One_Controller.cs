using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_One_Controller : MonoBehaviour
{
    public bool move;
    public bool aim;

    public float rotation_speed;
    public float movement_speed;

    Vector3 aim_vector;
    Quaternion target_rotation;

    Vector3 move_vector;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Move1X") < -0.1f || Input.GetAxis("Move1X") > 0.1f || 
            Input.GetAxis("Move1Y") < -0.1f || Input.GetAxis("Move1Y") > 0.1f)
        {
            move_vector = new Vector3(Input.GetAxisRaw("Move1Y"), 0.0f, Input.GetAxisRaw("Move1X"));
            move = true;
        }

        if (Input.GetAxis("Aim1X") < -0.1f || Input.GetAxis("Aim1X") > 0.1f ||
            Input.GetAxis("Aim1Y") < -0.1f || Input.GetAxis("Aim1Y") > 0.1f)
        {
            aim_vector = new Vector3(-Input.GetAxisRaw("Aim1Y"), 0.0f, -Input.GetAxisRaw("Aim1X"));
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
