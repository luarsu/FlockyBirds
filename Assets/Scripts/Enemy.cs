using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Level level;
    public Vector3 position;
    public Vector3 velocity;
    
    // Use this for initialization
    void Start () {
        position = transform.position;
        level = FindObjectOfType<Level>();
    }
	
	// Update is called once per frame
	void Update () {
        movement();
    }

    public virtual void movement()
    {
        position = position + velocity * Time.deltaTime;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Remover")
        {
            level.enemies.Remove(this);
        }
    }
}
