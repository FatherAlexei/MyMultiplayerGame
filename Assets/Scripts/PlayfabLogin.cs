using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEditor.PackageManager;

public class PlayfabLogin : MonoBehaviour
{
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = "6EAD7";

        LogIn();
    }

    public void LogIn()
    {
        var needCreation = PlayerPrefs.HasKey(StaticClass.AuthGuidKey);
        var id = PlayerPrefs.GetString(StaticClass.AuthGuidKey, Guid.NewGuid().ToString());

         var request = new LoginWithCustomIDRequest
         {
             CustomId = id,
             CreateAccount = true
         };

         PlayFabClientAPI.LoginWithCustomID(request,
             result => {
                 PlayerPrefs.SetString(StaticClass.AuthGuidKey, id);
                 OnLoginSucces(result);
                 }, OnLoginError);
    }

    public void OnLoginError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.Log(errorMessage);
    }

    private void OnLoginSucces(LoginResult result)
    {
        Debug.Log("COmplete Login!");
    }

}
