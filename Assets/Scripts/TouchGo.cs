﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TouchGo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Menu");
        }
	}
}
