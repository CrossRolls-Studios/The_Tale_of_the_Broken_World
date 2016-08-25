﻿using UnityEngine;
using System.Collections;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;

    public string exitPoint;

    private PlayerController thePlayer;
	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            Application.LoadLevel(levelToLoad);
            thePlayer.startPoint = exitPoint;
        }
    }
}
