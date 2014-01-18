using UnityEngine;
using System.Collections;


public class InitBoardColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float xUnscaledSize = renderer.bounds.extents.x;
		float yUnscaledSize = renderer.bounds.extents.y;

		Debug.Log ("unscaled size X: " + xUnscaledSize + " Y: " + yUnscaledSize);

		float xTransform = transform.position.x;
		float yTransform = transform.position.y;

		Debug.Log ("transform X: " + xTransform + " Y: " + yTransform);

		//gives the length of one half of the board
		float xScaledSize = (xUnscaledSize / 8);
		float yScaledSize = (yUnscaledSize / 8);

		//step gives the length of one square on the board
		float xStep = xScaledSize / 4;
		float yStep = yScaledSize / 4;


				BoxCollider2D collider = (BoxCollider2D) gameObject.AddComponent (typeof(BoxCollider2D));
				collider.center = new Vector3(0, 0, 0);
				collider.size = new Vector3 (xScaledSize * 2, xScaledSize * 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown ()
	{
		Vector2 grid = getCenterOfGridAtMouse ();
		Debug.Log ("Center X: " + grid.x + " Y: " + grid.y);
	}

	Vector2 getCenterOfGridAtMouse() {
		float xUnscaledSize = renderer.bounds.extents.x;
		float yUnscaledSize = renderer.bounds.extents.y;
		//take into account the chessboard's transform scale by dividing by 8
		float xScaledSize = (xUnscaledSize / 8);
		float yScaledSize = (yUnscaledSize / 8);

		////////////////////////////////////////
		Vector3 pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition );
		
		float rawX = pointer.x;
		float rawY = pointer.y;
		
		float ratio = xScaledSize / 4;

		//Gives us coordinates in an even (-4, 4) scale
		float scaledX = rawX / 8 / ratio;
		float scaledY = rawY / 8 / ratio;
		//which we use to calculate the position of the innermost edge of the grid at the mouse position
		float floorX = Mathf.Floor (scaledX);
		float floorY = Mathf.Floor (scaledY);
		//add 0.5 to get the center of the grid...
		floorX += 0.5f;
		floorY += 0.5f;
		//and return it with the original scale
		return new Vector2 (floorX * ratio, floorY * ratio);
	}
}
