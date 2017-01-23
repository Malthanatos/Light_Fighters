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

    public float s_width = 360.0f;//Screen.width;
    public float s_height = 130.0f;//Screen.height;

    public int stage;

    public float timer;

    public float rand;

    public float stage_1_delay = 2.0f;
    public int stage_1_asteroid_groups = 3;

    public float stage_2_delay = 5.0f;
    public int stage_2_scout_groups = 3;

    public float stage_3_delay = 10.0f;
    public int stage_3_scout_groups = 6;

    public float stage_7_delay = 20.0f;
    public bool stage_7_run = false;

    public Material[] small;
    public Material[] medium;
    public Material[] large;

    void Start()
    {

    }

    public void Activate_Stage(int start_stage)
    {
        if (start_stage == 0)
        {
            Debug.Log("Game is now in standby...");
        }
        else if (start_stage == -1)
        {
            Debug.Log("Actvating Debug Stage 1");
            stage = -1;
            timer = stage_1_delay + Time.fixedTime;
        }
        else if (start_stage == 1)
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
        else if (start_stage == 3)
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
        else if (start_stage == 6)
        {
            Debug.Log("Actvating Stage 6");
            stage = 6;
        }
        else if (start_stage == 7)
        {
            Debug.Log("Actvating Stage 7");
            stage = 7;
            timer = stage_7_delay + Time.fixedTime;
            stage_7_run = false;
        }
        else if (start_stage == 8)
        {
            Debug.Log("Actvating Stage 8");
            stage = 8;
        }
        else if (start_stage == 9)
        {
            Debug.Log("Actvating Stage 9");
            stage = 9;
        }
        else if (start_stage == 10)
        {
            Debug.Log("Actvating Stage 10");
            stage = 10;
        }
        else
        {
            Debug.Log("Not Debug stage number or not valid");
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if(stage == 0)
        {

        }
        if (stage == -1)
        {
            if (timer <= Time.fixedTime)
            {
                Debug.Log("Placing Asteroid");
                GameObject asteroid = (GameObject)Instantiate(Asteroid, generate_random_direction(direction.any), Quaternion.identity);
                Enemy_Controller EC = asteroid.GetComponent<Enemy_Controller>();
                EC.movement_speed = 0.5f;
                timer = stage_1_delay + Time.fixedTime;
            }
        }
        else if (stage == 1)
        {
            Stage1();
        }
        else if (stage == 2)
        {
            Stage2();
        }
        else if (stage == 3)
        {
            Stage3();
        }
        else if (stage == 4)
        {
            Stage4();
        }
        else if (stage == 5)
        {
            Stage5();
        }
        else if (stage == 6)
        {
            Stage6();
        }
        else if (stage == 7)
        {
            Stage7();
        }
        else if (stage == 8)
        {
            Stage8();
        }
        else if (stage == 9)
        {
            Stage9();
        }
        else if (stage == 10)
        {
            Stage10();
        }
    }

    public void Stage1()
    {
        //Asteroids from above, in waves
        if (timer <= Time.fixedTime)
        {
            for (int i = 0; i < 10; ++i)
            {
                Debug.Log("Placing Astroid, Behavior: Default, From Above");
                GameObject asteroid = (GameObject)Instantiate(Asteroid, generate_random_direction(direction.up), Quaternion.identity);
                Enemy_Controller EC = asteroid.GetComponent<Enemy_Controller>();
                EC.movement_speed = 0.5f;
                if (i == 0)
                    EC.color = Pellet_Controller.colors.red;
                else if (i == 1)
                    EC.color = Pellet_Controller.colors.green;
                else
                    EC.color = Pellet_Controller.colors.blue;
            }

            --stage_1_asteroid_groups;
            timer = stage_1_delay + Time.fixedTime;
        }
        if (stage_1_asteroid_groups == 0)
        {
            stage = 0;
        }
    }

    public void Stage2()
    {
        //Three waves of scouts, each wave is a different color, default behavior, from above
        if (timer <= Time.fixedTime)
        {
            for (int i = 0; i < 10; ++i)
            {
                Debug.Log("Placing Scout, Behavior: Default, From Above");
                GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.up), Quaternion.identity);
                Enemy_Controller EC = scout.GetComponent<Enemy_Controller>();
                if (i == 0)
                    EC.color = Pellet_Controller.colors.red;
                else if (i == 1)
                    EC.color = Pellet_Controller.colors.green;
                else
                    EC.color = Pellet_Controller.colors.blue;
            }

            --stage_2_scout_groups;
            timer = stage_2_delay + Time.fixedTime;
        }
        if (stage_2_scout_groups == 0)
        {
            stage = 0;
        }
    }

    public void Stage3()
    {
        //6 randomly colored waves from above of scouts, + 1 lone scout from below
        rand = (float)Random.Range(1, 4);
        if (stage_3_scout_groups > 0 && timer <= Time.fixedTime)
        {
            for (int i = 0; i < 15; ++i)
            {
                Debug.Log("Placing Scout, Behavior: Default, From Above");
                GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.up), Quaternion.identity);
                Enemy_Controller EC = scout.GetComponent<Enemy_Controller>();
                if (rand == 0)
                    EC.color = Pellet_Controller.colors.red;
                else if (rand == 1)
                    EC.color = Pellet_Controller.colors.green;
                else
                    EC.color = Pellet_Controller.colors.blue;
            }

            --stage_3_scout_groups;
            timer = stage_3_delay + Time.fixedTime;
        }
        if (stage_3_scout_groups == 0 && timer <= Time.fixedTime)
        {
            Debug.Log("Placing Scout, Behavior: Default, From Below");
            GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.down), Quaternion.identity);
            Enemy_Controller EC = scout.GetComponent<Enemy_Controller>();
            if (rand == 0)
                EC.color = Pellet_Controller.colors.red;
            else if (rand == 1)
                EC.color = Pellet_Controller.colors.green;
            else
                EC.color = Pellet_Controller.colors.blue;
            stage = 0;
        }
    }

    public void Stage4()
    {
        //Spawn 3 fighters from above, and continue to spawn asteroids from above
        for (int i = 0; i < 3; ++i)
        {
            Debug.Log("Placing Scout, Behavior: Default, From Above");
            GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.up), Quaternion.identity);
            Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
            if (!stage_7_run)
            {
                if (i == 0)
                    EC.color = Pellet_Controller.colors.red;
                else if (i == 1)
                    EC.color = Pellet_Controller.colors.green;
                else
                    EC.color = Pellet_Controller.colors.blue;
            }
            else
            {
                if (i == 0)
                    EC.color = Pellet_Controller.colors.cyan;
                else if (i == 1)
                    EC.color = Pellet_Controller.colors.magenta;
                else
                    EC.color = Pellet_Controller.colors.yellow;
                stage_7_run = false;
            }
        }
        stage = 1;
    }

    public void Stage5()
    {
        //Spawn 3 fighters from left, right, and up, then scouts of the random color from those directions
        Debug.Log("Placing Fighter, Behavior: Default, From Above");
        GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.up), Quaternion.identity);
        Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.red;
        Debug.Log("Placing Fighter, Behavior: Default, From Left");
        GameObject fighter1 = (GameObject)Instantiate(Fighter, generate_random_direction(direction.left), Quaternion.identity);
        Enemy_Controller EC1 = fighter1.GetComponent<Enemy_Controller>();
        EC1.color = Pellet_Controller.colors.blue;
        Debug.Log("Placing Fighter, Behavior: Default, From Right");
        GameObject fighter2 = (GameObject)Instantiate(Fighter, generate_random_direction(direction.right), Quaternion.identity);
        Enemy_Controller EC2 = fighter2.GetComponent<Enemy_Controller>();
        EC2.color = Pellet_Controller.colors.green;
        for (int i = 0; i < 15; ++i)
        {
            if (i == 0)
            {
                Debug.Log("Placing Scout, Behavior: Default, From Above");
                GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.up), Quaternion.identity);
                Enemy_Controller EC3 = scout.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.blue;
            }
            else if (i == 1)
            {
                Debug.Log("Placing Scout, Behavior: Default, From Left");
                GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.left), Quaternion.identity);
                Enemy_Controller EC3 = scout.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.green;
            }
            else
            {
                Debug.Log("Placing Scout, Behavior: Default, From Right");
                GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.right), Quaternion.identity);
                Enemy_Controller EC3 = scout.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.red;
            }
        }
        stage = 0;
    }

    public void Stage6()
    {
        //Spawn 8 fighters, two from each direction, then scouts of the random color from those directions
        for (int i = 0; i < 8; ++i)
        {
            if (i == 0 || i == 7)
            {
                Debug.Log("Placing Fighter, Behavior: Default, From Above");
                GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.up), Quaternion.identity);
                Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.red;
            }
            else if (i == 1 || i == 6)
            {
                Debug.Log("Placing Fighter, Behavior: Default, From Right");
                GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.right), Quaternion.identity);
                Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.green;
            }
            else if (i == 2 || i == 5)
            {
                Debug.Log("Placing Fighter, Behavior: Default, From Left");
                GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.left), Quaternion.identity);
                Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.blue;
            }
            else
            {
                Debug.Log("Placing Fighter, Behavior: Default, From Below");
                GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.down), Quaternion.identity);
                Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
                EC.color = Pellet_Controller.colors.red;
            }
        }
        for (int i = 0; i < 60; ++i)
        {
            rand = (float)Random.Range(0, 3);
            Debug.Log("Placing Scout, Behavior: Default, From Above");
            GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.up), Quaternion.identity);
            Enemy_Controller EC = scout.GetComponent<Enemy_Controller>();
            if (rand == 0)
            {
                EC.color = Pellet_Controller.colors.blue;
            }
            else if (rand == 1)
            {
                EC.color = Pellet_Controller.colors.green;
            }
            else if (rand == 2)
            {
                EC.color = Pellet_Controller.colors.red;
            }
        }
        stage = 0;
    }

    public void Stage7()
    {
        //Spawn a secondary color fighter, wait a bit, then spawn more and asteroids
        if (!stage_7_run)
        {
            Debug.Log("Placing Fighter, Behavior: Default, OnScreen");
            GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.onscreen), Quaternion.identity);
            Enemy_Controller EC = fighter.GetComponent<Enemy_Controller>();
            EC.color = Pellet_Controller.colors.yellow;
            stage_7_run = true;
        }
        if (timer <= Time.fixedTime)
        {
            stage = 4;
        }
    }

    public void Stage8()
    {
        //Spawn 1 turrent, then add a few waves of scouts
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC = turrent.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.red;
        stage = 2;
    }

    public void Stage9()
    {
        //Place 3 turrents, some scouts, and some secondary colored fighters
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC = turrent.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.red;
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent1 = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC1 = turrent1.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.blue;
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent2 = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC2 = turrent2.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.green;
        for (int i = 0; i < 60; ++i)
        {
            rand = (float)Random.Range(0, 3);
            Debug.Log("Placing Scout, Behavior: Default, From Anywhere");
            GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.any), Quaternion.identity);
            Enemy_Controller EC3 = scout.GetComponent<Enemy_Controller>();
            if (rand == 0)
            {
                EC3.color = Pellet_Controller.colors.blue;
            }
            else if (rand == 1)
            {
                EC3.color = Pellet_Controller.colors.green;
            }
            else
            {
                EC3.color = Pellet_Controller.colors.red;
            }
        }
        Debug.Log("Placing Fighter, Behavior: Default, From Below");
        GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.down), Quaternion.identity);
        Enemy_Controller EC4 = fighter.GetComponent<Enemy_Controller>();
        EC4.color = Pellet_Controller.colors.cyan;
        Debug.Log("Placing Fighter, Behavior: Default, From Below");
        GameObject fighter1 = (GameObject)Instantiate(Fighter, generate_random_direction(direction.down), Quaternion.identity);
        Enemy_Controller EC5 = fighter1.GetComponent<Enemy_Controller>();
        EC5.color = Pellet_Controller.colors.magenta;
        Debug.Log("Placing Fighter, Behavior: Default, From Below");
        GameObject fighter2 = (GameObject)Instantiate(Fighter, generate_random_direction(direction.down), Quaternion.identity);
        Enemy_Controller EC6 = fighter2.GetComponent<Enemy_Controller>();
        EC6.color = Pellet_Controller.colors.yellow;
        stage = 0;
    }

    public void Stage10()
    {
        //Doom.
        //Spawn 3 secondary color turrents, then 120 secondary colored scouts, then 3 secondary color fighters, then infinite asteroids, 1 per 2 seconds
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC = turrent.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.cyan;
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent1 = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC1 = turrent1.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.magenta;
        Debug.Log("Placing Turrent, Behavior: Default, OnScreen");
        GameObject turrent2 = (GameObject)Instantiate(Turrent, generate_random_direction(direction.onscreen), Quaternion.identity);
        Enemy_Controller EC2 = turrent2.GetComponent<Enemy_Controller>();
        EC.color = Pellet_Controller.colors.yellow;
        for (int i = 0; i < 120; ++i)
        {
            rand = (float)Random.Range(0, 3);
            Debug.Log("Placing Scout, Behavior: Default, From Anywhere");
            GameObject scout = (GameObject)Instantiate(Scout, generate_random_direction(direction.any), Quaternion.identity);
            Enemy_Controller EC3 = scout.GetComponent<Enemy_Controller>();
            if (rand == 0)
            {
                EC3.color = Pellet_Controller.colors.cyan;
            }
            else if (rand == 1)
            {
                EC3.color = Pellet_Controller.colors.magenta;
            }
            else
            {
                EC3.color = Pellet_Controller.colors.yellow;
            }
        }
        Debug.Log("Placing Fighter, Behavior: Default, From Below");
        GameObject fighter = (GameObject)Instantiate(Fighter, generate_random_direction(direction.any), Quaternion.identity);
        Enemy_Controller EC4 = fighter.GetComponent<Enemy_Controller>();
        EC4.color = Pellet_Controller.colors.cyan;
        Debug.Log("Placing Fighter, Behavior: Default, From Below");
        GameObject fighter1 = (GameObject)Instantiate(Fighter, generate_random_direction(direction.any), Quaternion.identity);
        Enemy_Controller EC5 = fighter1.GetComponent<Enemy_Controller>();
        EC5.color = Pellet_Controller.colors.magenta;
        Debug.Log("Placing Fighter, Behavior: Default, From Below");
        GameObject fighter2 = (GameObject)Instantiate(Fighter, generate_random_direction(direction.any), Quaternion.identity);
        Enemy_Controller EC6 = fighter2.GetComponent<Enemy_Controller>();
        EC6.color = Pellet_Controller.colors.yellow;
        stage = -1;
    }

    public enum direction { up, down, left, right, horizontal, vertical, any, onscreen };
    public Vector3 generate_random_direction(direction dir)
    {
        int a = (int)Mathf.Round(s_width / 2);
        int b = (int)Mathf.Round(s_width / 8);
        int c = (int)Mathf.Round(s_height / 2);
        int d = (int)Mathf.Round(s_height / 8);
        int z_inner_bound = a + b;
        int z_outer_bound = z_inner_bound + b;
        int x_inner_bound = c + d;
        int x_outer_bound = x_inner_bound + d;
        float z_right = Random.Range(z_inner_bound, z_outer_bound);
        float z_left = Random.Range(-z_outer_bound, -z_inner_bound);
        float x_down = Random.Range(x_inner_bound, x_outer_bound);
        float x_up = Random.Range(-x_outer_bound, -x_inner_bound);
        //Values outside of the screen, random between up/down, left/right
        float z_out = Random.value > 0.5f ? z_left : z_right;
        float x_out = Random.value > 0.5f ? x_up : x_down;
        //Values somewhere within the screen bounds
        float x_in = Random.Range(-c, c);
        float z_in = Random.Range(-a, a);
        /*Debug.Log(s_width);
        Debug.Log(s_height);
        Debug.Log(x_inner_bound);
        Debug.Log(x_outer_bound);
        Debug.Log(z_inner_bound);
        Debug.Log(z_outer_bound);
        Debug.Log(x_in);
        Debug.Log(z_in);
        Debug.Log(x_out);
        Debug.Log(z_out);*/

        if (dir == direction.any)
        {
            if (Random.value > 0.5f)
                dir = direction.horizontal;
            else
                dir = direction.vertical;
        }
        if (dir == direction.onscreen)
        {
            return new Vector3(x_in, 0.0f, z_in);
        }
        else if (dir == direction.up)
        {
            return new Vector3(x_up, 0.0f, z_in);
        }
        else if (dir == direction.down)
        {
            return new Vector3(x_down, 0.0f, z_in);
        }
        else if (dir == direction.left)
        {
            return new Vector3(x_in, 0.0f, z_left);
        }
        else if (dir == direction.right)
        {
            return new Vector3(x_in, 0.0f, z_right);
        }
        else if (dir == direction.horizontal)
        {
            return new Vector3(x_in, 0.0f, z_out);
        }
        else if (dir == direction.vertical)
        {
            return new Vector3(x_out, 0.0f, z_in);
        }
        else
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
