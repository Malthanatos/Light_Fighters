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
    public GameObject Swarm;
    public GameObject Fighter;
    public GameObject Turrent;
    public GameObject Missile;
    public GameObject Boss;

    public float s_width = 320.0f;//Screen.width;
    public float s_height = 140.0f;//Screen.height;

    public int stage;

    public float timer;

    public float stage_1_delay = 5.0f;
    public int stage_1_asteroids = 12;

    public float stage_2_delay = 5.0f;
    public int stage_2_scouts = 12;

    public float stage_3_delay = 5.0f;
    public int stage_3_fighters = 12;

    public int stage_5_scouts = 21;

    void Start()
    {

    }

    public void Activate_Stage(int start_stage)
    {
        if (start_stage == 1)
        {
            Debug.Log("Actvating Stage 1");
            stage = 1;
            timer = stage_1_delay + Time.fixedTime;
        }
        else if (start_stage == 2)
        {
            Debug.Log("Actvating Stage 2");
            stage = 2;
            timer = stage_2_delay + Time.fixedTime;
        }
        if (start_stage == 3)
        {
            Debug.Log("Actvating Stage 3");
            stage = 3;
        }
        else if (start_stage == 4)
        {
            Debug.Log("Actvating Stage 4");
            stage = 4;
        }
        else if (start_stage == 5)
        {
            Debug.Log("Actvating Stage 5");
            stage = 5;
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
            //asteroids
            if (timer <= Time.fixedTime)
            {
                Debug.Log("Placing Asteroid");
                GameObject asteroid = (GameObject)Instantiate(Asteroid, generate_random_direction(direction.any), Quaternion.identity);
                Enemy_Controller EC = asteroid.GetComponent<Enemy_Controller>();
                EC.movement_speed = 0.5f;
                --stage_1_asteroids;
                timer = stage_1_delay + Time.fixedTime;
            }
            if (stage_1_asteroids == 0)
            {
                stage = 0;
            }
        }
        if (stage == 2)
        {
            //scouts
            if (timer <= Time.fixedTime)
            {
                Debug.Log("Placing Scout");
                GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.any), Quaternion.identity);
                --stage_2_scouts;
                timer = stage_2_delay + Time.fixedTime;
            }
            if (stage_2_scouts == 0)
            {
                stage = 0;
            }
        }
        if (stage == 3)
        {
            //fighters
            if (timer <= Time.fixedTime)
            {
                Debug.Log("Placing Fighter");
                GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.any), Quaternion.identity);
                --stage_3_fighters;
                timer = stage_3_delay + Time.fixedTime;
            }
            if (stage_3_fighters == 0)
            {
                stage = 0;
            }
        }
        if (stage == 4)
        {
            //turrents
             for (int i = 0; i < 3; ++i)
            {
                Debug.Log("Placing turrent");
                GameObject turrent = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
            }
            stage = 0;
        }
        if (stage == 5)
        {
            //scout swarm
            Debug.Log("Placing swarm");
            GameObject swarm = (GameObject)Instantiate(Swarm, generate_random_direction(direction.any), Quaternion.identity);
            stage = 0;
        }
    }

    public enum direction { up, down, left, right, horizontal, vertical, any, onscreen };
    public Vector3 generate_random_direction(direction d)
    {
        //Values outside of the screen within half of the screen size
        /*float left = Random.Range(s_height, s_height + (s_height / 2));
        float right = Random.Range(-s_height - (s_height / 2), s_height);
        float up = Random.Range(s_width, s_width + (s_width / 2));
        float down = Random.Range(-s_width - (s_width / 2), s_width);*/
        float x_right = Random.Range(200,240);
        float x_left = Random.Range(-240,-200);
        float z_down = Random.Range(88,105);
        float z_up = Random.Range(-105,-88);
        //Values outside of the screen, random between up/down, left/right
        float x_out = Random.value > 0.5f ? x_left : x_right;
        float z_out = Random.value > 0.5f ? z_up : z_down;
        //Values somewhere within the screen bounds
        float x_in = Random.Range(-s_width/2, s_width/2);
        float z_in = Random.Range(-s_height/2, s_height/2);

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
            return new Vector3(x_in, 0.0f, z_up);
        }
        else if (d == direction.down)
        {
            return new Vector3(x_in, 0.0f, z_down);
        }
        else if (d == direction.left)
        {
            return new Vector3(x_left, 0.0f, z_in);
        }
        else if (d == direction.right)
        {
            return new Vector3(x_right, 0.0f, z_in);
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
