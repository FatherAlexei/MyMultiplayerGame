using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class SignInWindow : AccountDataWindowBase
{
    [SerializeField] private Button signInButton;

    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();

        signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        }, result =>
        {
            Debug.Log("Success: " + username);
            EnterInGameScene();
        }, error => {
            Debug.Log("Fail: " + error.ErrorMessage);
        });
    }
}
