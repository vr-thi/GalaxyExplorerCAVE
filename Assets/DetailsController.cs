using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cave;

public class DetailsController : InteractionTrigger {
	private Renderer renderer;
	private PlanetController planetController;
	private bool selected;
	private float transitionTime;
	private float totalTransitionTime;
	private TextMesh descTextMesh;
	private string description;
	private InputController inputController;
	// Use this for initialization
	void Start () {
		totalTransitionTime = 1;
		renderer = GetComponent<Renderer> ();
		planetController = transform.parent.GetComponent<PlanetController> ();
		TextMesh nameTextMesh = transform.GetChild(0).FindChild("Name").GetComponent<TextMesh>();
		nameTextMesh.text = planetController.name; 
		descTextMesh = transform.GetChild(0).FindChild("Description").GetComponent<TextMesh>();
		description = "";
		description += "\n\nRadius:\n" + (planetController.radius * 1000000).ToString ("N") + " km"; 
		description += "\n\nUmlaufperiode:\n" + planetController.orbitalPeriod.ToString ("N") + " Jahre"; 
		description += "\n\nRotationsperiode:\n" + planetController.rotationPeriod.ToString ("N") + " Tage"; 

		inputController = GameObject.Find ("Controller").GetComponent<InputController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (InputSynchronizer.GetKeyUp ("flystick 0")) {
			if (selected) {
				if (ViewController.logDistance > 0) {
					inputController.SelectPlanet (transform.parent.gameObject);
				} else {
					transform.GetChild (0).gameObject.SetActive (true);
					descTextMesh.text = "";
					transitionTime = 0;
				}
			} else {
				transform.GetChild (0).gameObject.SetActive (false);
				transitionTime = totalTransitionTime;
			}
		}

		if (transitionTime < totalTransitionTime) {
			transitionTime = Mathf.Min(totalTransitionTime, transitionTime + TimeSynchronizer.deltaTime);
			descTextMesh.text = description.Substring (0, (int)(transitionTime / totalTransitionTime * description.Length));
		}
	}

	public override void OnFlyStickEnter() {
		renderer.material.SetColor ("_AlbedoMultiplier", new Color (0.7f, 0.7f, 0.7f));
		selected = true;
	}

	public override void OnFlyStickExit() {
		renderer.material.SetColor ("_AlbedoMultiplier", new Color (1, 1, 1));
		selected = false;
	}
}
