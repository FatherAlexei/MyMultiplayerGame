using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyUi : MonoBehaviour
{
    [SerializeField] PlayfabLogin playfabLogin;
    [SerializeField] TextMeshProUGUI username;
    [SerializeField] Button button;

    private void Start()
    {
    }
    public void LogIn()
    {
        if (username.text == "Player 1")
        {
            playfabLogin.LogIn();
        }
        else
        {
            Debug.Log("Wrong Name");
            ChangeBGColor(false);
        }
    }

    public void ChangeBGColor(bool result)
    {
        if(result)
            button.GetComponent<Image>().color = Color.green;
        else
            button.GetComponent<Image>().color = Color.red;
    }
}
