using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmutterController : MonoBehaviour {

    public Animator animator;
    GameObject energy;
    EnergyController energyController;
    public bool fire = false;
    public float damage=100;
    public AudioClip audioClipIdle;
    public AudioClip audioClipFire;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        energy = GameObject.Find("Energy");
        energyController = energy.GetComponent<EnergyController>();
        audioSource.clip = audioClipFire;
        audioSource.Play();

        // animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            if (!fire)
            {
                energyController.AddConsumption(0.2f);
                fire = true;
                audioSource.clip = audioClipIdle;
                audioSource.Play();
            }

            animator.SetBool("Fire", true);


        }
        else
        {
            if (fire)
            {
                energyController.RemoveConsumption(0.2f);
                fire = false;
                audioSource.clip = audioClipFire;
                audioSource.Play();

            }
            animator.SetBool("Fire", false);
        }


    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (fire)
            if (coll.tag == "Bug")
                coll.GetComponent<BugController>().Damage(damage * Time.deltaTime);
            else if (coll.tag == "Spawner")
                coll.GetComponent<SpawnerController>().Damage(damage * Time.deltaTime);

    }
}
