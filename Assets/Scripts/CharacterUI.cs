using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    // Health bars
    [SerializeField] private Slider char1HeathBar;
    [SerializeField] private Slider char2HeathBar;

    [SerializeField] private GameObject char1Icon;
    [SerializeField] private GameObject char2Icon;

    void Start()
    {
        char1HeathBar.maxValue = player.GetChar1MaxHealth();
        char2HeathBar.maxValue = player.GetChar2MaxHealth();

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        char1HeathBar.value = player.GetChar1Health();
        char2HeathBar.value = player.GetChar2Health();

        if (char1Icon && char2Icon)
        {
            bool char1Active = player.isChar1Active;

            char1Icon.SetActive(char1Active);
            char2Icon.SetActive(!char1Active);

        }
    }
}
