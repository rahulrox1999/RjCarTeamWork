using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeDamege : MonoBehaviour
{

    public float Health = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Takedamege(float amount)
    {
        Health -= amount;

        if(Health <= 0f)
        {
            Die();
        }

        void Die()
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
