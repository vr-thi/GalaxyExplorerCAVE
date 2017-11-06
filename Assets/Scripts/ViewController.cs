using System.Collections;
using System.Collections.Generic;
using Cave;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private GameObject origin;
    public GameObject galaxy;
    public static float speed = 1440;

    private GameObject activePlanet;
	// Use this for initialization
	void Start ()
	{
	    origin = null;
	    SelectPlanet(GameObject.Find("EarthUpClose"));
	}

    public void SelectPlanet(GameObject planet)
    {
        activePlanet = planet;
        float scale = 0.5f / planet.GetComponent<PlanetController>().radius;
        galaxy.transform.localScale = new Vector3(scale, scale, scale);
    }
	
	// Update is called once per frame
	void LateUpdate () {
	    if (origin == null)
	    {
	        origin = InstantiateNode.FindOrigin();
	    }
	    else
	    {
	        origin.transform.position = new Vector3(activePlanet.transform.position.x, activePlanet.transform.position.y - 1.3f, activePlanet.transform.position.z);
        }
	}
}
