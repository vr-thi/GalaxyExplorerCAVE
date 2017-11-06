using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public float semiMajorAxis;
    public float eccentricity;
    public float orbitalPeriod;
    public float radius;
    public float rotationPeriod;
    private float realTimeSpeedFac = 1.0f / (365 * 24 * 3600);
    void Start ()
    {
        SphereCollider collider = this.transform.GetChild(0).GetComponent<SphereCollider>();
        float scale = radius / (collider.radius * collider.transform.localScale.x);
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
	
	void Update ()
	{
	    if (orbitalPeriod > 0)
	    {
	        float angle = ViewController.speed * realTimeSpeedFac * Time.time / orbitalPeriod * Mathf.PI * 2;
	        float rad = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos(angle));
	        this.transform.localPosition = new Vector3(rad * Mathf.Cos(angle), 0, rad * Mathf.Sin(angle));
	    }
	    if (rotationPeriod > 0)
	    {
	        float angle = ViewController.speed * realTimeSpeedFac * Time.time / rotationPeriod * 360 * 365;
	        this.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

	}
}
