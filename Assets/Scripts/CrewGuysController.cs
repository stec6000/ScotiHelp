using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrewGuysController : MonoBehaviour {

    List<Transform> transformsList = new List<Transform>();

    void Start () {
	    
	}
	
	void Update () {
    }

    public Vector3 getTheClosestByDistance(Vector3 pos)
    {
        int nrTransform = -1;
        float theShrotestDist = 99999;
        for (int i = 0; i < transformsList.Count; i++)
        {
            float tempDist = Vector3.Distance(transformsList[i].position, pos);
            if(theShrotestDist > tempDist)
            {
                nrTransform = i;
                theShrotestDist = tempDist;
            }
        }

        if(theShrotestDist < 14)
        {
            return transformsList[nrTransform].position;
        }

        return new Vector3(0,-111,0);
    }

    public void addCrewGuy(Transform transform)
    {
        transformsList.Add(transform);
    }

    public void removeCrewGuy(Transform transform)
    {
        transformsList.Remove(transform);
    }
}
