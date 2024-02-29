using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Starter items
    void Awake()
    {
        inventory["food"] = 2;
        inventory["water"] = 3;
        inventory["toy"] = 1;
    }

    void Start()
    {
        uiManager.UpdateInventoryCounts();
    }

    // Method to add items to the inventory
    public void AddItem(string itemType, int quantity)
    {
        if (!inventory.ContainsKey(itemType))
        {
            inventory[itemType] = 0;
        }
        inventory[itemType] += quantity;
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

    // Get the count of a specific item in the inventory
    public int GetItemCount(string itemType)
    {
        if (inventory.ContainsKey(itemType))
        {
            return inventory[itemType];
        }
        return 0;
    }
}