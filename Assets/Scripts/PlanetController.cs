using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cave;

public class PlanetController : MonoBehaviour
{
    public float semiMajorAxis;
    public float eccentricity;
    public float orbitalPeriod;
    public float radius;
	public float rotationPeriod;
	public bool rotateBackwards;
	public float axisRotation;
	public Transform orbitCenter;

	private float orbitAngle;
	private float rotationAngle;

    private float realTimeSpeedFac = 1.0f / (365 * 24 * 3600);
	private float offsetTime;
    void Start ()
    {
		advance (1000);
    }

	public float getScaledSemiMajorAxis()
	{
		return ViewController.logDistance > 0 ? Mathf.Log(semiMajorAxis) / Mathf.Log(ViewController.logDistance) : semiMajorAxis;
	}

	public float getScaledRadius()
	{
		return ViewController.logSize > 0 ? Mathf.Log(10000 * radius) / Mathf.Log(ViewController.logSize) * ViewController.sizeFac : radius;
	}
	
	void Update ()
	{
		SphereCollider collider = this.transform.GetComponent<SphereCollider>();
		if (collider == null)
			collider = this.transform.GetChild(0).GetComponent<SphereCollider>();
		float scale = getScaledRadius() / (collider.radius * collider.transform.localScale.x);
		this.transform.localScale = new Vector3(scale, scale, scale);

		advance (TimeSynchronizer.deltaTime);
	}

	private void advance(float time)
	{
		if (orbitalPeriod > 0)
		{
			orbitAngle += ViewController.speed * realTimeSpeedFac * time / orbitalPeriod * Mathf.PI * 2;
			float rad = getScaledSemiMajorAxis() * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos(orbitAngle));
			this.transform.localPosition = new Vector3(rad * Mathf.Cos(orbitAngle), 0, rad * Mathf.Sin(orbitAngle));
			if (orbitCenter != null)
				this.transform.localPosition += orbitCenter.localPosition;
		}
		if (rotationPeriod > 0)
		{
			rotationAngle += ViewController.speed * realTimeSpeedFac * time / rotationPeriod * 360 * 365 * (rotateBackwards ? 1 : -1);
			Vector3 rotationAxis = Quaternion.Euler (axisRotation, 0, 0) * Vector3.up;
			this.transform.rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis) * Quaternion.Euler(axisRotation, 0, 0) ;
		}
	}
}
