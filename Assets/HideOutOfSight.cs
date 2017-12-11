using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cave;

public class HideOutOfSight : MonoBehaviour {

	private enum State
	{
		Hidden,
		TransitionToVisible,
		Visible,
		TransitionToHidden
	}

	private Collider collider;
	private State state;
	private float alpha;
	public float speed = 0.1f;
	private LineRenderer[] lineRenderer;
	private TextMesh[] textMeshes;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider> ();	
		state = State.Hidden;
		alpha = 0;
		textMeshes = GetComponentsInChildren<TextMesh> ();
		lineRenderer = GetComponentsInChildren<LineRenderer> ();
		RefreshAlpha ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject cameraHolder = GameObject.Find ("CameraHolder");
		if (cameraHolder != null) {
			Ray ray = new Ray(cameraHolder.transform.position, cameraHolder.transform.forward);
			RaycastHit hit;
			bool inSight = collider.Raycast (ray, out hit, 1);

			if ((state == State.Hidden || state == State.TransitionToHidden) && inSight) {
				state = State.TransitionToVisible;
			} else if ((state == State.Visible || state == State.TransitionToVisible) && !inSight) {
				state = State.TransitionToHidden;
			}

			if (state == State.TransitionToHidden) {
				alpha -= speed * TimeSynchronizer.deltaTime;
				if (alpha <= 0) {
					alpha = 0;
					state = State.Hidden;
				}				
				RefreshAlpha ();
			} else if (state == State.TransitionToVisible) {
				alpha += speed * TimeSynchronizer.deltaTime;
				if (alpha >= 1) {
					alpha = 1;
					state = State.Visible;
				}				
				RefreshAlpha ();
			}
		}
	}

	private void RefreshAlpha() 
	{
		foreach (LineRenderer singleLineRenderer in lineRenderer) {
			singleLineRenderer.material.color = new Color (1, 1, 1, alpha);
		}

		foreach (TextMesh textMesh in textMeshes) {
			textMesh.color = new Color (1, 1, 1, alpha);
		}
	}
}
