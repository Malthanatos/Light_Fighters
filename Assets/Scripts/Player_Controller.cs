using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Player_Controller.cs:
Has References to: Game_Manager, Laser_Controller, 
WASD/Arrow for movement/aim by default
Right stick/Left stick for movement and aim for PS4 and XBox controllers
Firing is automatic by aim
Binds controller when player presses A/X
Enter/Start for pause -> signal to Game_Manager
Left shift/Left trigger for secondary fire
OnTriggerEnter for power-ups
Power-ups are passive
Health increases when hit by other players
Heath decreases according to type of damage
Collsion: minimal damage
Beams: moderate damage
Missile: instant death on direct hit, some splash damage
Boss: instant death on collision
*/

public class Player_Controller : MonoBehaviour
{
    public Game_Manager GM;
    //public Pellet_Shooter PS;

    public string player;

    public float health = 100.0f;

    //debug firing
    public Pellet_Shooter_Controller pellet_shooter;
    public float shine_time = 0.2f;

    public bool dead = false;
    public bool move;
    public bool aim;
    public float firing_timer;
    public float firing_time;
    public bool firing;

    public float rotation_speed;
    public float movement_speed;

    public float x_bounds;
    public float y_bounds;

    Vector3 aim_vector;
    Quaternion target_rotation;

    Vector3 move_vector;

    

	void Start ()
    {
        
	}
	
	void Update ()
    {
        if (health == 0)
            dead = true;
        else if (health == 100)
            dead = false;

        EmissionLevel();

        if (!dead)
        {
            if (Input.GetAxis("Move" + player + "X") < -0.1f || Input.GetAxis("Move" + player + "X") > 0.1f ||
                Input.GetAxis("Move" + player + "Y") < -0.1f || Input.GetAxis("Move" + player + "Y") > 0.1f)
            {
                move_vector = new Vector3(Input.GetAxisRaw("Move" + player + "Y"), 0.0f, Input.GetAxisRaw("Move" + player + "X"));
                move = true;
            }

            if (move == true)
            {
                transform.position += move_vector * movement_speed * Time.deltaTime;
                if (transform.position.x > x_bounds)
                {
                    transform.position = new Vector3(x_bounds, 0.0f, transform.position.z);
                }
                else if (transform.position.x < -x_bounds)
                {
                    transform.position = new Vector3(-x_bounds, 0.0f, transform.position.z);
                }

                if (transform.position.z > y_bounds)
                {
                    transform.position = new Vector3(transform.position.x, 0.0f, y_bounds);
                }
                else if (transform.position.z < -y_bounds)
                {
                    transform.position = new Vector3(transform.position.x, 0.0f, -y_bounds);
                }
                move = false;
            }

            if (Input.GetAxis("Aim" + player + "X") < -0.1f || Input.GetAxis("Aim" + player + "X") > 0.1f ||
                Input.GetAxis("Aim" + player + "Y") < -0.1f || Input.GetAxis("Aim" + player + "Y") > 0.1f)
            {
                firing = true;
                firing_timer += Time.deltaTime;

                aim_vector = new Vector3(-Input.GetAxisRaw("Aim" + player + "Y"), 0.0f, -Input.GetAxisRaw("Aim" + player + "X"));
                target_rotation = Quaternion.LookRotation(aim_vector, transform.up);
                aim = true;
            }
            else
            {
                firing = false;
                if (firing_timer > 0)
                    firing_timer -= Time.deltaTime;

                if (firing_timer < 0)
                    firing_timer = 0;
            }

            //debug firing
            if (GM.DEBUG == true)
            {
                if (pellet_shooter.GetComponent<MeshRenderer>().enabled == true)
                {
                    shine_time -= Time.deltaTime;
                    if (shine_time <= 0.0f)
                    {
                        pellet_shooter.GetComponent<MeshRenderer>().enabled = false;
                        shine_time = 0.2f;
                    }

                }
            }
        }
    }

    void EmissionLevel()
    {
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;
        Color base_color = Color.red;

        float emission = health / 100;
        if (player == "1")
            base_color = Color.red;
        else if (player == "2")
            base_color = Color.green;
        else if (player == "3")
            base_color = Color.blue;

        Color final_color = base_color * emission * 2;

        mat.SetColor("_EmissionColor", final_color);

    }

   void FixedUpdate()
    {

        if (aim == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, rotation_speed);
            aim = false;
        }

        if (firing == true && firing_timer >= firing_time)
        {
            //if debug, light up debug
            if (GM.DEBUG == true)
                pellet_shooter.GetComponent<MeshRenderer>().enabled = true;

            //shoot
            pellet_shooter.Shoot();

            firing_timer = 0;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (GM.DEBUG)
                print("I hit an enemy");

            if (other.gameObject.name.Contains("Asteroid"))
            {
                health -= 25;
                if (health < 0)
                    health = 0;
            }

            else if (other.gameObject.name.Contains("Scout"))
            {
                health -= 10;
                if (health < 0)
                    health = 0;
            }

        }

        if(other.gameObject.tag == "Pellet")
        {
            if (other.gameObject.name.Contains("Grey"))
            {
                health -= 10;
                if (health < 0)
                    health = 0;
            }
            else
            {
                health += 1;
                if (health > 100)
                    health = 100;
                    
            }
        }
    }
}