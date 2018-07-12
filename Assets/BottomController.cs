using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomController : MonoBehaviour {

    public GameObject Neck;
    public GameObject ps;
    public GameObject sprite;
    GameObject gm;


    public void Bang()
    {
         gm = Instantiate(ps, new Vector3(Neck.transform.position.x, Neck.transform.position.y,-8), Quaternion.identity);
        gm.transform.parent = transform;

    }

    public void Gang()
    {
        GameObject.Destroy(gm);

    }
}
