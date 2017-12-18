using System.Collections;
using System.Collections.Generic;
using Cave;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject[] planets;
    public int planetIndex;
    public ViewController viewController;

	void Start ()
	{
		viewController.SelectPlanet(planets[planetIndex], planets);
	}

	public void SelectPlanet(GameObject planetToSelect)
	{
		int newPlanetIndex = 0;
		foreach (GameObject planet in planets)
		{
			if (planet == planetToSelect) {
				planetIndex = newPlanetIndex;
				viewController.ZoomIn (planet, planets);
				break;
			}
			newPlanetIndex ++;
		}
	}

	// Update is called once per frame
	void Update () {
		if (ViewController.logDistance == 0 && (InputSynchronizer.GetKeyUp("right") || InputSynchronizer.GetKeyUp("flystick 1")) && planetIndex < planets.Length - 1)
			viewController.SelectPlanet(planets[++planetIndex], planets);
		if (ViewController.logDistance == 0 && (InputSynchronizer.GetKeyUp("left") || InputSynchronizer.GetKeyUp("flystick 3")) && planetIndex > 0)
			viewController.SelectPlanet(planets[--planetIndex], planets);
		if (InputSynchronizer.GetKeyUp ("down") || InputSynchronizer.GetKeyUp ("flystick 2")) {
			if (ViewController.logDistance > 0)
				viewController.ZoomIn (planets[planetIndex], planets);
			else
				viewController.ZoomOut (planets);
		}

		viewController.ChangeSpeed(InputSynchronizer.GetAxis ("flystick horizontal") * TimeSynchronizer.deltaTime * 10 + 1);
    }
}
