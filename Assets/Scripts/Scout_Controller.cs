using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout_Controller : Enemy_Controller
{
    public override void Default_Behvaior()
    {
        target();
    }
    public override void Formation_Behvaior() { }
    public override void Drift_Behvaior() { }
}