using UnityEngine;
using System.Collections;

public class spawnEnemy : MonoBehaviour {

    public GameObject[] enemies;
    public GameObject player;

    public Vector3 spawnPos = new Vector3(0f ,0.5f, 10f);

    private int a = 1;

    private Positions positions;
    private float timestamp;
    private float cooldown;
    // Use this for initialization
    void Start () {
        timestamp = 0;
        cooldown = 1.72f;
        positions = new Positions();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        if(player)
        if (Time.time >= timestamp)
        {
            Instantiate(enemies[0], positions.RandomPoint(player.transform.position), Quaternion.identity);
            timestamp = Time.time + cooldown;
        }
    }
    	
}
