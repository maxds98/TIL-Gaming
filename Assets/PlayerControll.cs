using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour {

    public bool walk = false;
    public float speed = 1f;
    public float runSpeed = 2f;
    public bool running=false;
    Rigidbody2D rb2d;
    Animator animator;
    public Animator animatorLegs;
    public GameObject legs;
    public float realAngle;
    GameObject energy;
    EnergyController energyController;
    AudioSource audioSource;
    public List<AudioClip> listIfClips;
    System.Random r;
    public AudioClip runningClip;
    // Use this for initialization
    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Time.fixedDeltaTime = 0.01f;
        energy = GameObject.Find("Energy");
        energyController = energy.GetComponent<EnergyController>();
        audioSource = GetComponent<AudioSource>();
        r = new System.Random();
        
    }
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
        Vector3 objpos = Camera.main.WorldToViewportPoint(transform.position);        //Object position on screen
        Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
        Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
        float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
        if (relmousepos.x > 0)
            angle = 360 - angle;
        Quaternion quat = Quaternion.identity;
        quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
        transform.rotation = quat;


       // listIfClips = new List<AudioClip>();
    }

    private void LateUpdate()
    {
           //var mousePosition = Input.mousePosition;
            ////mousePosition.z = transform.position.z - Camera.main.transform.position.z; // это только для перспективной камеры необходимо
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
            // angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
            ////if (transform.position.y > mousePosition.y) angle = 360 - angle;
            //// transform.eulerAngles = Vector3.Lerp(new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle + 90 : -angle + 90), transform.eulerAngles,0.5f);//немного магии на последок
            //transform.eulerAngles = Vector3.Lerp(new Vector3(0f, 0f, angle), transform.eulerAngles, 0.5f);//немного магии на последок

    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (move.x > 0)
        {
            if (move.y > 0)
                legs.transform.eulerAngles = new Vector3(0, 0, 135);
            else if (move.y < 0)
                legs.transform.eulerAngles = new Vector3(0, 0, 45);
            else legs.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (move.x < 0)

        {
            if (move.y > 0)
                legs.transform.eulerAngles = new Vector3(0, 0, 225);
            else if (move.y < 0)
                legs.transform.eulerAngles = new Vector3(0, 0, 315);
            else legs.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else
        {
            if(move.y>0)
                legs.transform.eulerAngles = new Vector3(0, 0, 180);
            else legs.transform.eulerAngles = new Vector3(0, 0, 0);

        }

        if (move.x != 0 || move.y != 0)
        {
            if (!walk)
                walk = true;
            animator.SetBool("Walk", true);
            animatorLegs.SetBool("Walk", true);

        }
        else
        {
            if (walk)
                walk = false;
            animatorLegs.SetBool("Walk", false);
            animator.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.LeftShift)&&(move.x!=0||move.y!=0))
        {
            if (!running)
            {
                energyController.AddConsumption(0.2f);
                animator.speed = 3;
                animatorLegs.speed = 3;

            }
            running = true;            

        }
        else
        {
            if (running)
            {
                animator.speed = 2;
                animatorLegs.speed = 2;

                energyController.RemoveConsumption(0.2f);
            }
            running = false;
        }

        if (!running)
        transform.position += move * speed*Time.deltaTime;
        else transform.position += move * runSpeed * Time.deltaTime;

        if (walk||running)
        {
            audioSource.mute = false;


            if (!audioSource.isPlaying|| (!running && audioSource.clip == runningClip)||(running && audioSource.clip != runningClip))
            {
                if (!running)
                    audioSource.clip = listIfClips[r.Next(listIfClips.Count)];
                else audioSource.clip = runningClip;
                audioSource.Play();
            }
        }
        else
            if (audioSource.isPlaying)
        {
            audioSource.mute = true;
        }

       
        //Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float x0 = transform.position.x;
        //float y0 = transform.position.y;


        //float x1 = v3.x;
        //float y1 = v3.y;

        //float dist = Mathf.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));

        //float angle = Mathf.Abs(Mathf.Atan((y1 - y0) / (x1 - x0)) * (180 / Mathf.PI));


        //if (x1 > x0)
        //{
        //    if (y1 > y0)
        //        realAngle = angle;
        //    else realAngle = 360 - angle;
        //}
        //else
        //{
        //    if (y1 > y0)
        //        realAngle = 180 - angle;
        //    else
        //        realAngle = 180 + angle;
        //}
        //transform.eulerAngles = new Vector3(0, 0, realAngle + 90);
    }
    
    IEnumerator stopAudio()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.mute = true;
    }

}
