using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawnerController : MonoBehaviour {
    public GameObject Neck;
    public GameObject Target;
    public GameObject Alienz;
    public GameObject ps;
    public Animator Middle;
     AudioSource au;
    private void Start()
    {
        au = GetComponent<AudioSource>();
       }
    public void Miracle()
    {
        GameObject.Instantiate(Alienz, Target.transform.position, Quaternion.identity);
        au.Play();
    }

    public void Bang()
    {
        GameObject gm = Instantiate(ps, new Vector3(Neck.transform.position.x, Neck.transform.position.y, -8), Quaternion.identity);
        gm.transform.parent = transform;

    }

    public void Gang()
    {
        Middle.SetTrigger("Dead");
        GameObject.Destroy(gameObject);
    }
  
}
