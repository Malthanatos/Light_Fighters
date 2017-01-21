using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Laser_Controller.cs: (Laser Generators are dummy objects that only have position and rotation, act as references)
Has References to: Game_Manager,
Red, Green, Blue; Yellow, Cyan, Magenta; White
Single script controlls all 7 laser generators, which are instantiated at startup
The primary laser generators are all childeren of the corresponding players, and thus follow them around at all times, rotation follows left stick
Beam pellets are generated one at a time and are fired in the direction that the generator is pointing
When signaled that two primary colors have collided, it places and activates a secondary color
The same will occur when a secondary color collides with a primary color, except with white
Any one secondary color will place and activate white when it intersects with any primary color
Secondary and white laser generators will always shoot exactly 1 pellet per collision
*/

public class Pellet_Shooter_Controller : MonoBehaviour
{
    private Game_Manager GM;

    public GameObject pellet;

    public float pellet_forward_force;

	void Start ()
    {
		
	}
	
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject temporary_pellet_handler;
                temporary_pellet_handler = Instantiate(pellet, transform.position, transform.rotation) as GameObject;

                //Despawn pellet after 3 seconds if not already destroyed
                Destroy(temporary_pellet_handler, 3.0f);
            }	
	}

    private void FixedUpdate()
    {

    }
}
