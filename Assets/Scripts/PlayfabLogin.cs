using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEditor.PackageManager;

public class PlayfabLogin : MonoBehaviour
{
    public MyUi myUiInstance;
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = "6EAD7";
    }

    public void LogIn()
    {

         var request = new LoginWithCustomIDRequest
         {
             CustomId = "Player 1",
             CreateAccount = true
         };

         PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnLoginError);
    }

    public void OnLoginError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.Log(errorMessage);
        myUiInstance.ChangeBGColor(false);
    }

    private void OnLoginSucces(LoginResult result)
    {
        Debug.Log("COmplete Login!");
        myUiInstance.ChangeBGColor(true);
    }

}
