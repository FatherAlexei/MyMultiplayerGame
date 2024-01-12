using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField] private InputField mailField;
    [SerializeField] private Button createAccountButton;

    private string mail;

    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();

        mailField.onValueChanged.AddListener(UpdateMail);
        createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest{
            Username = username,
            Email = mail,
            Password = password
        }, result =>
        {
            Debug.Log("Success: " + username);
            EnterInGameScene();
        }, error => {  
            Debug.Log("Fail: " + error.ErrorMessage);
        }
        );
    }

    private void UpdateMail(string _mail)
    {
        mail = _mail;
    }
}
