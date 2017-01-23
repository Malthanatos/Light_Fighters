using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent_Controller : Enemy_Controller
{
    public float timer = 0.0f;
    public float delay = 5.0f;
    private bool left = true;

    public override void Default_Behvaior()
    {
        target();
        if (timer == 0.0f)
        {
            timer = Time.fixedTime + delay;
        }
        if (timer <= Time.fixedTime)
        {
            fire_missile();
            timer = Time.fixedTime + delay;
        }
    }
    public override void Formation_Behvaior() { }
    public override void Drift_Behvaior() { }

    private void fire_missile()
    {
        Vector3 shift = Vector3.zero;
        //If we can fix this it would fire alternating left/right launcher positions
        //if (left) {
            //shift = new Vector3(4.0f, 0.0f, -3.0f);
        //}
        //else
        //{
            //shift = new Vector3(-4.0f, 0.0f, -3.0f);
        //}
        left = !left;
        //Debug.Log("Firing Missile");
        GameObject missile = (GameObject)Instantiate(EG.Missile, transform.localPosition + shift, transform.rotation);
        Missile_Controller MC = missile.GetComponent<Missile_Controller>();
        MC.movement_speed = 25.0f;
    }
}
