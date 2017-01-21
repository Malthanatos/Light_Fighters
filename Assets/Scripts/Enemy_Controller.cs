using System.Collections;
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
    private Game_Manager GM;

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {

    }
}
