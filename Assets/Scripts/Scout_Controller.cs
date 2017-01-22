using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout_Controller : Enemy_Controller
{
    public override void Default_Behvaior()
    {
        float dist = target();
        if (dist <= 1.0f)
        {
            Destroy(gameObject);
        }
        //movement_vector = Vector3.forward;
        movement_speed = 5.0f;
        transform.position += (transform.forward * movement_speed * Time.deltaTime);
    }
    public override void Formation_Behvaior() { }
    public override void Drift_Behvaior() { }
}