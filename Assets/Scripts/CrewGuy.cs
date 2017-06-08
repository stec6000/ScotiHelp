using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrewGuy : MonoBehaviour {
    public Transform target;
    public Transform Endtarget;
    public Transform effect;

    private UnityEngine.AI.NavMeshAgent navComponent;
    private Vector3 targetPos;
    private Animator anim;

    private bool isReady = false;
    private GameObject hpCanvas;
    private GameObject hpBar;
    private Slider healthSlider;

    public float currentHealth = 100;

    private float bloodHitTimeStamp = 0;
    private float bloodHitCd = 0.5f;

    private GameObject prefabBlood;

    private CrewGuysController CrewGuysController_;

    void Start()
    {
        hpCanvas = (GameObject)Instantiate(Resources.Load("hpBarCanvas") as GameObject);
        prefabBlood = Resources.Load("BloodRed") as GameObject;
        hpBar = hpCanvas.transform.Find("HealthSlider").gameObject;
        healthSlider = hpBar.GetComponent<Slider>();
        navComponent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        increaseAmountInPLayerController(1);
        CrewGuysController_ = GameObject.FindGameObjectWithTag("CrewGuysController").GetComponent<CrewGuysController>();
        CrewGuysController_.addCrewGuy(transform);
    }
    
    void Update()
    {

    }

    void FixedUpdate()
    {
        hpBar.transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y , transform.position.z + 0.5f);
        if (target && Endtarget)
        {
            if (isReady)
                targetPos = Endtarget.position;
            else
                targetPos = target.position;

            if (Vector3.SqrMagnitude(transform.position - targetPos) > 1.5f)
            {
                anim.SetBool("isWalking", true);
                navComponent.Resume();
                navComponent.SetDestination(targetPos);
            }
            else
            {
                anim.SetBool("isWalking", false);
                navComponent.Stop();
                navComponent.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if (col.CompareTag("Endquad"))
        {
            isReady = true;
        }
        if (col.CompareTag("EndPoint"))
        {
            increaseAmountInPLayerController(-1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().scoreUp();
            dead();
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy")
        {
            if (bloodHitTimeStamp < Time.time)
            {
                GameObject blood = Instantiate(prefabBlood, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity) as GameObject;
                blood.transform.rotation = Quaternion.Euler(90, 0, 0);
                bloodHitTimeStamp = Time.time + bloodHitCd;
            }
            currentHealth -= 10f;
            healthSlider.value = currentHealth;
            if(currentHealth <= 0)
            {
                increaseAmountInPLayerController(-1);
                dead();
            }
        }
    }

    void dead()
    {
        CrewGuysController_.removeCrewGuy(transform);

        GameObject blood = Instantiate(prefabBlood, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity) as GameObject;
        blood.transform.rotation = Quaternion.Euler(90, 0, 0);
        bloodHitTimeStamp = Time.time + bloodHitCd;

        Destroy(hpCanvas);
        Destroy(gameObject);

        if (GameObject.FindGameObjectsWithTag("mapPoint").Length == 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().endGame();
    }

    void increaseAmountInPLayerController(int value)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().increaseCrewGuy(
            gameObject.transform.GetChild(0).gameObject.name,
            value
            );
    }
}
