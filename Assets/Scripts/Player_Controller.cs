using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    public bool p1_move;

    float p1_horizontal;
    float p1_vertical;

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
            p1_horizontal = Input.GetAxisRaw("Move1X");
            p1_vertical = Input.GetAxisRaw("Move1Y");
            p1_move = true;
        }
	}

   void FixedUpdate()
    {
        if (p1_move == true)
        {
            p1.GetComponent<Transform>().position += new Vector3(p1_vertical, 0.0f, p1_horizontal) * 0.5f;
            p1_move = false;
        }
    } 
}
