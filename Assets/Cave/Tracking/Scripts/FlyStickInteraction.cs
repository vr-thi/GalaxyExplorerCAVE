using UnityEngine;
using System.Collections;


public class FlyStickInteraction : MonoBehaviour
{
    private RaycastHit rHit;
    private LineRenderer lineRender;
	private InteractionTrigger activeInteractionTrigger;
    private TrackerSettings trackerSettings;

    public Transform origin;
    public Transform dest;
    public float maxRayDist = 10f;


    public TrackerSettings TrackerSettings
    {
        get
        {
            return trackerSettings;
        }

        set
        {
            trackerSettings = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        trackerSettings = GetComponent<TrackerSettings>();
		activeInteractionTrigger = null;
    }

    // Update is called once per frame
    void Update()
    {
		SendRay();
        DrawLaser();
    }

    public void SendRay()
    {
		InteractionTrigger interactionTrigger = null;
		if (Physics.Raycast(transform.position, transform.forward, out rHit, maxRayDist))
			interactionTrigger = rHit.collider.gameObject.GetComponent<InteractionTrigger>();

		if (interactionTrigger != activeInteractionTrigger) {
			if (activeInteractionTrigger != null)
				activeInteractionTrigger.OnFlyStickExit ();

			if (interactionTrigger != null)
				interactionTrigger.OnFlyStickEnter ();

			activeInteractionTrigger = interactionTrigger;
		}			
    }
    
    private void DrawLaser()
    {
        lineRender.SetPosition(0, origin.position);
        lineRender.SetPosition(1, dest.position);
    }

 }

