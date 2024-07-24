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
            player = GameObject.FindGameObjectWithTag("Player").transform;  //Sürüklemeden bir nesneyi bulup konumunu almak için!!! Bildiðim þekilde yalnýzca prefablarý sürüklüyordum. (sabit 0 konum)
                                                                                                                // Bunda direkt olarak hareket eden nesneyi aliyordu.
        }
    }


    void Update()
    {
        
        deger = transform.position.z - player.transform.position.z; //player pozisyonunu hep 0 aliyor!!! -> prefab oldugu icin nasýl çözülecek >> Yukarýda çözüm.
        if(deger < -10 )
        {
            //Debug.Log("SÝLÝNDÝ");
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log(transform.position.z +"VE" + player.transform.position.z); -> Player konum sabit = 0
        }
    }
}
