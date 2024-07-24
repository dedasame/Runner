using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skipped : MonoBehaviour
{
    public Transform player;
    public float deger;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;  //S�r�klemeden bir nesneyi bulup konumunu almak i�in!!! Bildi�im �ekilde yaln�zca prefablar� s�r�kl�yordum. (sabit 0 konum)
                                                                                                                // Bunda direkt olarak hareket eden nesneyi aliyordu.
        }
    }


    void Update()
    {
        
        deger = transform.position.z - player.transform.position.z; //player pozisyonunu hep 0 aliyor!!! -> prefab oldugu icin nas�l ��z�lecek >> Yukar�da ��z�m.
        if(deger < -10 )
        {
            //Debug.Log("S�L�ND�");
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log(transform.position.z +"VE" + player.transform.position.z); -> Player konum sabit = 0
        }
    }
}
