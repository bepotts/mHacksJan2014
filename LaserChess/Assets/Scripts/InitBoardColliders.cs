using UnityEngine;
using System.Collections;


public class InitBoardColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float xUnscaledSize = renderer.bounds.extents.x;
		float yUnscaledSize = renderer.bounds.extents.y;

		//gives the length of one half of the board
		float xScaledSize = (xUnscaledSize / 8);
		float yScaledSize = (yUnscaledSize / 8);

		BoxCollider2D collider = (BoxCollider2D) gameObject.AddComponent (typeof(BoxCollider2D));
		collider.center = new Vector3(0, 0, -20);
		collider.size = new Vector3 (xScaledSize * 2, xScaledSize * 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
