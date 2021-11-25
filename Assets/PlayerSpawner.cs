using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    //public float minX, minY, maxX, maxY;



    


    // Start is called before the first frame update
    void Start()
    {
        //Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        //PhotonNetwork.Instantiate(playerToSpawn.name,randomPosition, Quaternion.identity);


        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);



        
        


    }



    // Update is called once per frame

    
    
}
