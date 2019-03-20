using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockMember : MonoBehaviour {

    //Class for the flock ai members ~ this is where the magic happens

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public FlockManager flockManager;
    public MemberConfig conf;

    Vector3 wanderTarget;

    void Start () {
        //Get level and config data
        flockManager = FindObjectOfType<FlockManager>();
        conf = FindObjectOfType<MemberConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3,3), Random.Range(-3,3),0);
    }

    // Update is called once per frame
    void Update () {
        //Get the acceleration
        acceleration = Combine();
        //restricts it to the max acceleration
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration);
        //Calculate velocity vector
        velocity = velocity + acceleration * Time.deltaTime;
        //Retrict the value to the max velocity
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);
        //Calculate position based on velocity and acceleration
        position = position + velocity * Time.deltaTime;
        //Adjust to level limits
        //WrapAround(ref position, -level.bounds, level.bounds);
        transform.position = position;
   
	   
	}

    /* METHODS TO CALCULATE DIRECTION, VELOCITY AND ACCELERATION*/


    //Combines combines wander, cohesion, alignment, separation, avoidance and gotopositive stimuli based on their priorities. Returns the final acceleration vector
    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = conf.cohesionPriority * Cohesion() + conf.wanderPriority * Wander()
            + conf.alignmentPriority * Alignment() + conf.separationPriority * Separation()
            + conf.avoidancePriority * Avoidance() + conf.stimuliPriority * GoToPositiveStimuli();
        return finalVec;
    }

    //Methos to return the random wander acceleration vector for the flock member /* RANDOM MOVEMENT*/
    protected Vector3 Wander()
    {
        float jitter = conf.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        //project random vector in the radius
        wanderTarget *= conf.wanderRadious;
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(0, conf.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= this.position;
        return targetInWorldSpace.normalized;

    }

    //TRIES TO GET CLOSER TO OTHER BIRDS. Calculates the cohesion vector to maintain the flock members close
    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbours = flockManager.GetNeighbours(this, conf.cohesionRadius);
        //No neighbours close to the member
        if (neighbours.Count == 0)
        {
            return cohesionVector;
        }
        //Basically gets the position of all the birds that are withn the range defined, computes the averge position of all of them and goes towards it.
        foreach (var member in neighbours)
        {
            if (isInFOV(member.position))
            {
                cohesionVector += member.position;
                countMembers++;
            }

        }

        if (countMembers == 0)
            return cohesionVector;

        //We calculate the cohesion vector with the member position and other members in FOV position and we normalize it
        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    //TRY TO GO IN THE SAME DIRECTION AS OTHER BIRDS. Compute the alignment acceleration vector based on the direction of the other birds.
    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        var members = flockManager.GetNeighbours(this, conf.alignmentRadius);
        if (members.Count == 0)
        {
            return alignVector;
        }
        // Computes the average of the direction of all the birds within the defined radious. 
        foreach (var member in members)
        {
            if (isInFOV(member.position))
            {
                alignVector += member.velocity;
            }
        }
        return alignVector.normalized;
    }


    // TRIES TO STAY SEPARATED/ NOT COLLIDE WITH OTHER BIRDS. Computes the acceleration vector of separation vector based on the neighbours inside the separation radius
    Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();
        var members = flockManager.GetNeighbours(this, conf.separationRadius);
        if (members.Count == 0)
        {
            return separateVector;
        }

        foreach (var member in members)
        {
            if (isInFOV(member.position))
            {
                //Tries to go in the oposite direction of the position of the birds that are too close. Calculates the average of all those directions.
                Vector3 movingTowards = this.position - member.position;
                if (movingTowards.magnitude > 0)
                {
                    separateVector += movingTowards.normalized / movingTowards.magnitude;
                }
            }
        }
        return separateVector.normalized;
    }

    

    //TRIES TO ESTAY AWAY FROM ENEMIES. Calculate the vector for the avoidance of the enemies by computing the averange of the avoidance vector from the enemies
    Vector3 Avoidance() {
        Vector3 avoidVector = new Vector3();
        //get enemies within the range
        var enemyList = flockManager.GetEnemies(this, conf.avoidanceRadius);
        if (enemyList.Count == 0) {
            return avoidVector;
        }
        //Calculate the average of the avoidance direction (go in the oposite direction of where the enemies are) and returns it
        foreach (var enemy in enemyList) {
            avoidVector += RunAway(enemy.position);

        }
        return avoidVector.normalized;
    }

    //Returns vector acceleration direction towards the closer positive stimuli
    Vector3 GoToPositiveStimuli() {
        Vector3 stimuliVector = new Vector3();
        var stimuliList = flockManager.GetStimuli(this, conf.stimuliRadius);
        if (stimuliList.Count == 0)
        {
            return stimuliVector;
        }
        float closer = 10000000000;
        float distanceTo;
        Vector3 direction;
        foreach (var stimuli in stimuliList)
        {
            //Calculate direction vector towards the positive stimuli
            direction = stimuli.position - this.position;
            distanceTo = Mathf.Abs(direction.magnitude);
            if (distanceTo < closer) {
                stimuliVector = direction;
                closer = distanceTo;
            }
            

        }
        return stimuliVector.normalized;
}

    //Run oposite direction from target
    Vector3 RunAway(Vector3 target) {
        Vector3 neededVelocity = (position - target).normalized * conf.maxVelocity;
        return neededVelocity - velocity;
    }

    


    //Returns true if the neighbours is in the field of vision of the member
    bool isInFOV(Vector3 vec) {
        return Vector3.Angle(this.velocity, vec - this.position) <+ conf.maxFOV;

    }



    
    //Checks collision to remove positive stimulis on touch or remove self from the level list when the object is deleted
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PStimuli")
        {
            Destroy(collision.gameObject, .1f);
        }

        if (collision.gameObject.tag == "Remover" || collision.gameObject.tag == "Obstacle")
        {
            flockManager.members.Remove(this);
        }
    }


    //If an object is moving to the limits of the screen it will return to a valid position. I used this for first tests
    void WrapAround(ref Vector3 vector, float min, float max)
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);
    }

    //Function to adjust the wrap around values
    float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
        {
            value = min;
        }
        else if (value < min)
        {
            value = max;
        }
        return value;

    }


    //It does what it says ~ return a random binomial
    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);

    }

}
