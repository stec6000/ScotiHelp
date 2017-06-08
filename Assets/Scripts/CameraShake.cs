using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;

    public GameObject player;
    private PlayerController playerCtrl;
    public mapEvent mapCtrl;

    public float shakeTimer;
    public float shakeAmout;

    private Vector3 offset;

    void Start()
    {
        playerCtrl = player.GetComponent<PlayerController>();
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
    }

	void FixedUpdate () {
        float posX = Mathf.SmoothDamp(transform.position.x, transform.position.y, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, transform.position.y, ref velocity.y, smoothTimeY);

        //transform.position = new Vector3(posX, posY, transform.position.z);

    }

    void Update()
    {
        if(shakeTimer > 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmout;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;

        }
        else transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);


        if (mapCtrl.IsShaking())
        {
            ShakeCamera(0.1f, 0.1f);
        }
    }


    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmout = shakePwr;
        shakeTimer = shakeDur;
    }
}
