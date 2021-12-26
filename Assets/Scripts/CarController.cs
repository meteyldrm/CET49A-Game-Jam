using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles;
using Resources;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private GameObject TrafficChoice;
    [SerializeField] private GameObject PedestrianChoice;
    [SerializeField] private GameObject LaneChoice;

    [SerializeField] private GameObject trafficLightObstaclePrefab;
    [SerializeField] private GameObject pedestrianObstaclePrefab;
    [SerializeField] private GameObject laneObstaclePrefab;

    [SerializeField] private Material BlackMaterial;
    [SerializeField] private Material GreenMaterial;
    [SerializeField] private Material BlueMaterial;
    [SerializeField] private Material RedMaterial;

    public float carSpeed = 5f;
    
    [SerializeField] private Material chassis;
    [SerializeField] private Material headlight;
    [SerializeField] private Material windshield;
    [SerializeField] private Material spikes;
    [SerializeField] private Material backlight;

    [SerializeField] private Text CoinText;


    private int health = 5;
    private int coin = 0;

    public IBaseObstacle activeObstacle = null;
    private GameObject activeUI;

    private bool hasMadeChoice = false;


    private float sidewaysMoveSpeed = 3f;
    private Transform selfTransform;
    private float xTarget;
    private bool xMoving;
    private bool moveOnce;
    private int onSide = 0;

    [SerializeField] private GameObject alertUI;
    private AlertController _alertController;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        setVelocity(carSpeed);

        _alertController = alertUI.GetComponent<AlertController>();

        selfTransform = transform;
        xMoving = false;
        moveOnce = true;

        coin = 0;

        if (PlayerPrefs.GetInt("Initialized") != 1)
        {
            Initialize();
        }

        setMaterials(carChoice());

        StartCoroutine(spawnRandomChallenge(3f));
    }

    
    void Update()
    {
        //TODO: Fix bug where if a lane switch button is pressed when close to target, existing velocity drags player out of bounds due to changed target location
        
        if (xMoving) {
            if (Math.Abs(xTarget - selfTransform.position.x) < 0.05f) {
                rb.velocity = rb.velocity.Strip(true, false, false);
                var position = rb.position;
                position = new Vector3(xTarget, position.y, position.z);
                rb.position = position;
                xMoving = false;
                moveOnce = true;
                if (Math.Sign(xTarget) < 0) {
                    onSide = 0;
                } else {
                    onSide = 1;
                }
            } else if(moveOnce) {
                Vector3 velocity;
                velocity = new Vector3(Math.Sign(xTarget) * sidewaysMoveSpeed, (velocity = rb.velocity).y, velocity.z);
                rb.velocity = velocity;
                moveOnce = false;
            }
        }
    }

    public void setVelocity(float speed) {
        rb.velocity = new Vector3(0,0,speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Coin")) {
            coin += 1;
            other.gameObject.SetActive(false);
            CoinText.text = "Coin: " + coin.ToString();
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coin);
            return;
        }

        hasMadeChoice = false;
        
        if (other.CompareTag("LightObstacle")) {
            LightObstacle obstacle = other.GetComponent<LightObstacle>();
            activeObstacle = obstacle;

            TrafficChoice.SetActive(true);
            activeUI = TrafficChoice;
            obstacle.setInitialState(0);
            obstacle.setStateAfterTime(1, Random.Range(2.5f, 4f));
        }

        if (other.CompareTag("PedestrianObstacle")) {
            PedestrianObstacle obstacle = other.GetComponent<PedestrianObstacle>();
            activeObstacle = obstacle;

            TrafficChoice.SetActive(true);
            activeUI = TrafficChoice;
            obstacle.setStateAfterTime(1, 0.4f);
        }

        if (other.CompareTag("LaneObstacle")) {
            LaneObstacle obstacle = other.GetComponent<LaneObstacle>();
            activeObstacle = obstacle;
            
            LaneChoice.SetActive(true);
            activeUI = LaneChoice;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("PedestrianCollision")) {
            takeDamageWithReason("You hit a pedestrian!");
        } else if (collision.gameObject.CompareTag("RegularObstacle")) {
            takeDamageWithReason("You hit a road block!");
        }
    }

    public void madeChoice() {
        hasMadeChoice = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("RegularObstacle")) return;
        
        activeObstacle = null;
        if(activeUI != null) activeUI.gameObject.SetActive(false);

        if (other.CompareTag("LaneObstacle")) {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (!hasMadeChoice && other.CompareTag("LightObstacle")) {
            takeDamageWithReason("You cannot cross a red light.");
            hasMadeChoice = false;
        }

        if (!hasMadeChoice && !other.CompareTag("LaneObstacle")) {
            takeDamageWithReason("You didn't make a choice in time.");
            hasMadeChoice = false;
        }
    }

    public void takeDamage() {
        health -= 1;
        rb.velocity = Vector3.zero;
    }
    
    public void takeDamageWithReason(string reason) {
        takeDamage();
        _alertController.alert("Game Over", reason, "OK");
    }

    public void switchLane(string lane) {
        if (lane == "right") {
            xTarget = 1.5f;
            xMoving = true;
        } else if (lane == "left") {
            xTarget = -1.5f;
            xMoving = true;
        }
    }

    private IEnumerator spawnRandomChallenge(float time) {
        var c = Random.Range(0, 101);
        
        yield return new WaitForSeconds(time);
        
        if (c < 20) {
            Instantiate(trafficLightObstaclePrefab, (rb.position.Strip(true, false, false) + Vector3.forward * 20f), Quaternion.identity);
        } else if (c < 50) {
            Instantiate(pedestrianObstaclePrefab, (rb.position.Strip(true, false, false) + Vector3.forward * 25f), Quaternion.identity);
        } else if (c < 101) {
            GameObject o = Instantiate(laneObstaclePrefab, (rb.position.Strip(true, false, false) + Vector3.forward * 30f), Quaternion.identity);
            o.GetComponent<LaneObstacle>().setInitialState(onSide);
        }

        StartCoroutine(spawnRandomChallenge(Random.Range(6f, 13f)));
    }

    Material carChoice()
    {
        string choice = PlayerPrefs.GetString("CarChoice");
        switch (choice)
        {
            case "Black":
                return BlackMaterial;
            case "Green":
                return GreenMaterial;
            case "Blue":
                return BlueMaterial;
            case "Red":
                return RedMaterial;
            default:
                return BlackMaterial;
        }
    }

    void setMaterials(Material carMat) {
        gameObject.GetComponent<MeshRenderer>().materials = new[] { carMat, headlight, windshield, spikes, backlight, backlight };
    }

    void Initialize()
    {
        PlayerPrefs.SetInt("HasBlack", 1);
        PlayerPrefs.SetString("CarChoice", "Black");
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetInt("HasGreen", 0);
        PlayerPrefs.SetInt("HasBlue", 0);
        PlayerPrefs.SetInt("HasRed", 0);
        PlayerPrefs.SetInt("Initialized", 1);
    }
}
