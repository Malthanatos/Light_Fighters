using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Laser_Pellet_Controller.cs:
Has References to: Game_Manager, Pellet_Shooter_Controller,
Default pellet is enemy pellet, auto deletes iteself on collision
Governs the local behavior of given pellets
Beams are pellets that are rotating spring shapes
When the laser pellets hit something they do something corresponding to the type of collision, then self destruct
Signals Laser_Controller when two colored pellets collide
Deletes when it collides with an uncolored pellet
*/

public class Pellet_Controller : MonoBehaviour
{
    private Game_Manager GM;
    public LaserController LC;
    
    public float speed;

    public float x_bounds;
    public float y_bounds;

    public enum colors { red, blue, green, yellow, cyan, magenta, white, gray, none };
    public colors color;

    void Start ()
    {
		GM = GameObject.FindObjectOfType<Game_Manager>();
        LC = GameObject.FindObjectOfType<LaserController>();
    }
	
	void Update ()
    {
        transform.position +=(transform.forward*speed*Time.deltaTime);
        if (transform.position.x > x_bounds)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -x_bounds)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > y_bounds)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < -y_bounds)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {   
        //print("I hit something!");

        Vector3 hit = this.transform.position;
        Quaternion angle = Quaternion.identity;

        angle = Quaternion.Slerp(this.transform.rotation, other.transform.rotation, 0.5f);

        if (other.gameObject.tag == "Pellet")
        {
            if (this.color == colors.red && other.GetComponent<Pellet_Controller>().color == colors.green)
            {
                print("Red and Green Collided ");
                //move and orient yellow object
                LC.yellowShoot(hit, angle);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else if (this.color == colors.blue && other.GetComponent<Pellet_Controller>().color == colors.green)
            {
                print("Blue and Green Collided");
                //move and orient cyan object
                LC.cyanShoot(hit, angle);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else if (this.color == colors.blue && other.GetComponent<Pellet_Controller>().color == colors.red)
            {
                print("Blue and Red collided");
                //move and orient magena object?
                LC.magentaShoot(hit, angle);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else if (this.color == colors.red && other.GetComponent<Pellet_Controller>().color == colors.cyan)
            {
                print("Red and Cyan Collided ");
                //move and orient white object?
                LC.whiteShoot(hit, angle);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else if (this.color == colors.blue && other.GetComponent<Pellet_Controller>().color == colors.yellow)
            {
                print("Blue and Yellow Collided");
                //move and orient white object
                LC.whiteShoot(hit, angle);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else if (this.color == colors.green && other.GetComponent<Pellet_Controller>().color == colors.magenta)
            {
                print("Green and Magenta collided");
                //move and orient white object
                LC.whiteShoot(hit, angle);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                print("Unneeded collision");
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (GM.DEBUG == true)
                print("I am hitting a player");
            Destroy(gameObject);
            //Heal that Player
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (GM.DEBUG == true)
                print("I am hitting an enemy");
            if (!other.gameObject.name.Contains("Start"))
            {
                if (other.gameObject.name.Contains("Asteroid"))
                {
                    other.GetComponent<Enemy_Controller>().health -= 25;
                }
                else if (other.gameObject.name.Contains("Missile"))
                {
                    other.GetComponent<Enemy_Controller>().health -= 100;
                }
                else if (other.gameObject.name.Contains("Scout"))
                {
                    if (other.gameObject.GetComponent<Enemy_Controller>().color == this.color)
                        other.GetComponent<Enemy_Controller>().health -= 100;
                    else
                        other.GetComponent<Enemy_Controller>().health -= 25;
                }
                else if (other.gameObject.name.Contains("Fighter"))
                {
                    if (other.gameObject.GetComponent<Enemy_Controller>().color == this.color)
                        other.GetComponent<Enemy_Controller>().health -= 50f;
                    else
                        other.GetComponent<Enemy_Controller>().health -= 12.5f;
                }
                else if (other.gameObject.name.Contains("Turrent"))
                {
                    if (other.gameObject.GetComponent<Enemy_Controller>().color == this.color)
                        other.GetComponent<Enemy_Controller>().health -= 25f;
                    else
                        other.GetComponent<Enemy_Controller>().health -= 6.25f;
                }
                Destroy(gameObject);
            }
            //Damage that Enemy
        }
    } 
}
