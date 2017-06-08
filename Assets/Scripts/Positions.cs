using UnityEngine;
using System.Collections;

class Positions
    {
    public GameObject player;
    public float mapHalfWidth;
    public float mapHalfHeight;

    private Vector3 pos;
    private Vector3 ScreenPoint;

    public Positions()
    {
        mapHalfWidth = GameObject.FindGameObjectWithTag("MainFloor").transform.localScale.y/2 - 1;
        mapHalfHeight = GameObject.FindGameObjectWithTag("MainFloor").transform.localScale.x / 2 - 1;
    }

    public Vector3 RandomPoint(Vector3 position = new Vector3())
    {
        do
        {
            pos = new Vector3(Random.Range(-mapHalfWidth, mapHalfWidth), 0.5f, Random.Range(-mapHalfHeight, mapHalfHeight));
            //ScreenPoint = Camera.main.WorldToViewportPoint(pos);
        }
        while (Vector3.Distance(pos, position) < 9);
        return pos;
    }
}

