using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public homeScript home;

    //Kamera Takip

    [SerializeField] public GameObject player; 

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(0,2.5f, player.transform.position.z - 3.5f);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y+2, player.transform.position.z-3);


    }
}
