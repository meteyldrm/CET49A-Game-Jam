using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRotate : MonoBehaviour
{
    private void Update() {
        transform.Rotate(Vector3.forward, 60f * Time.deltaTime);
    }
}
