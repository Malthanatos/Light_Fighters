using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBox : MonoBehaviour {

    public Game_Manager GM;

    private bool red_hit=false;
    private bool green_hit=false;
    private bool blue_hit=false;

    // Use this for initialization
    void Start ()
    {
        //GM = GameObject.FindObjectOfType<Game_Manager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(red_hit && blue_hit && green_hit)
        {
            Destroy(this.gameObject);
            GM.StartGame();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;

        //Assumes only thing hitting it is pellet
        if (other.GetComponent<Pellet_Controller>().color == Pellet_Controller.colors.green)
        {
            green_hit = true;
            if (red_hit && green_hit)
            {
                mat.SetColor("_Color", Color.yellow);
            }
            else if (blue_hit && green_hit)
            {
                mat.SetColor("_Color", Color.cyan);
            }
            else if(green_hit)
            { 
                mat.SetColor("_Color", Color.green);
            }
        }
        else if (other.GetComponent<Pellet_Controller>().color == Pellet_Controller.colors.red)
        {
            red_hit = true;
            if (red_hit && green_hit)
            {
                mat.SetColor("_Color", Color.yellow);
            }
            else if (blue_hit && red_hit)
            {
                mat.SetColor("_Color", Color.magenta);
            }
            else if (red_hit)
            {
                mat.SetColor("_Color", Color.red);
            }
        }
        else if (other.GetComponent<Pellet_Controller>().color == Pellet_Controller.colors.blue)
        {
            blue_hit = true;
            if (blue_hit && green_hit)
            {
                mat.SetColor("_Color", Color.cyan);
            }
            else if (blue_hit && red_hit)
            {
                mat.SetColor("_Color", Color.magenta);
            }
            else if (blue_hit)
            {
                mat.SetColor("_Color", Color.blue);
            }
        }
    }
}
