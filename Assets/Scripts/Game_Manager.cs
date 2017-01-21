using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Game_Manager.cs:
Has References to: Laser_Controller, Enemy_Generator
Governs different stages through states, include debug state setter in start menu
Governs Start and pause menu behvaior
Starts/resumes game when all or two players fire at the start icon
Scrolls background while game is running
Signals Enemy_Generator if no objects are tagged enemy (asteroids are included)
Send signal to Enemy_Generator when game starts/round ends with stage number
*/

public class Game_Manager : MonoBehaviour
{
<<<<<<< HEAD
    private Pellet_Shooter_Controller LC;
=======
    public Laser_Controller LC;
    public Enemy_Generator EG;
>>>>>>> origin/master

    public bool DEBUG;

	void Start ()
    {
        EG.Activate_Stage(1);
	}
	
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {

    } 
}
