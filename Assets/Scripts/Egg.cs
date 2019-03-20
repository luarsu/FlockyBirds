using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    public flockMember flock;
    public FlockManager flockManager;
	// Use this for initialization
	void Start () {
        flockManager = FindObjectOfType<FlockManager>();
    }

    //Create birds when collected
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember")
        {
            flockManager.members.Add(Instantiate(flock, this.transform.position, Quaternion.identity));
            flockManager.members.Add(Instantiate(flock, this.transform.position, Quaternion.identity));
            Destroy(gameObject);
        }
    }
}
