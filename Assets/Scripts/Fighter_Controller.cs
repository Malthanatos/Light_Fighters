using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Controller : Enemy_Controller
{
    public float timer = 0.0f;
    public float delay = 5.0f;
    private bool left = true;

    public override void Default_Behvaior()
    {
        float dist = target();
        if (dist <= 2.0f)
        {
            Destroy(gameObject);
        }
        movement_speed = 10.0f;
        transform.position += (transform.forward * movement_speed * Time.deltaTime);
        if (timer == 0.0f)
        {
            timer = Time.fixedTime + delay;
        }
        if (timer <= Time.fixedTime)
        {
            //fire
            timer = Time.fixedTime + delay;
        }
    }
    public override void Formation_Behvaior() { }
    public override void Drift_Behvaior() { }
}
