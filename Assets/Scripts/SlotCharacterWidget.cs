using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotCharacterWidget : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject infoCharacterSlot;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text hpLabel;
    [SerializeField] private TMP_Text damageLabel;
    [SerializeField] private TMP_Text expLabel;

    public Button SlotButton => button;

    public void ShowInfoCharacterSlot(string name, string damage, string hp, string exp)
    {
        nameLabel.text = "Name: " + name;
        damageLabel.text = "Damage: " + damage;
        hpLabel.text = "Health: " + hp;
        expLabel.text = "Experience: " + exp;

        infoCharacterSlot.SetActive(true);
        empty.SetActive(false);
    }


    public void ShowEmptySlot()
    {
        infoCharacterSlot.SetActive(false);
        empty.SetActive(true);
    }
}
