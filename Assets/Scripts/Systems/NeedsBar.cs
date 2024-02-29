using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NeedsBar : MonoBehaviour
{
    //tutorial by katherine kwasny on yt

    public float happiness = 100;
    public float hunger = 100;
    public float thirsty = 100;
    public float tired = 100;
    public float bored = 100;
    public float max = 100;

    [Header("Needs Bars")]
    [SerializeField] private Image currentHungry;
    [SerializeField] private Image currentThirsty;
    [SerializeField] private Image currentTired;
    [SerializeField] private Image currentBored;

    [Header("Needs Bubbles")]
    [SerializeField] private Image foodBubble;
    [SerializeField] private Image waterBubble;
    [SerializeField] private Image bedBubble;
    [SerializeField] private Image toyBubble;

    [Header("Pet Happiness Display")]
    [SerializeField] private Image petVeryHappy;
    [SerializeField] private Image petHappy;
    [SerializeField] private Image petSad;
    [SerializeField] private Image petDying;

    [Header("Sound Settings")]
    public AudioClip playSound;
    private AudioSource audioSource;

    [Header("Death Sequence")]
    [SerializeField] private Button ResetButton;
    [SerializeField] private Canvas DeathSequenceCanvas;
    [SerializeField] TextMeshProUGUI GameOverText;

    private void Start()
    {
        foodBubble.CrossFadeAlpha(0, 0.001f, true);
        waterBubble.CrossFadeAlpha(0, 0.001f, true);
        bedBubble.CrossFadeAlpha(0, 0.001f, true);
        toyBubble.CrossFadeAlpha(0, 0.001f, true);

        petSad.gameObject.SetActive(false);

        {
            if (DeathSequenceCanvas != null)
                DeathSequenceCanvas.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = playSound;
    }

    private void Update()
    {
        // Adjust the rate of decrease for hunger, thirsty, tired, and bored
        hunger -= 1.3f * Time.deltaTime;
        thirsty -= 2f * Time.deltaTime;
        tired -= 0.3f * Time.deltaTime;
        bored -= 0.7f * Time.deltaTime;

        // Ensure the values don't drop below 0
        hunger = Mathf.Max(hunger, 0);
        thirsty = Mathf.Max(thirsty, 0);
        tired = Mathf.Max(tired, 0);
        bored = Mathf.Max(bored, 0);

        // Calculate happiness based on the average of the four needs
        happiness = (hunger + thirsty + tired + bored) / 4;

        UpdateAllBars();
        NeedsCheck();
    }

    private void NeedsCheck()
    {
        if (hunger <= 50)
        {
            foodBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            foodBubble.CrossFadeAlpha(0, 0.5f, true);
        }

        if (thirsty <= 60)
        {
            waterBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            waterBubble.CrossFadeAlpha(0, 0.5f, true);
        }

        if (tired <= 20)
        {
            bedBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            bedBubble.CrossFadeAlpha(0, 0.5f, true);
        }

        if (bored <= 40)
        {
            toyBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            toyBubble.CrossFadeAlpha(0, 0.5f, true);
        }


        GoodOwnerCheck();

        UpdateAllBars();

        GameOver();

    }

    public void FeedThePet()
    {
        hunger = Mathf.Min(hunger + 5, max);
        UpdateBar(hunger, currentHungry);
    }

    public void WaterThePet()
    {
        thirsty = Mathf.Min(thirsty + 5, max);
        UpdateBar(thirsty, currentThirsty);
    }

    public void RestThePet()
    {
        tired = Mathf.Min(tired + 20, max);
        UpdateBar(tired, currentTired);
    }

    public void PlayWithThePet()
    {
        bored = Mathf.Min(bored + 20, max);
        UpdateBar(bored, currentBored);
    }

    private void GoodOwnerCheck()
    {
        // Check for the most critical condition first
        if (hunger <= 20 || thirsty <= 20 || tired <= 20 || bored <= 20)
        {
            // If any condition for dying is met, only show the dying image
            petHappy.gameObject.SetActive(false);
            petSad.gameObject.SetActive(false);
            petVeryHappy.gameObject.SetActive(false); // Ensure very happy is not shown
            petDying.gameObject.SetActive(true);
        }
        else if (hunger <= 50 || thirsty <= 50 || tired <= 50 || bored <= 50)
        {
            // If conditions for being sad are met (and not dying), show only the sad image
            petHappy.gameObject.SetActive(false);
            petDying.gameObject.SetActive(false); // Ensure dying is not shown
            petVeryHappy.gameObject.SetActive(false); // Ensure very happy is not shown
            petSad.gameObject.SetActive(true);
        }
        else if (happiness > 75)
        {
            // New condition for very happy
            petHappy.gameObject.SetActive(false);
            petSad.gameObject.SetActive(false);
            petDying.gameObject.SetActive(false); // Ensure dying is not shown
            petVeryHappy.gameObject.SetActive(true); // Show very happy image
        }
        else
        {
            // If none of the above conditions are met, the pet is happy
            petSad.gameObject.SetActive(false);
            petDying.gameObject.SetActive(false); // Ensure dying is not shown
            petVeryHappy.gameObject.SetActive(false); // Ensure very happy is not shown
            petHappy.gameObject.SetActive(true);
        }
    }

    private void UpdateAllBars()
    {
        UpdateBar(hunger, currentHungry);
        UpdateBar(thirsty, currentThirsty);
        UpdateBar(tired, currentTired);
        UpdateBar(bored, currentBored);
    }

    private void UpdateBar(float value, Image img)
    {
        float ratio = value / max;
        img.rectTransform.localScale = new Vector3(ratio, 1, 1);

    }

    private void GameOver()
    {
        if (hunger <= 0 || thirsty <= 0 || tired <= 0 || bored <= 0)
        {
            if (DeathSequenceCanvas != null)
                DeathSequenceCanvas.gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        if (DeathSequenceCanvas != null)
            DeathSequenceCanvas.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}