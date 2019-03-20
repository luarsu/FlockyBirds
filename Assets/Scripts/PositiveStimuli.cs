using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveStimuli : MonoBehaviour
{

    //Class for the positive stimuli

    public FlockManager flockManager;
    public Vector3 position;

    // Use this for initialization
    void Start()
    {
        flockManager = FindObjectOfType<FlockManager>();
        position = transform.position;
    }

    //Remove member from the level list when a flock member touches it
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlockMember" || collision.gameObject.tag == "Remover")
        {
            flockManager.pstimulis.Remove(this);
        }
    }
}
