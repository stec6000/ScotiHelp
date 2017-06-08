using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    
void OnTriggerEnter(Collider col)
    {
        if(!col.CompareTag("Player") && !col.CompareTag("CrewGuy") && !col.CompareTag("Endquad")) Destroy(gameObject);
    }
}
