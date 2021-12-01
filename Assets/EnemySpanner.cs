using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpanner : MonoBehaviour
{

    public GameObject EnemyPrefabs;
    public Transform[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        PhotonNetwork.Instantiate(EnemyPrefabs.name, spawnPoint.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
