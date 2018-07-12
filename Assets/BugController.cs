using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour {

    public Transform target;
    public float moveSpeed = 10f;
    public float speedOfRotate = 3f;
    public GameObject fieldOfView;
    public bool foundTarget;
    public float agroTime=3f;
    DynamicLight view;
    public float timer = 0;
    public bool chase = false;
    public GameObject dummyGO;
    public float temp = 0;
    System.Random r;
    public float RangeOfSearch = 10f;
    public float biteTime = 0f;
    public float chaseSpeed=15;
    GameObject dummy;
    public List<AudioClip> listOfClips;
    AudioSource audioSource;
    public float health = 100;
    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        view = fieldOfView.GetComponent<DynamicLight>();
        view.InsideFieldOfViewEvent += found;        
        r = new System.Random();
        FindNewWay();
        dummy = new GameObject();
        dummy.transform.parent = gameObject.transform;
        dummy.name = "dummy";
        audioSource = GetComponent<AudioSource>();
        temp = 0;
        rb2d = GetComponent<Rigidbody2D>();
    }

   // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        temp += Time.deltaTime;

        if (timer > 1)
        {
            if (dummy != null)
                if (foundTarget)
                    target = dummy.transform;
            foundTarget = false;
        }
        if (timer > 6)
        {
            if (chase)
                FindNewWay();

            chase = false;
        }
        //Debug.Log(temp);
        if (!chase)
        {
            //    if (target.position == transform.position)
            //        FindNewWay();

            if (temp > 5f)
            {
                FindNewWay();
                temp = 0;

            }
        }

        if (target != null)
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedOfRotate);

            if (!chase)
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.position, moveSpeed * Time.deltaTime);
            else transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.position, chaseSpeed * Time.deltaTime);

            //if (!chase)
            //   rb2d.velocity = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.position, moveSpeed * Time.deltaTime);
            //else rb2d.velocity = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.position, chaseSpeed * Time.deltaTime);
        }
    }

    void FindNewWay()
    {
        if (!chase)
        {
            Collider2D[] coll = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), RangeOfSearch);
            List<Collider2D> listOfColl = new List<Collider2D>();
            foreach (Collider2D c in coll)
            {
                if (c.tag == "WayPoint")
                    listOfColl.Add(c);
            }
            if (listOfColl.Count > 0)
            {
                int x = Random.Range(0, listOfColl.Count - 1);
                target = listOfColl[x].transform;
            }
        }
    }

    void found(GameObject[] g)
    {
        foreach (GameObject go in g)
        {
            if (go.tag == "Player")
            {
                if (!foundTarget)
                   StartCoroutine(Anonce());
                foundTarget = true;
                timer = 0f;
                target = go.transform;
                chase = true;
                dummy.transform.position = target.position;
                break;
            }

        }
        // target = dummy;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            biteTime += Time.deltaTime;
            if (biteTime > 1)
            {
                audioSource.clip = listOfClips[r.Next(listOfClips.Count)];
                audioSource.Play();
                collision.GetComponentInChildren<EnergyController>().Bite();

                biteTime = 0;

            }
        }
    }


    public IEnumerator Anonce()
    {
        GameObject amb = GameObject.Find("Bugs Ambient");

        yield return new WaitForSeconds(.5f);
        //Debug.Log("kek");
        int x = 0;

        Collider2D[] coll = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 30);
        foreach (Collider2D c in coll)
        {
            if (c.tag == "Bug" && c.gameObject != gameObject && !c.isTrigger)
            {
                c.gameObject.GetComponent<BugController>().GetSignal(target);
                x++;
            }
           
        }
        //if (x > 5)
        //{
        //    amb.transform.position = transform.position;
        //    amb.GetComponent<AudioSource>().Play();

        //}
        //else amb.GetComponent<AudioSource>().Stop();
    }

    public void GetSignal(Transform tg)
    {

        if (!foundTarget&&tg!=null)
        {

            
            GameObject[] g = new GameObject[] { tg.gameObject };
            //foundTarget = true;
            //timer = 0f;
            //target = tg;
            //chase = true;          
            found(g);

            //yield return new WaitForSeconds(1);

            //Anonce();
        }
    }

    public void Damage(float count)
    {
        if (health > 0)
            health -= count;

        if (health <= 0)
        {
            health = 0;
            Death();
        }

    }

    void Death()
    {
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        GetComponents<AudioSource>()[1].Play();
        this.enabled = false;
    }
     void LeshaHuyosha()
    {
        GameObject.Destroy(gameObject);
    }
}
