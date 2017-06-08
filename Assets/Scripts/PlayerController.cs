using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public float drag = 0.5f;
    public float terminalRotationSpeed = 25f;
    private Rigidbody rig;

    public bool enableToTeleport = false;
    private GameObject enemies;
    private Animator anim;
    private GameObject coin;
    private GameObject canvas;

    private Canvas gameOverCanvas;
    private GameObject prefabBlood;

    public AudioClip shoot;
    private AudioSource source;

    GameObject prefabBullet;
    Behaviour halo;

    private float timeBetweenShots;
    private float timestamp;
    private bool canShoot;
    private bool shake;

    private float bloodHitTimeStamp = 0;
    private float bloodHitCd = 0.5f;

    private Vector3[] coins;

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public float currentHealth;                                   // The current health the player has.
    public Slider healthSlider;

    public VirtualJoystick joystick;
    public RotateJoystick rotateJoystick;
    private bool isShooting;

    private Text scoreText;
    private int maxScore = 0;
    private int score = 0;

    private int crewGuy1 = 0;//cooldownreduction
    private int crewGuy2 = 0;//heal
    private int crewGuy3 = 0;//speed

    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;

    Vector3 dir;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        shake = false;
        isShooting = false;
        rig = GetComponent<Rigidbody>();
        rig.maxAngularVelocity = terminalRotationSpeed;
        rig.drag = drag;

        anim = GetComponentInChildren<Animator>();
        coin = GameObject.FindGameObjectWithTag("Coin");
        coin.GetComponent<CoinController>().SpawnCoin();
        canvas = GameObject.Find("Canvas");
        prefabBlood = Resources.Load("BloodRed") as GameObject;

        gameOverCanvas = GameObject.FindGameObjectWithTag("MenuCanvasEnd").GetComponent<Canvas>();
        gameOverCanvas.enabled = false;

        currentHealth = startingHealth;
        // dziwne sa chyba te jednostki, tutaj chyba jest cos koło 1 sekundy
        timeBetweenShots = 1f;
        timestamp = 0;

        prefabBullet = Resources.Load("Bullet") as GameObject;

        halo = (Behaviour)GetComponent("Halo");

        GameObject kamera = GameObject.FindGameObjectWithTag("MainCamera");

        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();
        maxScore = GameObject.FindGameObjectsWithTag("mapPoint").Length;
        score = 0;

        GameObject.FindGameObjectWithTag("CrewGuysController").GetComponent<CrewGuysController>().addCrewGuy(transform);
        
        effect1.SetActive(false);
        effect2.SetActive(false);
        effect3.SetActive(false);
    }

    void Update()
    {
        if (Time.time >= timestamp && Input.GetMouseButtonDown(0))
        {
            source.PlayOneShot(shoot);
            GameObject bullet = Instantiate(prefabBullet) as GameObject;
            bullet.transform.position = transform.position + -1f * Camera.main.transform.forward - new Vector3(0, 1f, 0);
            Rigidbody ri = bullet.GetComponent<Rigidbody>();
            ri.velocity = transform.TransformDirection(new Vector3(0, 0, 20f));
            timestamp = Time.time + (timeBetweenShots - (crewGuy1 * (timeBetweenShots/10)));
        }
        if (enableToTeleport)
        {
            halo.enabled = true;
        }
        else {
            halo.enabled = false;
        }

        if(crewGuy1 > 0) effect1.SetActive(true);
        else effect1.SetActive(false);
        if (crewGuy2 > 0) effect2.SetActive(true);
        else effect2.SetActive(false);
        if (crewGuy3 > 0) effect3.SetActive(true);
        else effect3.SetActive(false);
    }

	void FixedUpdate () {
        if(crewGuy2>0)
            increaseHealth(crewGuy2*0.3f);

        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        //transform.localEulerAngles = new Vector3(0, transform.rotation.y, 0);

        dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();
        if (dir.magnitude > 1) dir.Normalize();
        if (dir.x != 0 || dir.z != 0)
        {
             //rig.AddForce(dir * ((speed / 10) + (crewGuy3 * (speed / 10))), ForceMode.VelocityChange);
            rig.velocity = dir * (speed + (crewGuy3 * (speed / 10)));
        }

        if (dir.x != 0 || dir.z != 0) anim.SetBool("isWalking", true);
        else anim.SetBool("isWalking", false);

        if (dir.x == 0 && dir.z == 0) rig.velocity = new Vector3(0, 0, 0);

        if (rotateJoystick.GetX() != 0 && rotateJoystick.GetY() != 0)
        {
            isShooting = true;
            float heading = Mathf.Atan2(rotateJoystick.GetX(), rotateJoystick.GetY());
            transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
        }
        if (rotateJoystick.GetX() == 0 && rotateJoystick.GetY() == 0) isShooting = false;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            enableToTeleport = true;
        }
        if (col.CompareTag("TeleZone") && enableToTeleport)
        {
            GameObject.FindGameObjectWithTag("Map").GetComponent<mapEvent>().Init();
            enableToTeleport = false;
        }
        if (col.CompareTag("TeleZone"))
        {
            shake = true;
        }
    }

    public void telezon()
    {
        coin.GetComponent<CoinController>().SpawnCoin();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("TeleZone"))
        {
            shake = false;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if(bloodHitTimeStamp < Time.time)
            {
                GameObject blood = Instantiate(prefabBlood, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity) as GameObject;
                blood.transform.rotation = Quaternion.Euler(90, 0, 0);
                bloodHitTimeStamp = Time.time + bloodHitCd;
            }
            increaseHealth(-1);
            if (currentHealth <= 0)
            {
                endGame();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
    
    }

    void increaseHealth(float value)
    {
        currentHealth += value;
        if (currentHealth > 100)
            currentHealth = 100;
        if (currentHealth < 0)
            currentHealth = 0;
        healthSlider.value = currentHealth;
    }

    public void increaseCrewGuy(string name, int value)
    {
        Debug.Log(name);
        if (name == "crewguy1")
        {
            crewGuy1 += value;
            //effect1.SetActive(true);
        }
        if (name == "crewguy2")
        {
            crewGuy2 += value;
            //effect2.SetActive(true);
        }
        if (name == "crewguy3")
        {
            crewGuy3 += value;
            //effect3.SetActive(true);
        }

    }

    public void endGame()
    {
        scoreText.text = "Score " + score + "/" + maxScore;
        gameOverCanvas.enabled = true;

        GameObject blood = Instantiate(prefabBlood, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity) as GameObject;
        blood.transform.rotation = Quaternion.Euler(90, 0, 0);
        bloodHitTimeStamp = Time.time + bloodHitCd;

        Destroy(gameObject);
    }

    public bool IsShake()
    {
        return shake;
    }

    public void scoreUp()
    {
        score++;
    }
}
