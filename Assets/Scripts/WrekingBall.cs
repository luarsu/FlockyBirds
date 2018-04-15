using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrekingBall : MonoBehaviour {

    public float rotSpeed = 25.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * rotSpeed);

    }

}
