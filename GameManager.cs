using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject coin;
    [SerializeField] public GameObject shop;
    [SerializeField] Text coinsScore;
    [SerializeField] Text startText;
    [SerializeField] Text buyZoneText;
    [SerializeField] Text shopCurrentCoinsText;
    [SerializeField] Text buyInfo;
    [SerializeField] Button startButton;

    [SerializeField] public bool isGameActive = false;
    [SerializeField] public bool isBuyActive = false;
    [SerializeField] public bool timerActive = false;
    [SerializeField] public int scoreCoins = 0;
    [SerializeField] private float timerValue = 4;
    [SerializeField] private float xRange = 49.0f;
    [SerializeField] private float zRange = 49.0f;

    [SerializeField] public PlayerController playerController;

    void Start() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update() { if (timerActive) BeginBuyInfoCountdown(); }

    public void StartGame() {
        isGameActive = true;
        startButton.gameObject.SetActive(false);
        coinsScore.gameObject.SetActive(true);
        startText.gameObject.SetActive(false);
        UpdateScoreCoins();
        SpawnCoins(10);
    }

    public void SpawnCoins(int coinsNumber) {
        for(int i = 0; i < coinsNumber; i++) {
            Instantiate(coin, SpawnPosition(RandomPositionX(), RandomPositionZ()), coin.transform.rotation);
        }
    }

    public void UpdateScoreCoins() {
        coinsScore.text = "Coins: " + scoreCoins;
    }

    public Vector3 SpawnPosition(float xPos, float zPos) {
        return new Vector3(xPos, 1.25f, zPos);
    }

    public float RandomPositionX() {
        return Random.Range(-xRange, xRange);
    }

    public float RandomPositionZ() {
        return Random.Range(-zRange, zRange);
    }

    public void ShopZone(bool storeStatus) {
        if(isGameActive)
        {
            switch (storeStatus)
            {
                case true:
                    {
                        buyZoneText.gameObject.SetActive(true);
                        isBuyActive = true;
                        break;
                    }
                case false:
                    {
                        buyZoneText.gameObject.SetActive(false);
                        isBuyActive = false;
                        break;
                    }
            }
        }
    }

    public void OpenShop() {
        shop.gameObject.SetActive(true);
        coinsScore.gameObject.SetActive(false);
        shopCurrentCoinsText.text = scoreCoins.ToString();
    }

    public void CloseShop() {
        shop.gameObject.SetActive(false);
        coinsScore.gameObject.SetActive(true);
    }

    public void SpeedAdd() {
        if(scoreCoins == 10) {
            playerController.speed++;
            scoreCoins -= 10;
            UpdateScoreCoins();
            CloseShop();
            isGameActive = true;
            SpawnCoins(10);
            
            BuyInfo("The purchase was completed successfully!");
        }
        else {
            BuyInfo("Missing number of coins!");
        }
    }


    public void BuyInfo(string message) {
        timerActive = true;
        buyInfo.text = message;
        buyInfo.gameObject.SetActive(true);
        CloseShop();
        isGameActive = true;
    }

    public void BeginBuyInfoCountdown() {
        timerValue -= Time.deltaTime;
        if(timerValue <= 1) {
            buyInfo.gameObject.SetActive(false);
            timerActive = false;
            timerValue = 4;
        }
    }
}
