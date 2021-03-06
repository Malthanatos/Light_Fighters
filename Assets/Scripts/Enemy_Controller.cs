﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Enemy_Controller.cs:
Has References to: Game_Manager, 
Which class governs which mechanics: scout, fighter, turrent, boss, asteroid, missile
All attacks of the same type do equal damage (damage is done in Player_Controller)
Any enemy can be any color, except boss, which is always white
Checks color of impacts to determine damage from player
All objects can be collided with
Asteroids may drop power-ups, despawn after they leave the screen
Scouts will kamakaze anything they are near, they tend to swarm, won't hit each other
Fighters will use beam attacks and tend to target colors opposite to theirs (both primary and secondary colors)
Turrents will use missiles and tend to attack anyone and do not move
Missiles will are very slow, they track their designated target and explode after a certain amount of time
Missiles can only be directly destroyed by their color
Boss will do nothing, instant death on collision
*/

public class Enemy_Controller : MonoBehaviour
{
    public Game_Manager GM;
    public Enemy_Generator EG;

    public enum behavior_type { default_behavior, formation, drift };
    public behavior_type actions = behavior_type.default_behavior;
    public ArrayList formation_spec;
    public float movement_speed;
    public float rotation_speed;
    public Vector3 movement_vector;
    public Vector3 aim_vector;
    public Quaternion target_rotation;
    public bool live;
    public bool bounds;
    public Pellet_Controller.colors color = Pellet_Controller.colors.none;
    public bool has_color = false;
    public Renderer r;

    public float health = 100;

    public void Start()
    {
        GM = GameObject.FindObjectOfType<Game_Manager>();
        EG = GameObject.FindObjectOfType<Enemy_Generator>();
        bounds = false;
        r = GetComponent<Renderer>();
    }

    public void Update()
    {
        if(color != Pellet_Controller.colors.none && !has_color)
        {
            if (color == Pellet_Controller.colors.red)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[0];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[0];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[0];
                }
            }
            if (color == Pellet_Controller.colors.green)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[1];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[1];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[1];
                }
            }
            if (color == Pellet_Controller.colors.blue)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[2];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[2];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[2];
                }
            }
            if (color == Pellet_Controller.colors.cyan)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[3];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[3];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[3];
                }
            }
            if (color == Pellet_Controller.colors.magenta)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[4];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[4];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[4];
                }
            }
            if (color == Pellet_Controller.colors.yellow)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[5];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[5];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[5];
                }
            }
            if (color == Pellet_Controller.colors.gray)
            {
                if (gameObject.name.Contains("Scout"))
                {
                    r.sharedMaterial = EG.small[6];
                }
                if (gameObject.name.Contains("Fighter"))
                {
                    r.sharedMaterial = EG.medium[6];
                }
                if (gameObject.name.Contains("Turrent"))
                {
                    r.sharedMaterial = EG.large[6];
                }
            }

            has_color = true;
        }



        if (health <= 0)
            Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        switch (actions)
        {
            case behavior_type.default_behavior:
                Default_Behvaior();
                break;
            case behavior_type.formation:
                Formation_Behvaior();
                break;
            case behavior_type.drift:
                Drift_Behvaior();
                break;
            default:
                return;
        }
        if (live)// && !GM.DEBUG)
        {
            transform.position += movement_vector * movement_speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, rotation_speed);
        }
        if (!bounds && On_Screen())
        {
            bounds = true;
        }
        if (bounds && !On_Screen())
        {
            Destroy(gameObject);
        }
    }

    public bool On_Screen()
    {
        if (transform.position.x > -EG.s_height && transform.position.x < EG.s_height)
            if (transform.position.z > -EG.s_width && transform.position.z < EG.s_width)
                return true;
        return false;
    }

    public virtual void Default_Behvaior() { }
    public virtual void Formation_Behvaior() { }
    public virtual void Drift_Behvaior() { }

    private float dist_to(float x, float z)
    {
        float dx = transform.position.x - x;
        float dz = transform.position.z - z;
        return Mathf.Sqrt(dx * dx + dz * dz);
    }

    public float target()
    {
        float dist = 0.0f;
        float dist_to_i = 0.0f;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = players[0];
        for (int i = 0; i < players.Length; ++i)
        {
            dist_to_i = Vector3.Distance(transform.position, players[i].transform.position);
            if (i == 0 || dist > dist_to_i)
            {
                dist = dist_to_i;
                player = players[i];
            }
        }
        if (dist != 0.0f)
        {
            transform.LookAt(player.transform);
        }
        return dist;
    }
}