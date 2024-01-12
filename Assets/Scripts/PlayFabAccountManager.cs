using PlayFab;
using TMPro;
using UnityEngine;
using PlayFab.ClientModels;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text titleLabel;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
    }


    private void OnGetAccount(GetAccountInfoResult result)
    {
        titleLabel.text = $"Playfab id: {result.AccountInfo.PlayFabId} \n" +
            $"Username: {result.AccountInfo.Username}\n" +
            $"GameCenter info: {result.AccountInfo.GameCenterInfo}";
    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.Log(errorMessage);
    }
}
