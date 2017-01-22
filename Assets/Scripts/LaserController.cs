using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    public Pellet_Shooter_Controller yellow_shooter;
    public Pellet_Shooter_Controller cyan_shooter;
    public Pellet_Shooter_Controller magenta_shooter;
    public Pellet_Shooter_Controller white_shooter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void yellowShoot(Vector3 hit, Quaternion angle)
    {
        print("yellow shot");
        yellow_shooter.transform.position=hit;
        yellow_shooter.transform.rotation=angle;
        yellow_shooter.Shoot();
    }

    public void cyanShoot(Vector3 hit, Quaternion angle)
    {
        print("cyan shot");
        cyan_shooter.transform.position = hit;
        cyan_shooter.transform.rotation = angle;
        cyan_shooter.Shoot();
    }
    
    public void magentaShoot(Vector3 hit, Quaternion angle)
    {
        print("magenta shot");
        magenta_shooter.transform.position = hit;
        magenta_shooter.transform.rotation = angle;
        magenta_shooter.Shoot();
    } 
    public void whiteShoot(Vector3 hit, Quaternion angle)
    {
        print("white shot");
        white_shooter.transform.position = hit;
        white_shooter.transform.rotation = angle;
        white_shooter.Shoot();
    }


}
