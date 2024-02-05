using PlayFab;
using TMPro;
using UnityEngine;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerInfo;
    [SerializeField] private TMP_Text itemsInfo;
    [SerializeField] private GameObject newCharacterPanel;
    [SerializeField] private Button createCharacterButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] List<SlotCharacterWidget> slots;
    private string characterName;



    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalog, OnError);


        GetCharacters();
        foreach (var slot in slots)
            slot.SlotButton.onClick.AddListener(OpenCreateNewCharacter);

        inputField.onValueChanged.AddListener(OnNameChanged);
        createCharacterButton.onClick.AddListener(CreateCharacter);
    }

    private void CreateCharacter()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            CharacterName = characterName,
            ItemId = "character_token"
        }, result =>
        {
            Debug.Log("Complete CreateCharacter");
            UpdateCharacterStatistics(result.CharacterId);
        }, OnError);
    }

    private void UpdateCharacterStatistics(string characterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest
        {
            CharacterId = characterId,
            CharacterStatistics = new Dictionary<string, int>
            {
                {"Damage", 1},
                {"Health", 100 },
                {"Experience", 0}
            }
        }, result =>
        {
            Debug.Log("Complete UpdateCharacterStatistics");
            CloseCreateNewCharacter();
            GetCharacters();
        }, OnError );
    }

    private void OnNameChanged(string changeName)
    {
        characterName = changeName;
    }

    private void OpenCreateNewCharacter()
    {
        newCharacterPanel.SetActive(true);
    }

    private void CloseCreateNewCharacter()
    {
        newCharacterPanel.SetActive(false);
    }

    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
        result =>
        {
            Debug.Log("Characters count :" + result.Characters.Count);
            ShowCharacterInSlot(result.Characters);
        }, OnError ); 
    }

    private void ShowCharacterInSlot(List<CharacterResult> characters)
    {
        if(characters.Count == 0)
        {
            foreach(var slot in slots)
                slot.ShowEmptySlot();
        }
        else if(characters.Count > 0 && characters.Count < 5)
        {
            PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest
            {
                CharacterId = characters.First().CharacterId
            }, result =>
            {
                Debug.Log("Characters count :");
                var damage = result.CharacterStatistics["Damage"].ToString();
                var health = result.CharacterStatistics["Health"].ToString();
                var expirience = result.CharacterStatistics["Experience"].ToString();

                slots.First().ShowInfoCharacterSlot(characters.First().CharacterName, damage, health, expirience);
            }, OnError);
        }
        else
        {
            Debug.Log("Add slots for characters!");
        }
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
