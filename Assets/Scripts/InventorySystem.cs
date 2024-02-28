using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private const int maxInventorySize = 12; // Maximum number of items in the inventory

    void Awake()
    {
        inventory["food"] = 2;
        inventory["water"] = 3;
        inventory["toy"] = 1;
        // Initialize rare items with 0 quantity
        inventory["rare"] = 0;
    }

    void Start()
    {
        uiManager.UpdateInventoryCounts();
    }

    public void AddItem(string itemType)
    {
        if (GetTotalItemCount() < maxInventorySize)
        {
            if (inventory.ContainsKey(itemType))
            {
                inventory[itemType]++;
                uiManager.UpdateInventoryCounts();
            }
        }
        else
        {
            Debug.Log("Inventory full. Sell or use items to make space.");
        }
    }

    public void HandleAdventureLoot(AdventureResult result)
    {
        // Ensure adding coins does not depend on the inventory limit
        AddItemSafely("coins", result.Coins);

        // Check and add other items if there's space
        AddItemBasedOnSpace("food", result.Food);
        AddItemBasedOnSpace("water", result.Water);
        AddItemBasedOnSpace("toy", result.Toys);
        AddItemBasedOnSpace("rare", result.RareItems);
    }

    private void AddItemBasedOnSpace(string itemType, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            if (GetTotalItemCount() < maxInventorySize)
            {
                AddItem(itemType);
            }
            else
            {
                Debug.Log($"No more space to add {itemType}. Inventory is full.");
                break;
            }
        }
    }

    private void AddItemSafely(string itemType, int quantity)
    {
        if (!inventory.ContainsKey(itemType))
        {
            inventory[itemType] = 0;
        }
        inventory[itemType] += quantity;
        uiManager.UpdateInventoryCounts();
    }

    public int GetItemCount(string itemType)
    {
        if (inventory.ContainsKey(itemType))
        {
            return inventory[itemType];
        }
        return 0;
    }

    private int GetTotalItemCount()
    {
        int total = 0;
        foreach (var item in inventory)
        {
            total += item.Value;
        }
        return total;
    }

    // Method to check if the player has at least one item of the given type
    public bool HasItem(string itemType)
    {
        if (inventory.ContainsKey(itemType))
        {
            return inventory[itemType] > 0;
        }
        return false;
    }

    // Method to consume an item
    public bool UseItem(string itemType)
    {
        if (HasItem(itemType))
        {
            inventory[itemType]--;
            return true;
        }
        return false;
    }
}