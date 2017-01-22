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

    public GameObject Asteroid;
    public GameObject Scout;
    public GameObject Fighter;
    public GameObject Turrent;
    public GameObject Missile;
    public GameObject Boss;

    public float s_width = 320.0f;//Screen.width;
    public float s_height = 100.0f;//Screen.height;

    public int stage;

    public float timer;
    public float stage_2_delay = 5.0f;
    public int stage_2_asteroids = 12;

    void Start()
    {

    }

    public void Activate_Stage(int start_stage)
    {
        if (start_stage == 1)
        {
            Debug.Log("Actvating Stage 1");
            stage = 1;
        }
        else if (start_stage == 2)
        {
            Debug.Log("Actvating Stage 2");
            stage = 2;
            timer = stage_2_delay + Time.fixedTime;
        }
        else
        {
            Debug.Log("Wrong stage number");
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (stage == 1)
        {
            GameObject fighter = (GameObject)Instantiate(Fighter, new Vector3(25.0f, 0.0f, 25.0f), Quaternion.identity);
            //Enemy_Controller EC = fighter.AddComponent<Enemy_Controller>();
            //FighterClass f = new FighterClass();
        }
        if (stage == 2)
        {
            //asteroids
            if (timer <= Time.fixedTime)
            {
                Debug.Log("Placing Asteroid");
                GameObject asteroid = (GameObject)Instantiate(Asteroid, generate_random_direction(direction.any), Quaternion.identity);
                Enemy_Controller EC = asteroid.GetComponent<Enemy_Controller>();
                EC.movement_speed = 0.5f;
                --stage_2_asteroids;
                timer = stage_2_delay + Time.fixedTime;
            }
            if (stage_2_asteroids == 0)
            {
                stage = 0;
            }
        }
    }

    public enum direction { up, down, left, right, horizontal, vertical, any, onscreen };
    public Vector3 generate_random_direction(direction d)
    {
        //Note: these names are out of whack due to camera changes
        //Values outside of the screen within half of the screen size
        float left = Random.Range(s_height, s_height + (s_height / 2));
        float right = Random.Range(-s_height - (s_height / 2), s_height);
        float up = Random.Range(s_width, s_width + (s_width / 2));
        float down = Random.Range(-s_width - (s_width / 2), s_width);
        /*Debug.Log("Left: " + left);
        Debug.Log("Right: " + right);
        Debug.Log("Up: " + up);
        Debug.Log("Down: " + down);*/
        //Values outside of the screen, random between up/down, left/right
        float x_out = Random.value > 0.5f ? left : right;
        float z_out = Random.value > 0.5f ? up : down;
        //Values somewhere within the screen bounds
        float z_in = Random.Range(-s_width, s_width);
        float x_in = Random.Range(-s_height, s_height);

        if (d == direction.any)
        {
            return new Vector3(x_out, 0.0f, z_out);
        }
        else if (d == direction.onscreen)
        {
            return new Vector3(x_in, 0.0f, z_in);
        }
        else if (d == direction.up)
        {
            return new Vector3(x_in, 0.0f, up);
        }
        else if (d == direction.down)
        {
            return new Vector3(x_in, 0.0f, down);
        }
        else if (d == direction.left)
        {
            return new Vector3(left, 0.0f, z_in);
        }
        else if (d == direction.right)
        {
            return new Vector3(right, 0.0f, z_in);
        }
        else if (d == direction.horizontal)
        {
            return new Vector3(x_out, 0.0f, z_in);
        }
        else if (d == direction.vertical)
        {
            return new Vector3(x_in, 0.0f, z_out);
        }
        else
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
