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

    //debug firing
    public Pellet_Shooter_Controller pellet_shooter;
    public float shine_time = 0.2f;

    public bool move;
    public bool aim;
    public float firing_timer;
    public float firing_time;
    public bool firing;
    public bool has_special;
    bool use_special;

    public float rotation_speed;
    public float movement_speed;

    Vector3 aim_vector;
    Quaternion target_rotation;

    Vector3 move_vector;

	void Start ()
    {
        has_special = true;
	}
	
	void Update ()
    {
        if (Input.GetAxis("Move" + player + "X") < -0.1f || Input.GetAxis("Move"+ player + "X") > 0.1f || 
            Input.GetAxis("Move" + player + "Y") < -0.1f || Input.GetAxis("Move" + player + "Y") > 0.1f)
        {
            move_vector = new Vector3(Input.GetAxisRaw("Move" + player + "Y"), 0.0f, Input.GetAxisRaw("Move" + player + "X"));
            move = true;
        }

        if (move == true)
        {
            transform.position += move_vector * movement_speed * Time.deltaTime;
            move = false;
        }

        if (Input.GetAxis("Aim" + player + "X") < -0.1f || Input.GetAxis("Aim" + player + "X") > 0.1f ||
            Input.GetAxis("Aim" + player + "Y") < -0.1f || Input.GetAxis("Aim" + player + "Y") > 0.1f)
        {
            firing = true;
            firing_timer += Time.deltaTime;

            aim_vector = new Vector3(-Input.GetAxisRaw("Aim" + player + "Y"), 0.0f, -Input.GetAxisRaw("Aim" + player+ "X"));
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


        if (Input.GetAxisRaw("Special" + player) == 1)
        {
            if (has_special == true)
                use_special = true;
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

        if (use_special == true)
        {
            has_special = false;
            if(player == "1")
            {

            }
            else if(player =="2")
            {

            }
            else if(player == "3")
            {

            }
        }
         
    } 
}