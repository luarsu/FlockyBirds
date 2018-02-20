using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour {

    //Class to set the configuration of the flock algorithm

    //Max field of view
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    //wander variables
    public float wanderJitter;
    public float wanderRadious;
    public float wanderDistance;
    public float wanderPriority;

    // cohesion variables
    public float cohesionRadius;
    public float cohesionPriority;

    // algnment variables
    public float alignmentRadius;
    public float alignmentPriority;

    //Separation Variables
    public float separationRadius;
    public float separationPriority;

    //Avoidance variables
    public float avoidanceRadius;
    public float avoidancePriority;

    //Positive stimuli variables
    public float stimuliRadius;
    public float stimuliPriority;

    // Use this for initialization
    void Start () {
		
	}
}
