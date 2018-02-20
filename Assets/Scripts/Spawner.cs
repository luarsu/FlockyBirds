using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public CameraController cam;
    public Egg egg;
    public Obstacle obstacle;

    //Class for the objects spawner

	void Start () {
        //Get the camera controller
        cam = FindObjectOfType<CameraController>();
        //Invoke the function that spawns ojects randomly
        Invoke("RandomThing", 0.5f);
    }

    //Function that spawns eggs and obstacles randomly
    void RandomThing()
    {
        //random time for invoking again
        float randomTime = Random.Range(3, 13);
        //Calculate position of new spawn
        float posX = Random.Range(-120, 120);
        Vector3 campos = new Vector3(posX, (cam.position.y + 125) , 0);

        if (randomTime < 9)
        {
            //Instantiate egg
            Instantiate(egg, campos, Quaternion.identity);
        }
        else {
            //Instantiate obstacle
            Instantiate(obstacle, campos, Quaternion.identity);
        }

        //Invoke the function again based on the random time
        Invoke("RandomThing", randomTime);

    }
    
}
