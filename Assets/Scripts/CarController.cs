using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles;
using Resources;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private GameObject TrafficChoice;
    [SerializeField] private GameObject PedestrianChoice;
    [SerializeField] private GameObject LaneChoice;

    [SerializeField] private GameObject trafficLightObstaclePrefab;
    [SerializeField] private GameObject pedestrianObstaclePrefab;
    [SerializeField] private GameObject laneObstaclePrefab;

    public float carSpeed = 5f;

    private int health = 5;
    private int coin = 0;

    public IBaseObstacle activeObstacle = null;
    private GameObject activeUI;

    private bool hasMadeChoice = false;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        setVelocity(carSpeed);

        StartCoroutine(spawnRandomChallenge(3f));
    }

    
    void Update()
    {
        
    }

    public void setVelocity(float speed) {
        rb.velocity = new Vector3(0,0,speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Coin")) {
            coin += 1;
            other.gameObject.SetActive(false);
            return;
        }

        hasMadeChoice = false;
        
        if (other.CompareTag("LightObstacle")) {
            LightObstacle obstacle = other.GetComponent<LightObstacle>();
            activeObstacle = obstacle;

            TrafficChoice.SetActive(true);
            activeUI = TrafficChoice;
            var state = Random.Range(0, 101);
            if (state < 20) {
                obstacle.setInitialState(1);
            } else {
                obstacle.setInitialState(0);
                obstacle.setStateAfterTime(1, 2.5f);
            }
        }

        if (other.CompareTag("PedestrianObstacle")) {
            PedestrianObstacle obstacle = other.GetComponent<PedestrianObstacle>();
            activeObstacle = obstacle;

            TrafficChoice.SetActive(true);
            activeUI = TrafficChoice;
            var state = Random.Range(0, 101);
            if (state < 20) {
                obstacle.setInitialState(1);
            } else {
                obstacle.setInitialState(0);
                obstacle.setStateAfterTime(1, 2.5f);
            }
        }
    }

    public void madeChoice() {
        hasMadeChoice = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("RegularObstacle")) return;
        
        activeObstacle = null;
        activeUI.gameObject.SetActive(false);

        if (!hasMadeChoice) {
            takeDamage();
            hasMadeChoice = false;
        }
    }

    public void takeDamage() {
        health -= 1;
        print($"Remaining health {health}");
    }

    public void switchLane(string lane) {
        if (lane == "right") {
            var position = rb.position;
            rb.transform.position = new Vector3(1.5f, position.y, position.z);
        } else if (lane == "left") {
            var position = rb.position;
            rb.transform.position = new Vector3(-1.5f, position.y, position.z);
        }
    }

    private IEnumerator spawnRandomChallenge(float time) {
        var c = Random.Range(0, 35);
        
        yield return new WaitForSeconds(time);
        
        if (c < 20) {
            Instantiate(trafficLightObstaclePrefab, (rb.position + Vector3.forward * 15f), Quaternion.identity);
        } else if (c < 35) {
            Instantiate(pedestrianObstaclePrefab, (rb.position + Vector3.forward * 15f), Quaternion.identity);
        } else if (c < 101) {
            Instantiate(laneObstaclePrefab, (rb.position + Vector3.forward * 25f), Quaternion.identity);
        }

        StartCoroutine(spawnRandomChallenge(Random.Range(4f, 8f)));
    }
}
