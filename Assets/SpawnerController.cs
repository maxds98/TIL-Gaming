using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public float timer =0;
    public Animator Bot;
    public Animator Middle;
    public Animator Top;
    public float timeToSpawn = 5f;
    public float health=200;
    public GameObject ps;
    public int maxBugs = 10;
    public bool dead = false;
    AudioSource audio;
    public float radOfSearch = 20f;

    private void Start()
    {
    }
    void Update () {
        if (!dead)
        {
            timer += Time.deltaTime;

            if (timer >= timeToSpawn)
            {
                int x = 0;
                Collider2D[] coll = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1000);
                foreach (Collider2D c in coll)
                {
                    if (c.tag == "Bug" && c.gameObject != gameObject && !c.isTrigger)
                    {
                        x++;

                    }

                }

                if (x > maxBugs)
                {
                    timer = 0;
                    return;
                }

                Middle.SetTrigger("Born");
                Top.SetTrigger("Born");
                timer = 0;
            }
        }
    }

    public void Damage(float count)
    {
        if (!dead)
        {
                Middle.SetTrigger("Take damage");
                Top.SetTrigger("Take damage");

            if (health > 0)
                health -= count;

            if (health <= 0)
            {
                health = 0;
                Death();
                dead = true;
            }
        }
    }

    void Death()
    {
        Top.SetTrigger("Dead");

       // GameObject.Destroy(gameObject);
    }
}
