using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout_Controller : Enemy_Controller
{
    public override void Default_Behvaior()
    {
        float dist = target();
        if (dist <= 2.0f)
        {
            Destroy(gameObject);
        }
        movement_speed = 40.0f;
        transform.position += (transform.forward * movement_speed * Time.deltaTime);
    }
    public override void Formation_Behvaior() { }
    public override void Drift_Behvaior() { }
}