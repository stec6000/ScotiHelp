using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
    private GameObject player;
    PlayerController playerScript;

    void Start () {
        player = GameObject.Find("playerContainer");
        playerScript = player.GetComponent<PlayerController>();

	}
	
	void Update () {
        if (playerScript.enableToTeleport) gameObject.SetActive(true);
        else gameObject.SetActive(false);
	}
}
