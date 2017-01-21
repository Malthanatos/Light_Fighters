using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Enemy_Generator.cs: (basically a level controller)
Has References to: Game_Manager, 
Generates Asteroids and Enemies
A primary function takes stage number as input from Game_Manager, then starts spawning, otherwise idle
Generates different combinations of enemies in pseudo-random configurations depending on the stage
Generally, asteroids will spawn randomly(outside of the map) if they are allowed in a stage, except first stage
Each stage can allow or disallow asteroids impacting level win condition
Stage 1: Asteroids only, random colors and configuration(asteroids are hard coded here)
Stage 2: Scout swarms from the top, 3 random primary color swarms
Stage 3: More scout swarms from the top, extra enemies from the bottom at the end(7), last is random secondary color
Stage 4: Enemies swarm from all sides randomly, 3 primary swarms, 3 secondary swarms afterwards
Stage 5: Introduces a single turrent centered, random primary color
Stage 6:
...
Boss Stage: Three turrents in opposing positions(secondary colors), after defeated, generate random fighters(6 waves, 3 primary, 3 secondary),
		concentric rings around centered boss, outer ring is split into secondary colors, inner ring is random primary colors
*/

public class Enemy_Generator : MonoBehaviour
{
    public Game_Manager GM;

    public GameObject Fighter;

	void Start ()
    {
		
	}

    public void Activate_Stage(int stage)
    {
        Stage1();
    }
	
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
            
    }

    public void Stage1()
    {
        GameObject fighter = (GameObject)Instantiate(Fighter, new Vector3(25.0f, 0.0f, 25.0f), Quaternion.identity);
        //Enemy_Controller EC = fighter.AddComponent<Enemy_Controller>();
        //FighterClass f = new FighterClass();
    }

    public void Stage2()
    {

    }
}
