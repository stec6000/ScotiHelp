using UnityEngine;
using System.Collections;

public class mapPoint : MonoBehaviour {

    public string crewGuyName = "Crewguy1";
    public Vector3 appearingPlace = new Vector3(0, 0.5f, 0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().enabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject.FindGameObjectWithTag("Map").GetComponent<mapEvent>().click(hit.point);
                GameObject.FindGameObjectWithTag("Map").GetComponent<mapEvent>().setShaking(true);
                GameObject guy = (GameObject)Instantiate(Resources.Load(crewGuyName) as GameObject, new Vector3(0, 0.5f, 14.1f), Quaternion.identity);
                guy.GetComponent<CrewGuy>().target = GameObject.FindGameObjectWithTag("Player").transform;
                guy.GetComponent<CrewGuy>().Endtarget = GameObject.FindGameObjectWithTag("EndPoint").transform;
                Destroy(gameObject);
            }
        }
    }
}
