using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public Collider2D collider;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetBool("Enter", true);
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetBool("Enter", false);
            audioSource.Play();

        }
    }

    void DeleteCollider()
    {
        collider.enabled = false;
    }

    void ReturnCollider()
    {
        collider.enabled = true;
    }
}
