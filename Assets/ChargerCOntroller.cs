using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerCOntroller : MonoBehaviour {

    public bool entered = false;
    public float charge=1000f;
    GameObject player;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update () {
		if(entered)
        {
            player.GetComponentInChildren<EnergyController>().FixArmor(0.5f * Time.deltaTime);
            if (charge >= 10 * Time.deltaTime)
                player.GetComponentInChildren<EnergyController>().AddEnergy(10 * Time.deltaTime);
                 charge -= 10 * Time.deltaTime;
            
        }
	}

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            entered = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            entered = false;
        }
    }

}
