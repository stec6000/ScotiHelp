using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

    private int random;
    private Vector3[] coins;
    private GameObject coin;
    private Vector3 pos;

    private Positions positions;

    private bool isTrigger = false;
    void Awake () {
        positions = new Positions();
        coin = GameObject.FindGameObjectWithTag("Coin");
    }

    public void FixedUpdate()
    {
        if (!isTrigger)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            isTrigger = true;//to jest po to ze jak sie wbije na sciane to zejdzie z niej a poxniej zrobi sie trigger znowu
        }
    }
	
    public void SpawnCoin()
    {
     Instantiate(coin, positions.RandomPoint(), Quaternion.identity);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
            SpawnCoin();
        }
    }

}
