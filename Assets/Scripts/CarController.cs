using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private GameObject TrafficChoice;
    [SerializeField] private GameObject PedestrianChoice;
    [SerializeField] private GameObject TurnChoice;

    public float carSpeed = 5f;

    private int health = 5;
    private int coin = 0;

    public IBaseObstacle activeObstacle = null;
    private GameObject activeUI;

    private bool hasMadeChoice = false;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        setVelocity(carSpeed);
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
}
