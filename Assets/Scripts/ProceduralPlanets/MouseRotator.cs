using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotator : MonoBehaviour {

    public float rotationSpeed = 10.0f;
    public Vector3 rotationVector;
    Transform planetTransform;

    private void Start() {
        planetTransform = GameObject.FindGameObjectWithTag("Planet").transform;
    }



    void Update() {
        planetTransform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

}
