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

    [SerializeField] private Image petHappy;
    [SerializeField] private Image petSad;

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
        // hunger 0.8f, thirsty 1f, tired 0.3f, bored 0.7f

        happiness -= 5.5f * Time.deltaTime;
        if (happiness < 0)
        {
            happiness = 0;
        }

        hunger -= 5.5f * Time.deltaTime;
        if (hunger < 0)
        {
            hunger = 0;
        }

        thirsty -= 5.5f * Time.deltaTime;
        if (thirsty < 0)
        {
            thirsty = 0;
        }

        tired -= 5.5f * Time.deltaTime;
        if (tired < 0)
        {
            tired = 0;
        }

        bored -= 5.5f * Time.deltaTime;
        if (bored < 0)
        {
            bored = 0;
        }

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
        hunger = Mathf.Min(hunger + 20, max);
        UpdateBar(hunger, currentHungry);
    }

    public void WaterThePet()
    {
        thirsty = Mathf.Min(thirsty + 20, max);
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

        if (playSound != null && audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void GoodOwnerCheck()
    {
        if (hunger <= 50 || thirsty <= 60 || tired <= 20 || bored <= 40)
        {
            petHappy.gameObject.SetActive(false);
            petSad.gameObject.SetActive(true);
        }
        else
        {
            petSad.gameObject.SetActive(false);
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
        if (hunger <= 0 || thirsty < 0 || tired <= 0 || bored <= 0)
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