using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountDataWindowBase : MonoBehaviour
{
    [SerializeField] private InputField usernameField;
    [SerializeField] private InputField passwordField;

    protected string password;
    protected string username;

    private void Start()
    {
        SubscriptionsElementsUi();
    }

    protected virtual void SubscriptionsElementsUi()
    {
        usernameField.onValueChanged.AddListener(UpdateUsername);
        passwordField.onValueChanged.AddListener(UpdatePassword);
    }

    private void UpdatePassword(string _password)
    {
        password = _password;
    }

    private void UpdateUsername(string _username)
    {
        username = _username;
    }

    protected void EnterInGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
