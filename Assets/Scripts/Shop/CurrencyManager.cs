using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private int currentCoins = 100;

    private void Start()
    {
        UpdateCoinsUI();
    }

    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            UpdateCoinsUI();
            return true;
        }
        return false;
    }

    public void EarnCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = "Coins: " + currentCoins;
    }
}
