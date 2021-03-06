﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    public Vector3 position;
    public Vector3 velocity;
    public bool reached;
    public GameObject menu;
    public FlockManager flockManager;
    public MoneyKeeper mon;
    public Text t;
    // Use this for initialization
    void Start () {
        menu.SetActive(false);
        position = transform.position;
        reached = false;
    }
	
	// Update is called once per frame
    //Move with the set velocity
	void Update () {

        if (position.y < 1200)
        {
            position = position + velocity * Time.deltaTime;
            transform.position = position;
        }
        else if (!reached) {
            flockManager.levelEnd = true;
            reached = true;
            Invoke("showMenu", 2.5f);

        }
    }

    public void showMenu() {
        t.text = flockManager.score.ToString();
        menu.SetActive(true);
        mon.setCurrentGems(500);
    }
}
