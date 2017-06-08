using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}
