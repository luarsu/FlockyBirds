using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{

    // I only created this class to override the movement from the enemies spawned, as the ones used for boundaries of the level have to move at the same speed at the camera
    public override void movement() {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Remover")
        {
            level.enemies.Remove(this);
        }
    }

}