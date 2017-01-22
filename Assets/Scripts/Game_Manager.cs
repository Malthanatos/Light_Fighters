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
    public Enemy_Generator EG;

    public bool DEBUG;

    public float time_till_enemy_check = 10.0f;

	void Start ()
    {
        EG.Activate_Stage(1);
	}
	
	void Update ()
    {
        time_till_enemy_check -= Time.deltaTime;

        if(time_till_enemy_check >= 0)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }

	}

    private void FixedUpdate()
    {

    } 
}
