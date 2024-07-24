using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UIElements;


public class Random : MonoBehaviour
{

    [SerializeField] public GameObject trapObject;
    [SerializeField] public GameObject coinObject;
    [SerializeField] public GameObject buildObject;

    public float timerSec;
    private float timer;

    void Start()
    {
        timerSec = 5f;

        System.Random rand = new System.Random();

        for (int i = 0; i < GenerateRandomValue(rand); i++)
        {
            Instantiate(coinObject, new Vector3(RandomCoin(rand), RandomCoinHigh(rand), transform.position.z + UnityEngine.Random.Range(5f, 15f) * 2), transform.rotation);
            Instantiate(trapObject, new Vector3(RandomTrap(rand), RandomTrapHigh(rand), transform.position.z + UnityEngine.Random.Range(5f, 15f) * 2), transform.rotation);
            Instantiate(buildObject, new Vector3(RandomBuilding(rand), RandomBuildingHigh(rand), transform.position.z + UnityEngine.Random.Range(5f, 15f) * 2), transform.rotation);
        }

    }

    void Update()
    {
        if (Controller.Instance.isFailed) 
        {
            return;
        }

        //timer: 5s de bir 5-10 arasý coin -   100-250 birim uzaklýkta playerýn önüne + timer 0lama
        //player: transform.position.z + random.Next(100, 251); 

        timer += Time.deltaTime*Controller.Instance.acc;

        if (timer >= timerSec) //5s de bir
        {

            //random üretilecek coin ve trap sayisi   
            System.Random rand = new System.Random();

            for (int i = 0; i < GenerateRandomValue(rand); i++)  //GenerateRandomValue(rand)
            {
                Instantiate(coinObject, new Vector3(RandomCoin(rand), RandomCoinHigh(rand), transform.position.z + UnityEngine.Random.Range(15f, 20f)*Controller.Instance.acc), transform.rotation);
                Instantiate(trapObject, new Vector3(RandomTrap(rand), RandomTrapHigh(rand), transform.position.z + UnityEngine.Random.Range(15f, 20f) * Controller.Instance.acc), transform.rotation);
                Instantiate(buildObject, new Vector3(RandomBuilding(rand), RandomBuildingHigh(rand), transform.position.z + UnityEngine.Random.Range(15f, 20f) * 2), transform.rotation);
            }

            timer = 0f;//tekrar sifirla
        }
    }

    int GenerateRandomValue(System.Random rand)
    {
        int[] values = { 4, 5, 6};
        return values[rand.Next(values.Length)];
    }

    int RandomCoin(System.Random rand)
    {
        int[] values = { -1, 0, 1 };
        return values[rand.Next(values.Length)];
    }

    float RandomCoinHigh(System.Random rand)
    {
        float[] values = { 0f, 0.75f };
        return values[rand.Next(values.Length)];
    }

    float RandomTrap(System.Random rand)
    {
        float[] values = { -1.25f, 0f, 1.25f };
        return values[rand.Next(values.Length)];
    }

    float RandomTrapHigh(System.Random rand)
    {
        float[] values = { 0, 0.5f};
        return values[rand.Next(values.Length)];
    }

    float RandomBuilding(System.Random rand)
    {
        float[] values = { -3f , 3f };
        return values[rand.Next(values.Length)];
    }

    float RandomBuildingHigh(System.Random rand)
    {
        float[] values = { -2f, -1.5f };
        return values[rand.Next(values.Length)];
    }

}
