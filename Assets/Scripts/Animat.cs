using UnityEngine;
using System.Collections;

public class Animat : MonoBehaviour {

    Animation animation;
	// Use this for initialization
	void Start () {
        animation = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!animation.IsPlaying("dymek") && !animation.IsPlaying("blood"))
        {
            Destroy(this.gameObject);
        }
    }
}
