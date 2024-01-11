using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Launcher launcher;

    private void Start()
    {
        button.onClick.AddListener(ButtonClickHandler);
    }

    private void ButtonClickHandler()
    {
        if (PhotonNetwork.IsConnected)
        {
            button.GetComponent<Image>().color = Color.red;
            launcher.Disconnect();
        }
        else
        {
            button.GetComponent<Image>().color = Color.green;
            launcher.Connect();
        }
    }
}
