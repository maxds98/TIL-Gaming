using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour {

    public float energyCount=100f;
    float x=0;
    public float consumption=0.2f;
    public int batteryCount = 1;
    int bites = 0;
    public float damage = 0;
    public List<GameObject> battareys;

    SpriteRenderer sriteRenderer;

    // Use this for initialization
    void Start () {
        sriteRenderer = GetComponent<SpriteRenderer>();

    }

    void UpdateCount()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i > batteryCount - 1)
                battareys[i].SetActive(false);
            else battareys[i].SetActive(true);

        }
    }

    // Update is called once per frame
    void Update () {
                    UpdateCount();
                  transform.localScale = new Vector3(energyCount / 100f, 1, 1);
                  sriteRenderer.color = Color.Lerp(Color.yellow, Color.cyan, energyCount / 100f);

                  energyCount -= consumption * Time.deltaTime;

        if (energyCount < 0)
            GameObject.Destroy(GameObject.Find("Player"));

        if(Input.GetKeyDown(KeyCode.Space))
            {
            if(batteryCount!=0)
            {
                batteryCount--;
                energyCount += 20;
                if (energyCount > 100)
                    energyCount = 100;
            }
          }

        }

    public void AddConsumption(float cons)
    {
        consumption += cons;
    }

    public void RemoveConsumption(float cons)
    {
        consumption -= cons;
    }
    
    public float TakeEnergy()
    {
        if (energyCount > 0)
        {
            energyCount -= 4 * Time.deltaTime;
        }
        if (energyCount < 0)
            energyCount = 0;
        return 4 * Time.deltaTime;
    }
    public void Bite()
    {
        bites++;
        if (bites == 10)
        {
            damage++;
            bites = 0;
            AddConsumption(1f);
        }
    }

    public void AddEnergy(float f)
    {
        energyCount += f;
        if (energyCount > 100)
          energyCount = 100;
    }
   
    public void FixArmor(float f)
    {
        consumption -= damage;
    
        damage = 0;
        //if(damage>f)
        //{
        //    damage -= f;
        //    consumption -= f;
        //}
        //else if(damage>0)
        //{
        //    consumption -= damage;
        //    consumption = 0;
        //}
    }

    public bool AddBattery()
    {
        if (batteryCount == 3)
            return false;
        else batteryCount++;
        return true;
    }
}
