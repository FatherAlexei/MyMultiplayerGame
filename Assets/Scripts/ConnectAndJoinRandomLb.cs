using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRandomLb : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    [SerializeField]private ServerSettings serverSettings;
    [SerializeField]private TMP_Text stateUiText;
    [SerializeField] private TMP_Text serverListText;
    [SerializeField] private Button closeLobby;
    string[] friendIds = new[] { "friendUserId1", "friendUserId2" };
    int maxLevelDifference = 10;
    int playerLevel = 2;
    string playerLevelProp = "PlayerLevel";
    private LoadBalancingClient lbc;
    bool onlyFriends = false;

    private const string GAME_MOD_KEY = "gm";
    private const string AI_MOD_KEY = "ai   ";

    private const string MAP_PROP_KEY = "C0";
    private const string GOLD_PROP_KEY = "C1";

    private TypedLobby sqlLobby = new TypedLobby("customSqlLobby", LobbyType.SqlLobby);

    private void Start()
    {
        closeLobby.onClick.AddListener(CloseLobby);
        lbc = new LoadBalancingClient();
        lbc.AddCallbackTarget(this);

        lbc.ConnectUsingSettings(serverSettings.AppSettings);
    }

    void CloseLobby()
    {
        lbc.CurrentRoom.IsOpen = false;
        lbc.OpSetCustomPropertiesOfRoom(lbc.CurrentRoom.CustomProperties);
    }

    private void OnDestroy()
    {
        lbc.RemoveCallbackTarget(this);
        closeLobby.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (lbc == null)
            return;

        lbc.Service();
        var state = lbc.State.ToString();
        stateUiText.text = state;
    }

    public void OnConnected()
    {
        
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        if (onlyFriends)
        {
            CreateRoomForFriends("New Room", friendIds);
        }
        else
        {
            var roomOptions = new RoomOptions
            {
                MaxPlayers = 12,
                PublishUserId = true,
                CustomRoomPropertiesForLobby = new[] { GAME_MOD_KEY },
                CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { GOLD_PROP_KEY, 400 }, { MAP_PROP_KEY, "MAP3" } }
            };
            var enterRoomsParams = new EnterRoomParams
            {
                RoomName = "new Room",
                RoomOptions = roomOptions,
                ExpectedUsers = new[] { "1488" },
                Lobby = sqlLobby
         
            };

            lbc.OpCreateRoom(enterRoomsParams);
        }
    }

    public void CreateRoomForFriends(string roomName, string[] friendsUserIds)
    {
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 12,
            PublishUserId = true,
            CustomRoomPropertiesForLobby = new[] { GAME_MOD_KEY },
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { GOLD_PROP_KEY, 400 }, { MAP_PROP_KEY, "MAP3" } }
        };

        var enterRoomsParams = new EnterRoomParams
        {
            RoomName = roomName,
            RoomOptions = roomOptions,
            Lobby = sqlLobby,
            ExpectedUsers = friendsUserIds
        };

        lbc.OpCreateRoom(enterRoomsParams);
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        
    }

    public void OnJoinedLobby()
    {
        var sqlLobbyFilter = $"ABS({playerLevelProp} - {playerLevel}) <= {maxLevelDifference}";
        var opOpJoinRandomRoomParams = new OpJoinRandomRoomParams
        {
            SqlLobbyFilter = sqlLobbyFilter
        };
        lbc.OpJoinRandomRoom();
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        lbc.OpCreateRoom(new EnterRoomParams());
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
        lbc.OpCreateRoom(new EnterRoomParams());
    }

    public void OnLeftLobby()
    {
        
    }

    public void OnLeftRoom()
    {
        
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        serverListText.text = "Server List:\n";

        foreach (RoomInfo room in roomList)
        {
            string roomInfo = $"Room Name: {room.Name}, Players: {room.PlayerCount}/{room.MaxPlayers}\n";
            serverListText.text += roomInfo;
        }
    }


}
