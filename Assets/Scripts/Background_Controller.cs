using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Controller : MonoBehaviour {

    public float scrollx = 0.1f;
    public float resetx = 2700.0f;
    public float startx = -2650.0f;
    public int background = 0;
    public Material[] mats;
    public Renderer r;

	void Start () {
        r = GetComponent<Renderer>();
        r.enabled = true;
        transform.position = new Vector3(startx, -0.1f, 1.0f);
	}
	
	void FixedUpdate () {
        if (transform.position.x >= resetx)
        {
            ++background;
            if (background >= 4)
            {
                background = 0;
            }
            transform.position = new Vector3(startx, -0.1f, 1.0f);
            r.sharedMaterial = mats[background];
        }
        transform.position += new Vector3(scrollx, 0.0f, 0.0f);
    }
}
