using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private GameObject TrafficUI;

    [SerializeField] private GameObject TrafficChoice;
    [SerializeField] private GameObject PedestrianChoice;
    [SerializeField] private GameObject TurnChoice;

    public float carSpeed = 5f;

    private int health = 5;

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
        hasMadeChoice = false;
        if (other.CompareTag("LightObstacle")) {
            LightObstacle obstacle = other.GetComponent<LightObstacle>();
            activeObstacle = obstacle;
            var controller = TrafficUI.GetComponent<LightUIAppearanceController>();

            TrafficChoice.SetActive(true);
            activeUI = TrafficChoice;
            controller.gameObject.SetActive(true);
            var state = Random.Range(0, 101);
            if (state < 20) {
                controller.green();
            } else if (state > 20) {
                controller.red();
                controller.setStateAfterTime(false, 2.5f);
                obstacle.setStateAfterTime(false, 2.5f);
            }
        }
    }

    public void madeChoice() {
        hasMadeChoice = true;
    }

    private void OnTriggerExit(Collider other) {
        activeObstacle = null;
        activeUI.gameObject.SetActive(false);
        TrafficUI.SetActive(false);

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
