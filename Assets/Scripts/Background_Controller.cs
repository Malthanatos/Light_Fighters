using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Controller : MonoBehaviour {

    public float scrollx = 0.1f;

	void Start () {
        transform.position = new Vector3(-2700.0f, -0.1f, 1.0f);
	}
	
	void FixedUpdate () {
        if (transform.position.x >= 2700.0f)
        {
            transform.position = new Vector3(-2700.0f, -0.1f, 1.0f);
        }
        transform.position += new Vector3(scrollx, 0.0f, 0.0f);
	}
}
