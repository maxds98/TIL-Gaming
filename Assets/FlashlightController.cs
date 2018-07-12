using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour {
    public bool turned = false;
    public GameObject light;
    GameObject energy;
    EnergyController energyController;

	// Use this for initialization
	void Start () {
        energy = GameObject.Find("Energy");
        energyController = energy.GetComponent<EnergyController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T))
        {
            turned = !turned;
            if (turned)
                energyController.AddConsumption(0.2f);
            else energyController.RemoveConsumption(0.2f);

            light.SetActive(turned);
        }
	}
}
