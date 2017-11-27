using System.Collections;
using System.Collections.Generic;
using Cave;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private GameObject origin;
    public GameObject galaxy;
    public static float speed = 1440;
	public static float logDistance = 0;
	public static float logSize = 0;
	public static float sizeFac = 1;

    private GameObject activePlanet;
	// Use this for initialization
	void Start ()
	{
	    origin = null;
	}

	public void SelectPlanet(GameObject planet, GameObject[] planets)
    {
        activePlanet = planet;
		float maxRadius = 0;
		float minRadius = 0;
		foreach (GameObject currentPlanet in planets) {
			float radius = currentPlanet.GetComponent<PlanetController> ().radius;
			if (maxRadius < radius)
				maxRadius = radius;
			if (minRadius == 0 || minRadius > radius)
				minRadius = radius;
		}

		float radiusScale = (planet.GetComponent<PlanetController> ().radius - minRadius) / (maxRadius - minRadius);
		radiusScale = Mathf.Pow (radiusScale, 1 / 10.0f);

		float scale = 0.5f / planet.GetComponent<PlanetController>().radius * radiusScale;
        galaxy.transform.localScale = new Vector3(scale, scale, scale);
	}

	public void ZoomOut	(GameObject[] planets)
	{
		activePlanet = null;
		logDistance = 10.0f;
		logSize = 100000.0f;
		sizeFac = 0.25f;
		float scale = 1.0f / planets[planets.Length - 1].GetComponent<PlanetController>().getScaledSemiMajorAxis();
		speed = 1440 * 365;
		galaxy.transform.localScale = new Vector3(scale, scale, scale);
	}

	public void ZoomIn(GameObject planet, GameObject[] planets)
	{
		speed = 1440;	
		logDistance = 0;
		logSize = 0;
		sizeFac = 1;
		SelectPlanet (planet, planets);
	}

	public void ChangeSpeed(float fac)
	{
		speed *= fac;
		speed = Mathf.Max(1, Mathf.Min(10000000, speed));
	}

	// Update is called once per frame
	void LateUpdate () {
		if (origin == null) {
			origin = InstantiateNode.FindOrigin ();
		} else if (activePlanet != null) {
			origin.transform.position = new Vector3 (activePlanet.transform.position.x, activePlanet.transform.position.y - 1.3f, activePlanet.transform.position.z);
		} else {
			origin.transform.position = new Vector3 (0, -1.3f, 0);
		}
	}
}
