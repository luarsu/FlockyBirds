using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember") {
            Destroy(collision.gameObject);
        }
    }

}
