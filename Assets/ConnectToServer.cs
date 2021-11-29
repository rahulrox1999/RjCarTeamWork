using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttonText;

    public void OnClickConnect()
    {
        if(usernameInput.text.Length >=1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting now";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();

        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
        Debug.Log(PhotonNetwork.NickName = " Connected to Photon");

    }

}
