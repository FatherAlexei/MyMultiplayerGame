using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterGameWindow : MonoBehaviour
{
    [SerializeField] private Button signInButton;
    [SerializeField] private Button createAccountButton;
    [SerializeField] private Canvas enterInGameCanvas;
    [SerializeField] private Canvas createAccountCanvas;
    [SerializeField] private Canvas signInCanvas;

    private void Start()
    {
        signInButton.onClick.AddListener(OpenSignInWindow);
        createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
    }

    private void OpenSignInWindow()
    {
        signInCanvas.enabled = true;
        enterInGameCanvas.enabled = false;
    }

    private void OpenCreateAccountWindow()
    {
        createAccountCanvas.enabled = true;
        enterInGameCanvas.enabled = false;
    }

}
