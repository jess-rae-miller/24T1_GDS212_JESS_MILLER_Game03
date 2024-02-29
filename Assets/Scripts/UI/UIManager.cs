using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private NeedsBar needsBar;

    [Header("Buttons")]
    [SerializeField] private Button feedButton;
    [SerializeField] private Button waterButton;
    [SerializeField] private Button playButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI foodCountText;
    [SerializeField] private TextMeshProUGUI waterCountText;
    [SerializeField] private TextMeshProUGUI toyCountText;

    private void Start()
    {
        feedButton.onClick.AddListener(OnFeedButtonClick);
        waterButton.onClick.AddListener(OnWaterButtonClick);
        playButton.onClick.AddListener(OnPlayButtonClick);

        UpdateButtonInteractivity();
        UpdateInventoryCounts();
    }

    public void UpdateInventoryCounts()
    {
        foodCountText.text = "Food: " + inventorySystem.GetItemCount("food").ToString();
        waterCountText.text = "Water: " + inventorySystem.GetItemCount("water").ToString();
        toyCountText.text = "Toys: " + inventorySystem.GetItemCount("toy").ToString();
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
