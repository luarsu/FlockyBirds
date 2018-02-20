using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    public flockMember flock;
    public Level level;
	// Use this for initialization
	void Start () {
        level = FindObjectOfType<Level>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember")
        {
            level.members.Add(Instantiate(flock, this.transform.position, Quaternion.identity));
            level.members.Add(Instantiate(flock, this.transform.position, Quaternion.identity));
            Destroy(gameObject);
        }
    }
}
