using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using Photon.Pun.Demo.PunBasics;

public class DeleteProfileWindow : MonoBehaviour
{
    [SerializeField] private Button delete;

    private void Start()
    {
        delete.onClick.AddListener(DeleteProfile);
    }

    private void DeleteProfile()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        Debug.Log("Account has been deleted");
    }

    private void OnDestroy()
    {
        delete.onClick.RemoveAllListeners();
    }
}
