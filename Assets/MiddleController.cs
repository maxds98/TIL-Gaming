using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleController : MonoBehaviour {

    public GameObject Neck;

    public GameObject ps;
    public Animator Bottom;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Bang()
    {
        GameObject gm = Instantiate(ps, new Vector3(Neck.transform.position.x, Neck.transform.position.y, -8), Quaternion.identity);

        gm.transform.parent = transform;

    }

    public void Gang()
    {
        Bottom.SetTrigger("Dead");
        GameObject.Destroy(gameObject);
    }

}
