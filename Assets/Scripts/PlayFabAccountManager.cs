using PlayFab;
using TMPro;
using UnityEngine;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerInfo;
    [SerializeField] private TMP_Text itemsInfo;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalog, OnError);
    }


    private void OnGetCatalog(GetCatalogItemsResult result)
    {
        Debug.Log("OnGetCatalogSuccess");
        ShowItems(result.Catalog);
    }

    private void ShowItems(List<CatalogItem> catalog)
    {
        foreach (var item in catalog)
        {
            itemsInfo.text += "\n" + item.ItemId;
        }
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        playerInfo.text = $"Playfab id: {result.AccountInfo.PlayFabId} \n" +
            $"Username: {result.AccountInfo.Username}\n" +
            $"GameCenter info: {result.AccountInfo.GameCenterInfo}";
    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.Log(errorMessage);
    }
}
