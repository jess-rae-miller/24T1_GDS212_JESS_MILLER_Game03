using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private NeedsBar needsBar;

    [Header("Adventure UI Elements")]
    [SerializeField] private TextMeshProUGUI adventureResultText;
    [SerializeField] private GameObject adventurePopup;

    [Header("Inventory UI Elements")]
    [SerializeField] private TextMeshProUGUI foodCountText;
    [SerializeField] private TextMeshProUGUI waterCountText;
    [SerializeField] private TextMeshProUGUI toyCountText;
    [SerializeField] private TextMeshProUGUI rareItemCountText; // New UI element for rare items

    [Header("Buttons")]
    [SerializeField] private Button startAdventureButton;
    [SerializeField] private Button feedButton; 
    [SerializeField] private Button waterButton;
    [SerializeField] private Button playButton;
    private void Start()
    {
        startAdventureButton.onClick.AddListener(StartAdventure);

        UpdateInventoryCounts();
        adventurePopup.SetActive(false); // Initially hide the adventure result popup
    }

    public void StartAdventure()
    {
        // Assuming AdventureSystem is attached to the same GameObject
        GetComponent<AdventureSystem>().StartAdventure();
    }

    public void UpdateAdventureUI(AdventureResult result)
    {
        adventureResultText.text = $"Adventure Complete!\nCoins: {result.Coins}\nFood: {result.Food}\nWater: {result.Water}\nToys: {result.Toys}\nRare Items: {result.RareItems}";
        adventurePopup.SetActive(true);
    }

    public void UpdateInventoryCounts()
    {
        foodCountText.text = "Food: " + inventorySystem.GetItemCount("food").ToString();
        waterCountText.text = "Water: " + inventorySystem.GetItemCount("water").ToString();
        toyCountText.text = "Toys: " + inventorySystem.GetItemCount("toy").ToString();
        rareItemCountText.text = "Rare Items: " + inventorySystem.GetItemCount("rare").ToString(); // Update rare item count
    }

    private void OnFeedButtonClick()
    {
        if (inventorySystem.UseItem("food"))
        {
            needsBar.FeedThePet();
        }
        else
        {
            Debug.Log("No food items in inventory.");
        }
        UpdateButtonInteractivity();
        UpdateInventoryCounts();
    }

    private void OnWaterButtonClick()
    {
        if (inventorySystem.UseItem("water"))
        {
            needsBar.WaterThePet();
        }
        else
        {
            Debug.Log("No water items in inventory.");
        }
        UpdateButtonInteractivity();
        UpdateInventoryCounts();
    }

    private void OnPlayButtonClick()
    {
        if (inventorySystem.UseItem("toy"))
        {
            needsBar.PlayWithThePet();
        }
        else
        {
            Debug.Log("No toy items in inventory.");
        }
        UpdateButtonInteractivity();
        UpdateInventoryCounts();
    }

    public void UpdateButtonInteractivity()
    {
        feedButton.interactable = inventorySystem.HasItem("food");
        waterButton.interactable = inventorySystem.HasItem("water");
        playButton.interactable = inventorySystem.HasItem("toy");
    }
}
