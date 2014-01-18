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

		//step gives the length of one square on the board
		float xStep = xScaledSize / 4;
		float yStep = yScaledSize / 4;

		//create an 8x8 grid of colliders, corresponding to the chess squares on the board
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				BoxCollider2D collider = (BoxCollider2D) gameObject.AddComponent (typeof(BoxCollider2D));
				collider.center = new Vector3(-xScaledSize + (xStep / 2) + xStep * i, -yScaledSize + (yStep / 2) + yStep * j, 0);
				collider.size = new Vector3 (xStep, yStep, 0);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown ()
	{
		Debug.Log ("Clicked at mouse X: " + Input.mousePosition.x + " Y: " + Input.mousePosition.y);
	} 
}
