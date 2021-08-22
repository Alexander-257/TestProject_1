using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] public float speed;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if(gameManager.isGameActive) {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

            BorderMap();
        }

        ShopZone();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.scoreCoins++;
            gameManager.UpdateScoreCoins();
        }
    }

    void BorderMap() {
        if(transform.position.x > 49.0f) {
            transform.position = new Vector3(49.0f, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -49.0f) {
            transform.position = new Vector3(-49.0f, transform.position.y, transform.position.z);
        }

        if (transform.position.z > 49.0f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 49.0f);
        }

        if (transform.position.z < -49.0f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -49.0f);
        }
    }

    void ShopZone() {
        if (transform.position.x < 3 & transform.position.x > -3 & transform.position.z > -3 & transform.position.z < 3) {
            gameManager.ShopZone(true);

            if (Input.GetKeyDown(KeyCode.B) && gameManager.isGameActive) {
                gameManager.OpenShop();
                gameManager.isGameActive = false;
            }
            else {
                if (Input.GetKeyDown(KeyCode.B) && !gameManager.isGameActive) {
                    gameManager.CloseShop();
                    gameManager.isGameActive = true;
                }
            }
        }
        else { gameManager.ShopZone(false); }
    }
}
