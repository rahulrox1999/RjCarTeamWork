using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    public RoomItem roomItemPrefab;
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    
    List<RoomItem> roomItemsList = new List<RoomItem>();
    List<RoomItem> roomItemsList1 = new List<RoomItem>();
    public Transform contentObject;
    
    public float timeBetweenUpdates = 0.5f;
    float nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemsPrefab;
    public Transform playerItemsParent;

    public GameObject playButton;


    private void Start()
    {
        //UpdateRoomList(OnRoomListUpdate);
        PhotonNetwork.JoinLobby();
    }


    public void OnClickConnect()
    {
        if(roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){MaxPlayers = 5, BroadcastPropsChangeToAll = true});
        }
    }


    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "ROOM NAME IS :" +PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
        
    }


       void UpdateRoomList(List<RoomInfo> list)
       {
           foreach (RoomItem item in roomItemsList)
           {
               Destroy(item.gameObject);
           }
           roomItemsList.Clear();

           foreach (RoomInfo room in list)
           {
               RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
               newRoom.SetRoomName(room.Name);
               roomItemsList.Add(newRoom);
           }
       }


       public void JoinRoom(string roomName)
       {
           PhotonNetwork.JoinRoom(roomName);
       }

       public void OnClickLeaveRoom()
       {
           PhotonNetwork.LeaveRoom();
       }

       public void OnClickRefresheRoom()
       {
            //UpdateRoomList(roomItemsList);
       }

      

       public override void OnLeftRoom()
       {
           roomPanel.SetActive(false);
           lobbyPanel.SetActive(true);
       }

       public override void OnConnectedToMaster()
       {
           PhotonNetwork.JoinLobby();
       }


       void UpdatePlayerList()
       {
           foreach (PlayerItem item in playerItemsList)
           {
               Destroy(item.gameObject);
           }
           playerItemsList.Clear();

           if (PhotonNetwork.CurrentRoom == null)
           {
               return;
           }

           foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
           {
             PlayerItem newPlayerItem = Instantiate(playerItemsPrefab, playerItemsParent);
             newPlayerItem.SetPlayerInfo(player.Value);

             if (player.Value == PhotonNetwork.LocalPlayer)
             {
                 newPlayerItem.ApplyLocalChanges();
             }

             playerItemsList.Add(newPlayerItem);
           }
       }

       public override void OnPlayerEnteredRoom(Player newPlayer)
       {
           UpdatePlayerList();
       }

       public override void OnPlayerLeftRoom(Player otherPlayer)
       {
           UpdatePlayerList();
       }

       private void Update()
       {
           if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >=1)
           {
               playButton.SetActive(true);
           }
           else
           {
               playButton.SetActive(false);
           }
       }


       public void OnClickPlayButton()
       {
           PhotonNetwork.LoadLevel("Test2");
       }


    
}
