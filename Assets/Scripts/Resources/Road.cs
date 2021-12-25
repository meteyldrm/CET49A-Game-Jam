using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject Car;
    private Vector3 position;
    void Start()
    {
        position = new Vector3(0, 0, 0);
        Car = GameObject.Find("Car");
    }

    void Update()
    {
        if(Car.transform.position.z > gameObject.transform.position.z + 7)
        {
            Destroy(gameObject);
        }
    }
}
