using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles;
using Resources;
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


    private float sidewaysMoveSpeed = 2f;
    private Transform selfTransform;
    private float xTarget;
    private bool xMoving;
    private bool moveOnce;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        setVelocity(carSpeed);

        selfTransform = transform;
        xMoving = false;
        moveOnce = true;

        //StartCoroutine(spawnRandomChallenge(3f));
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
            return;
        }

        hasMadeChoice = false;
        
        if (other.CompareTag("LightObstacle")) {
            LightObstacle obstacle = other.GetComponent<LightObstacle>();
            activeObstacle = obstacle;

            TrafficChoice.SetActive(true);
            activeUI = TrafficChoice;
            obstacle.setInitialState(0);
            obstacle.setStateAfterTime(1, Random.Range(1.5f, 3f));
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

    public void madeChoice() {
        hasMadeChoice = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("RegularObstacle")) return;
        
        activeObstacle = null;
        if(activeUI != null) activeUI.gameObject.SetActive(false);

        if (!hasMadeChoice && !other.CompareTag("LightObstacle")) {
            takeDamage();
            hasMadeChoice = false;
        }
    }

    public void takeDamage() {
        health -= 1;
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
        var c = Random.Range(0, 35);
        
        yield return new WaitForSeconds(time);
        
        if (c < 20) {
            Instantiate(trafficLightObstaclePrefab, (rb.position.Strip(true, false, false) + Vector3.forward * 25f), Quaternion.identity);
        } else if (c < 35) {
            Instantiate(pedestrianObstaclePrefab, (rb.position.Strip(true, false, false) + Vector3.forward * 25f), Quaternion.identity);
        } else if (c < 101) {
            Instantiate(laneObstaclePrefab, (rb.position.Strip(true, false, false) + Vector3.forward * 25f), Quaternion.identity);
        }

        StartCoroutine(spawnRandomChallenge(Random.Range(6f, 12f)));
    }
}
