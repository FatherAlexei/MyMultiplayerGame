using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Launcher : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
            return;

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = Application.version;
        Debug.Log("You have been Connected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.CreateRoom("NewName");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("You have been disconnected");
    }
}
