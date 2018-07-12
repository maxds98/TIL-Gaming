using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CapsuleController : MonoBehaviour {
    public Animator doors;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            doors.SetTrigger("Lock");
            doors.GetComponent<BoxCollider2D>().enabled = true;

            SceneManager.LoadScene("Menu");
        }

    }
}
