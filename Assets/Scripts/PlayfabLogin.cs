using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEditor.PackageManager;
using System.Linq;

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
        SetUserData(result.PlayFabId);
        //MakePurchase();
        GetInventory();
    }

    private void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result => ShowInventory(result.Inventory), OnLoginError);
    }

    private void ShowInventory(List<ItemInstance> inventory)
    {
        var firstItem = inventory.First();
        Debug.Log(firstItem.ItemInstanceId);
        ConsumePotion(firstItem.ItemInstanceId);
    }

    private void ConsumePotion(string itemInstanceId)
    {
        PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest
        {
            ConsumeCount = 1,
            ItemInstanceId = itemInstanceId
        }, result =>
        {
            Debug.Log("ConsumePotion");
        }, OnLoginError);
    }

    private void MakePurchase()
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
        {
            CatalogVersion = "1",
            ItemId = "health_potion",
            Price = 3,
            VirtualCurrency = "SC"
        }, result =>
        {
            Debug.Log("Complete MakePurchase");
        }, OnLoginError);
    }

    private void SetUserData(string playFabId)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"time_recieve_daily_reward", DateTime.UtcNow.ToString() }
            }
        },
        result =>
        {
            Debug.Log("SetUserData");
            GetUserData(playFabId, "time_recieve_daily_reward");
        }, OnLoginError);
    }

    private void GetUserData(string playFabId, string keyData)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = playFabId
        }, result=>
        {
            if (result.Data.ContainsKey(keyData))
            Debug.Log($"{keyData}: {result.Data[keyData].Value}");
        }, OnLoginError);
    }
}
