using System.Collections;
using UnityEngine;

public class AdventureSystem : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private UIManager uiManager;

    // Adventure settings
    private const int adventureDuration = 10; // Duration in seconds
    private const int guaranteedCoins = 5; // Minimum coins found

    public void StartAdventure()
    {
        StartCoroutine(AdventureRoutine());
    }

    private IEnumerator AdventureRoutine()
    {
        yield return new WaitForSeconds(adventureDuration);

        AdventureResult result = GenerateAdventureLoot();
        inventorySystem.HandleAdventureLoot(result);
        uiManager.UpdateAdventureUI(result);
    }

    private AdventureResult GenerateAdventureLoot()
    {
        AdventureResult result = new AdventureResult();

        // Guaranteed coins
        result.Coins = Random.Range(guaranteedCoins, 20);

        // Random chance for other items
        result.Food = Random.Range(0, 2); // 0 or 1
        result.Water = Random.Range(0, 2);
        result.Toys = Random.Range(0, 2);
        result.RareItems = Random.value > 0.9f ? 1 : 0; // 10% chance for a rare item

        return result;
    }
}
