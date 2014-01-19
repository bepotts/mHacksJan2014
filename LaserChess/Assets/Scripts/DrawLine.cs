using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	public GameObject beam;
	public float lineDrawSpeed = 5f;

	private ArrayList events;
	private int index;

	// Use this for initialization
	void Start () {
		events = new ArrayList ();
	}

	public void fireLaser(ArrayList transforms) {
		fireLaser (0, 1, transforms);
	}

	private void fireLaser(int orgIndex, int destIndex, ArrayList transforms) {
		if (orgIndex >= transforms.Count || destIndex >= transforms.Count) {
			if (events.Count == 0) {
				Debug.Log ("No more fire events");
			}
			return;
		}
		if (transforms [orgIndex] is ArrayList) {
			return;
		}
		if (transforms[destIndex] is ArrayList) {
			fireLaser (0, 1, (ArrayList)transforms[destIndex]);
			fireLaser (destIndex + 1, destIndex + 2, transforms);
			return;
		}
		Vector3 origin = (Vector3)transforms [orgIndex];
		Vector3 destination = (Vector3)transforms [destIndex];
		fireLaser (origin, destination, orgIndex, destIndex, transforms);
	}

	//Because we need to be able to render multiple lasers at once, we'll keep the relevent information
	//of each fire event in an array and loop through them in the same frame.
	public void fireLaser (Vector3 fromOrigin, Vector3 toDestination, int orgIndex, int destIndex, ArrayList transforms)
	{
		fromOrigin = new Vector3 (fromOrigin.x, fromOrigin.y, -1);
		toDestination = new Vector3 (toDestination.x, toDestination.y, -1);
		FireEvent fireEvent = new FireEvent(fromOrigin, toDestination, orgIndex, destIndex, transforms, beam);
		events.Add (fireEvent);
	}
	
	void Update () {
		Debug.Log (events.Count);
		for (int i = 0; i < events.Count; i++) {
			FireEvent fireEvent = (FireEvent) events[i];
			if (fireEvent.firing == false) {
				//events.Remove (fireEvent);
				//i--;
				continue;
			}
			if (fireEvent.counter  < fireEvent.dist && fireEvent.lineRenderer != null) {
				fireEvent.counter += 1f / lineDrawSpeed;
				float x = fireEvent.counter;
				Vector3 pointA = fireEvent.origin;
				Vector3 pointB = fireEvent.destination;
				Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;
				fireEvent.lineRenderer.transform.position = pointAlongLine;
				fireEvent.lineRenderer.SetPosition (1, pointAlongLine);
			}
			else {
				Debug.Log ("reached end of fire event");
				fireLaser (fireEvent.originIndex + 1, fireEvent.destinationIndex + 1, fireEvent.transforms);
				fireEvent.firing = false;
			}
		}
		if (events.Count == 0) {
			//Debug.Log ("No more fire events");
		}
	}

	public class FireEvent {
		public LineRenderer lineRenderer;
		public ArrayList transforms;
		public Vector3 origin;
		public Vector3 destination;
		public int originIndex;
		public int destinationIndex;
		public bool firing;
		public float counter;
		public float dist;

		public FireEvent(Vector3 origin, Vector3 destination, int originIndex, int destinationIndex,
		                 ArrayList transforms, GameObject beam) {
			this.origin = origin;
			this.destination = destination;
			this.originIndex = originIndex;
			this.destinationIndex = destinationIndex;
			this.transforms = transforms;
			this.firing = true;
			GameObject renderer = (GameObject)Instantiate(beam, origin, Quaternion.identity);
			Destroy (renderer, 5f);
			lineRenderer = renderer.GetComponent<LineRenderer>();
			lineRenderer.SetPosition (0, origin);
			lineRenderer.SetWidth (0.45f, 0.45f);
			this.dist = Vector3.Distance (origin, destination);
		}
	}

}
