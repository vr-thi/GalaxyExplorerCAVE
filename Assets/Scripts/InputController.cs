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
	    viewController.SelectPlanet(planets[planetIndex]);
	}
	
	// Update is called once per frame
	void Update () {
        if (InputSynchronizer.GetKeyUp("right") && planetIndex < planets.Length - 1)
	        viewController.SelectPlanet(planets[++planetIndex]);
	    if (InputSynchronizer.GetKeyUp("left") && planetIndex > 0)
	        viewController.SelectPlanet(planets[--planetIndex]);
    }
}
