using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	public GameObject beam;
	private float counter;
	private float dist;
	private bool firing;

	public Transform origin;
	public Transform destination;

	public float lineDrawSpeed = 0.2f;

	private ArrayList events;

	// Use this for initialization
	void Start () {
		events = new ArrayList ();
	}

	//Because we need to be able to render multiple lasers at once, we'll keep the relevent information
	//of each fire event in an array and loop through them in the same frame.
	public void fireLaser (Transform fromOrigin, Transform toDestination)
	{
		fromOrigin.position = new Vector3 (fromOrigin.position.x, fromOrigin.position.y, -4);
		toDestination.position = new Vector3 (toDestination.position.x, toDestination.position.y, -4);
		FireEvent fireEvent = new FireEvent(fromOrigin, toDestination, beam);
		events.Add (fireEvent);
	}
	
	void FixedUpdate () {
		foreach (FireEvent fireEvent in events) {
			if (fireEvent.counter < fireEvent.dist) {
				fireEvent.counter += .1f / lineDrawSpeed;
				float x = Mathf.Lerp (0, fireEvent.dist, fireEvent.counter);
				Vector3 pointA = fireEvent.origin.position;
				Vector3 pointB = fireEvent.destination.position;
				Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;
				fireEvent.lineRenderer.SetPosition (1, pointAlongLine);
			}
		}
	}

	public class FireEvent {
		public LineRenderer lineRenderer;
		public Transform origin;
		public Transform destination;
		public float counter;
		public float dist;

		public FireEvent(Transform origin, Transform destination, GameObject beam) {
			this.origin = origin;
			this.destination = destination;
			GameObject renderer = (GameObject)Instantiate(beam, origin.position, Quaternion.identity);
			lineRenderer = renderer.GetComponent<LineRenderer>();
			lineRenderer.SetPosition (0, origin.position);
			lineRenderer.SetWidth (0.45f, 0.45f);
			this.dist = Vector3.Distance (origin.position, destination.position);
		}
	}

}
