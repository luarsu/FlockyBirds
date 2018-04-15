using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingEnemy : MonoBehaviour {

    //public flockMember targetBird;
    public Vector3 target;
    public Vector3 initialPos;
    public float speed = 50.0f;
    bool rightLeft = true;
    public Level level;
    public bool withtarget;



    // Use this for initialization
    void Start () {
        withtarget = false;
        level = FindObjectOfType<Level>();
        initialPos = transform.position;
        target = initialPos - new Vector3(-70,0,0);


    }
	
	// Update is called once per frame
	void Update () {

        //checkTargets();

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
    /*
    void checkTargets() {

        if (!withtarget)
        {
            var enemyList = level.GetNeighbours(this, detectingRadious);
            if (enemyList.Count > 0)
            {
                targetBird = enemyList[0];
                withtarget = true;

            }
        } //Update target position
        else {
            target = targetBird.position;
        }

    }
    */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember")
        {
            Destroy(collision.gameObject);
        }


    }

}
