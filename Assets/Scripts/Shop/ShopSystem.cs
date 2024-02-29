using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private NeedsBar needsBar;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private UIManager uiManager;

    // UI elements for the shop
    [SerializeField] private Button buyFoodButton;
    [SerializeField] private Button buyWaterButton;
    [SerializeField] private Button buyToyButton;

    private void Start()
    {
        buyFoodButton.onClick.AddListener(() => PurchaseItem("food"));
        buyWaterButton.onClick.AddListener(() => PurchaseItem("water"));
        buyToyButton.onClick.AddListener(() => PurchaseItem("toy"));
    }

    private void PurchaseItem(string itemType)
    {
        if (currencyManager.SpendCoins(10)) // Assume each item costs 10 coins
        {
            // Update the call to AddItem to include the quantity parameter
            inventorySystem.AddItem(itemType, 1); // Now passing 1 as the quantity
            uiManager.UpdateButtonInteractivity();
            uiManager.UpdateInventoryCounts();
        }
        else
        {
            Debug.Log("Not enough coins to purchase.");
        }
    }
}