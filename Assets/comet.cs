using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class comet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<Animator>().SetTrigger("Go");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
    void fas()
    {
         SceneManager.LoadScene("Learn", LoadSceneMode.Single);
    }
}
