using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class to have general control of the level

public class Level : MonoBehaviour {
    public Text tScore;
    public int score = 0;
    public Transform flockPrefab;
    public Transform enemyPrefab;
    public Enemy2 enemy2;
    public PositiveStimuli pstimul;
    public int numFlock;
    public int numEnemies;
    public List<flockMember> members;
    public List<Enemy> enemies;
    public List<PositiveStimuli> pstimulis;
    public float bounds;
    public float spawnRadius;
    public bool levelEnd;

    // Use this for initialization
    void Start () {

        levelEnd = false;
        members = new List<flockMember>();
        enemies = new List<Enemy>();


        Spawn(flockPrefab, 100);
        //Spawn(enemyPrefab, numEnemies);

        members.AddRange(FindObjectsOfType<flockMember>());
        enemies.AddRange(FindObjectsOfType<Enemy>());
        pstimulis.AddRange(FindObjectsOfType<PositiveStimuli>());
    }

    void Update()
    {
        if (!levelEnd) {
            updateScore();
            tScore.text = score.ToString();
        }
        


        //Create negative stimuli with left click
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            enemies.Add(Instantiate(enemy2, pz, Quaternion.identity));

        }

        //Create positive stimuli with right click
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            pstimulis.Add(Instantiate(pstimul, pz, Quaternion.identity));

        }


    }

    //Function to spawn enemies or flock members
    void Spawn(Transform prefab, int count) {
        for (int i = 0; i < count; i++) {
            Vector3 v = new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0);
            Instantiate(prefab, v, Quaternion.identity);
        }

    }

    public void updateScore() {
        score = score + (members.Count)/5;
    }

    //Get the neighbours that are inside the redius defined
    public List<flockMember> GetNeighbours(flockMember member, float radius) {
        List<flockMember> neighboursFound = new List<flockMember>() ;
        foreach (var otherMember in members) {
            if (otherMember == member)
                continue;

            if (Vector3.Distance(member.position, otherMember.position) <= radius) {
                neighboursFound.Add(otherMember);
            }

        }
        return neighboursFound;
    }

    //Get enemies from the level inside the radius defined
    public List<Enemy> GetEnemies(flockMember member, float radius) {
        List<Enemy> returnEnemies = new List<Enemy>();
        foreach (var enemy in enemies) {
            if (Vector3.Distance(member.position, enemy.position) <= radius)
            {
                returnEnemies.Add(enemy);
            }

        }
        return returnEnemies;
    }

    //Get positive stimuli from the level
    public List<PositiveStimuli> GetStimuli(flockMember member, float radius)
    {
        List<PositiveStimuli> returnStimuli = new List<PositiveStimuli>();
        foreach (var stimuli in pstimulis)
        {
            if (Vector3.Distance(member.position, stimuli.position) <= radius)
            {
                returnStimuli.Add(stimuli);
            }

        }
        return returnStimuli;
    }
}




