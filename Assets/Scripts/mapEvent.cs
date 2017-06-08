using UnityEngine;
using System.Collections;

public class mapEvent : MonoBehaviour {
    private GameObject mapClick;
    private GameObject mapClickObject;
    private Animation animation;
    private bool isShaking;

    void Start () {
        mapClick = Resources.Load("mapClick") as GameObject;
        isShaking = false;
    }

    void Update ()
    {
        if (mapClickObject)
        {
            if (animation.isPlaying)
            {
                AnimationState currentState = animation[animation.clip.name];
                currentState.time += Time.unscaledDeltaTime;
                animation.Sample();
            }
        }

        if (mapClickObject)
        {
            if (!animation.IsPlaying("mapClick"))
            {
                isShaking = false;
                Destroy(mapClickObject);
                GetComponent<SpriteRenderer>().enabled = false;
                setPoints(false);
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().telezon();
            }
        }
    }

    void OnMouseDown()
    {
        /*if (GetComponent<SpriteRenderer>().enabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mapClickObject = Instantiate(mapClick, hit.point, Quaternion.Euler(90, 0, 0)) as GameObject;
                animation = mapClickObject.GetComponent<Animation>();
            }
        }*/
    }

    public void click(Vector3 point)
    {
        mapClickObject = Instantiate(mapClick, point, Quaternion.Euler(90, 0, 0)) as GameObject;
        animation = mapClickObject.GetComponent<Animation>();
    }

    public void Init ()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
        setPoints(true);
    }

    private void setPoints(bool value)
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("mapPoint");
        foreach (GameObject element in points)
        {
            element.GetComponent<SpriteRenderer>().enabled = value;
        }
    }
    public bool IsShaking()
    {
        return isShaking;
    }
    public void setShaking(bool value)
    {
        isShaking = value;
    }
}
