using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {

    public bool EnabledGenerator = false;
    public float energyCount = 0;
    public float maxEnergy = 20;
    public GameObject energy;
    public Animator piston1;
    public Animator piston2;
    public Animator piston3;
    public Animator EnergyIndicator;
    public Animator Wheel;
    public AudioClip startAudio;
    public AudioClip continueAudio;
    public bool entered=false;


    // Use this for initialization

    void Start () {
             piston1.speed=0;
             piston2.speed = 0;
             piston3.speed = 0;
             EnergyIndicator.speed = 0;
             Wheel.speed = 0;
        energy.transform.localScale = new Vector3(1, energyCount / maxEnergy, 1);

    }

    // Update is called once per frame
    void Update () {
        if(entered)
        {
            energy.transform.localScale = new Vector3(1, energyCount / maxEnergy, 1);
            if (energyCount < maxEnergy)
                energyCount += GameObject.Find("Player").GetComponentInChildren<EnergyController>().TakeEnergy();
            else
            {
                if(!EnabledGenerator)
                {
                    EnabledGenerator = true;
                    piston1.speed = 1;
                    piston2.speed = 1;
                    piston3.speed = 1;
                    EnergyIndicator.speed = 1;
                    Wheel.speed = 1;

                    GameObject.Find("Main Controller").GetComponent<MainController>().Launched();


                    StartCoroutine(Started());
                }
            }

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

   

    IEnumerator Started()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = startAudio;
        audio.Play();
        yield return new WaitForSeconds(startAudio.length);
        audio.clip = continueAudio;
        audio.loop = true;
        audio.Play();
    }
}
