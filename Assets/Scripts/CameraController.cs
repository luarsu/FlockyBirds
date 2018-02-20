using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector3 position;
    public Vector3 velocity;
    // Use this for initialization
    void Start () {
        position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        position = position + velocity * Time.deltaTime;
        transform.position = position;
    }
}
