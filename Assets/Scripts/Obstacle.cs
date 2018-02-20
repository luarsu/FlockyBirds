using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember") {
            Destroy(collision.gameObject);
        }
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
