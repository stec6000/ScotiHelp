using UnityEngine;
using System.Collections;

public class hpBarOverCharacte : MonoBehaviour {
    // Use this for initialization
    float maxWidth;
    public float hit = 5;

	void Start () {
        maxWidth = transform.localScale.x;
        hit = (maxWidth / 100) * hit;
    }
	
	void Update () {
	    
	}

    void FixedUpdate ()
    {
        getHit();
    }

    bool getHit()
    {
        float xScale = transform.localScale.x - hit;
        if (xScale < 0)
            xScale = 0;
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        Debug.Log(transform.localScale.x / 2);
        transform.position = new Vector3(0 - (( maxWidth - transform.localScale.x) / 2), transform.position.y, transform.position.z);
        if(transform.localScale.x > 0)
            return true;
        return false;
    }
}
