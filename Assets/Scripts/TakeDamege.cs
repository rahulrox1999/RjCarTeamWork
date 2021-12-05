using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeDamege : MonoBehaviour
{

    public float Health = 100f;

     PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void Takedamege(float amount)
    {
        if (PhotonNetwork.isMine == true && PhotonNetwork.IsConnected == true)
        {
            Health -= amount;

            if (Health <= 0f)
            {
                PhotonNetwork.Destroy(gameObject);
            }

            
        }
        
    }
}
