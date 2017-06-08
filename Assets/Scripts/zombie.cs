using UnityEngine;
using System.Collections;

public class zombie : MonoBehaviour
{

    public Transform target;
    private UnityEngine.AI.NavMeshAgent navComponent;
    private GameObject prefabBlood;
    private Animator anim;
    private Animator zombieAttackAnimator;
    private Vector3 Lastvelocity;
    private Rigidbody rigidBody;
    private int speed = 0;
    private bool attack = false;

    private float distanceCalculatetimestamp = 0;
    private float distanceCalculateCd = 0.1f;

    public Transform effect;

    private CrewGuysController CrewGuysController_;

    void Start()
    {
        navComponent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        prefabBlood = Resources.Load("Blood") as GameObject;
        anim = GetComponentInChildren<Animator>();
        zombieAttackAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();

        CrewGuysController_ = GameObject.FindGameObjectWithTag("CrewGuysController").GetComponent<CrewGuysController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 targetPos = CrewGuysController_.getTheClosestByDistance(transform.position);
            if (targetPos.y != -111)
                if (Time.time >= distanceCalculatetimestamp)
                {
                    navComponent.SetDestination(targetPos);
                    distanceCalculatetimestamp += Time.time + distanceCalculateCd;
                }
                else if (Vector3.SqrMagnitude(transform.position - targetPos) > 1.5f)
                {
                    navComponent.SetDestination(targetPos);
                    distanceCalculatetimestamp = Time.time + distanceCalculateCd;
                }
            
        }
    }

    void FixedUpdate()
    {
        if (speed > 0)
            transform.Translate(speed * Lastvelocity.x * Time.deltaTime, 0f, speed * navComponent.velocity.z * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            //Destroy(effect);

            GameObject blood = Instantiate(prefabBlood, transform.position - new Vector3(0, 0.6f, 0), Quaternion.identity) as GameObject;
            blood.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "CrewGuy")
        {
            navComponent.velocity = new Vector3(0, 0, 0);
            zombieAttackAnimator.SetBool("isActive", true);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "CrewGuy")
        {
            navComponent.velocity = new Vector3(0, 0, 0);
            zombieAttackAnimator.SetBool("isActive", true);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "CrewGuy")
        {
            zombieAttackAnimator.SetBool("isActive", false);
        }
    }
}