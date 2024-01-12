using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : MonoBehaviour
{
    [SerializeField] TMP_Text loadingText;
    [SerializeField] Button[] accept;

    private void Start()
    {
        foreach (var item in accept)
        {
            item.onClick.AddListener(ShowLoading);
        }
    }

    void ShowLoading()
    {
        loadingText.text = "Loading...";
    }

    private void OnDestroy()
    {
        foreach (var item in accept)
        {
            item.onClick.RemoveAllListeners();
        }
    }
}
