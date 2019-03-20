using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingEnemy : MonoBehaviour {

    //public flockMember targetBird;
    public Vector3 target;
    public Vector3 initialPos;
    public float speed = 50.0f;
    bool rightLeft = true;
    public FlockManager flockManager;
    public bool withtarget;



    // Use this for initialization
    void Start () {
        withtarget = false;
        flockManager = FindObjectOfType<FlockManager>();
        initialPos = transform.position;
        target = initialPos - new Vector3(-70,0,0);


    }
	
	// Update is called once per frame
    //Enemies that move from one side to the other.
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, target, (speed * Time.deltaTime));

        if (transform.position == target) {
            
                if (rightLeft)
                {
                    target = initialPos + new Vector3(-70, 0, 0);
                    rightLeft = !rightLeft;
                }
                else
                {
                    target = initialPos - new Vector3(-70, 0, 0);
                    rightLeft = !rightLeft;
                }
            
            
        }
    }

    //Destroy colliding birds
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember")
        {
            Destroy(collision.gameObject);
        }
    }

}
