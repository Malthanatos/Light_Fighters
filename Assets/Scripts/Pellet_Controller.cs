using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Laser_Pellet_Controller.cs:
Has References to: Game_Manager, Laser_Controller,
Default pellet is enemy pellet, auto deletes iteself on collision
Governs the local behavior of given pellets
Beams are pellets that are rotating spring shapes
When the laser pellets hit something they do something corresponding to the type of collision, then self destruct
Signals Laser_Controller when two colored pellets collide
Deletes when it collides with an uncolored pellet
*/

public class Pellet_Controller : MonoBehaviour
{
    private Game_Manager GM;
    private Pellet_Shooter_Controller LC;
    
    public float speed;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.position +=(-1*transform.forward*speed*Time.deltaTime);
    }

    private void FixedUpdate()
    {

    } 
}
