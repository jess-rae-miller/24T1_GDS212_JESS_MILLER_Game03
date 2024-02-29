using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private NeedsBar needsBar;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI checkHappinessText;

    [Header("Objects")]
    [SerializeField] private Button questButton;
    [SerializeField] private Button completionButton;
    [SerializeField] private GameObject questCanvas;

    void Start()
    {
        questButton.onClick.AddListener(() => ToggleQuestUI(true));
        completionButton.gameObject.SetActive(false);
        completionButton.onClick.AddListener(() => ToggleQuestUI(false));
        checkHappinessText.gameObject.SetActive(false);
    }

    private void ToggleQuestUI(bool checkHappiness)
    {
        if (checkHappiness && needsBar.happiness <= 75)
        {
            checkHappinessText.text = "Your pet isn't happy enough to go on a quest!";
            checkHappinessText.gameObject.SetActive(true);

            StartCoroutine(HideCheckHappinessTextAfterDelay());

            return; // Do not toggle the UI if happiness check fails
        }

        questCanvas.SetActive(!questCanvas.activeSelf);

        if (!questCanvas.activeSelf)
        {
            completionButton.gameObject.SetActive(false);
        }
    }

    IEnumerator HideCheckHappinessTextAfterDelay()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        checkHappinessText.gameObject.SetActive(false);
    }

    public void StartQuest()
    {
        if (needsBar.happiness > 75)
        {
            StartCoroutine(QuestCoroutine());
        }
        else
        {
            resultText.text = "Your pet isn't happy enough to go on a quest!";
        }
    }

    IEnumerator QuestCoroutine()
    {
        questButton.interactable = false; // Disable quest button to prevent re-entry.
        resultText.text = "Quest in progress..."; // Provide immediate feedback that quest has started.

        // Simulate quest time.
        yield return new WaitForSeconds(10);

        // Generate rewards.
        int coinsCollected = Random.Range(30, 251); // Ensure inclusive range for Random.
        int foodCollected = Random.Range(0, 4); // 0-3
        int waterCollected = Random.Range(0, 5); // 0-4
        int toysCollected = Random.Range(0, 3); // 0-2

        // Update inventory and coins.
        currencyManager.EarnCoins(coinsCollected);
        inventorySystem.AddItem("food", foodCollected);
        inventorySystem.AddItem("water", waterCollected);
        inventorySystem.AddItem("toy", toysCollected);

        // Display results.
        resultText.text = $"Quest Complete! Collected: {coinsCollected} coins, {foodCollected} food, {waterCollected} water, {toysCollected} toys.";
        QuestCompleted();
    }
    public void QuestCompleted()
    {
        completionButton.gameObject.SetActive(true);
        questButton.interactable = true;
        uiManager.UpdateInventoryCounts();
    }
}
