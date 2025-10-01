using UnityEngine;
using UnityEngine.UI;
using System;

//Animación con dotween?
public class NeGiManager : MonoBehaviour
{
    public Image neGiImage;
    public GameObject bubbleThought;

    public Button btnComer;
    public Button btnDormir;
    public Button btnTrabajar;

    public float timeToNeedFood = 10f;
    public float timeToNeedSleep = 15f;
    public float timeToNeedWork = 20f;

    private float foodTimer;
    private float sleepTimer;
    private float workTimer;

    void Start()
    {
        btnComer.onClick.AddListener(Comer);
        btnDormir.onClick.AddListener(Dormir);
        btnTrabajar.onClick.AddListener(Trabajar);

        LoadTimers(); // calcula el tiempo fuera de app
        bubbleThought.SetActive(false);
    }

    void Update()
    {
        foodTimer -= Time.deltaTime;
        sleepTimer -= Time.deltaTime;
        workTimer -= Time.deltaTime;

        if (foodTimer <= 0 || sleepTimer <= 0 || workTimer <= 0)
        {
            bubbleThought.SetActive(true);
        }
    }

    void Comer()
    {
        foodTimer = timeToNeedFood;
        PlayerPrefs.SetString("LastEat", DateTime.Now.ToString());
        PlayerPrefs.Save();
        bubbleThought.SetActive(false);
        Debug.Log("Comió");
    }

    void Dormir()
    {
        sleepTimer = timeToNeedSleep;
        PlayerPrefs.SetString("LastSleep", DateTime.Now.ToString());
        PlayerPrefs.Save();
        bubbleThought.SetActive(false);
        Debug.Log("Durmió");
    }

    void Trabajar()
    {
        workTimer = timeToNeedWork;
        PlayerPrefs.SetString("LastWork", DateTime.Now.ToString());
        PlayerPrefs.Save();
        bubbleThought.SetActive(false);
        Debug.Log("Trabajó");
    }

    void LoadTimers()
    {
        DateTime now = DateTime.Now;

        if (PlayerPrefs.HasKey("LastEat"))
        {
            DateTime lastEat = DateTime.Parse(PlayerPrefs.GetString("LastEat"));
            double secondsPassed = (now - lastEat).TotalSeconds;
            foodTimer = Mathf.Max(0f, timeToNeedFood - (float)secondsPassed);
        }
        else foodTimer = timeToNeedFood;

        if (PlayerPrefs.HasKey("LastSleep"))
        {
            DateTime lastSleep = DateTime.Parse(PlayerPrefs.GetString("LastSleep"));
            double secondsPassed = (now - lastSleep).TotalSeconds;
            sleepTimer = Mathf.Max(0f, timeToNeedSleep - (float)secondsPassed);
        }
        else sleepTimer = timeToNeedSleep;

        if (PlayerPrefs.HasKey("LastWork"))
        {
            DateTime lastWork = DateTime.Parse(PlayerPrefs.GetString("LastWork"));
            double secondsPassed = (now - lastWork).TotalSeconds;
            workTimer = Mathf.Max(0f, timeToNeedWork - (float)secondsPassed);
        }
        else workTimer = timeToNeedWork;
    }
}
