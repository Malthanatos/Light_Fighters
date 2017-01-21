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

public class Player_Three_Controller : MonoBehaviour
{
    private Game_Manager GM;
    private Laser_Controller LC;

    public bool move;
    public bool aim;

    public float rotation_speed;
    public float movement_speed;

    Vector3 aim_vector;
    Quaternion target_rotation;

    Vector3 move_vector;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetAxis("Move3X") < -0.1f || Input.GetAxis("Move3X") > 0.1f ||
            Input.GetAxis("Move3Y") < -0.1f || Input.GetAxis("Move3Y") > 0.1f)
        {
            move_vector = new Vector3(Input.GetAxisRaw("Move3Y"), 0.0f, Input.GetAxisRaw("Move3X"));
            move = true;
        }

        if (Input.GetAxis("Aim3X") < -0.1f || Input.GetAxis("Aim3X") > 0.1f ||
            Input.GetAxis("Aim3Y") < -0.1f || Input.GetAxis("Aim3Y") > 0.1f)
        {
            aim_vector = new Vector3(-Input.GetAxisRaw("Aim3Y"), 0.0f, -Input.GetAxisRaw("Aim3X"));
            target_rotation = Quaternion.LookRotation(aim_vector, transform.up);
            aim = true;
        }
    }

    void FixedUpdate()
    {
        if (move == true)
        {
            transform.position += move_vector * movement_speed;
            move = false;
        }

        if (aim == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, rotation_speed);
            aim = false;
        }
    }
}
