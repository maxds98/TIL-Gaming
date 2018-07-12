using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public int countOfGenerators=3;
    public int LaunchedGenerators = 0;
    public GameObject finalDoor;
    public bool exit;
    public List<GameObject> listofCFinalDoors;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Launched()
    {
        LaunchedGenerators++;
        if (LaunchedGenerators == countOfGenerators)
            exit = true;

        if (exit)
        {
            foreach(GameObject go in listofCFinalDoors)
            {
                go.GetComponent<DoorController>().enabled = true;
                go.GetComponent<BoxCollider2D>().enabled = true;

            }

        }

    }
}
