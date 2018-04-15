using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Remover : MonoBehaviour {

    //Class for the remover of objects 

    public int score;
    public Vector3 position;
    public Vector3 velocity;
    public Level level;

    // Use this for initialization
    void Start () {
        position = transform.position;
        level = FindObjectOfType<Level>();
    }
	
	// Update position
	void Update () {
        position = position + velocity * Time.deltaTime;
        transform.position = position;
    }

    //Destroy every object that is touched by it
    void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(collision.gameObject, .1f);
        if (level.members.Count == 0) {
            //reload scene when die
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

    }

}
