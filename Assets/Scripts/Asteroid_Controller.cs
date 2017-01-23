using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Controller : Enemy_Controller
{
    private Vector3 go_to;

    public override void Default_Behvaior()
    {
        if (transform.rotation == target_rotation)
        {
            target_rotation = Random.rotation;
        }
        if (movement_vector == new Vector3(0.0f, 0.0f, 0.0f))
        {
            go_to = EG.generate_random_direction(Enemy_Generator.direction.onscreen);
            //Debug.Log(go_to);
            movement_vector = Vector3.Normalize(go_to - transform.position);
            //Debug.Log(movement_vector);
        }
    }
    public override void Formation_Behvaior() { }
    public override void Drift_Behvaior() { }
}
