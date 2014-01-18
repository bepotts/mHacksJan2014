using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject brownParent;
	private GameObject tanParent;

	private System.Collections.ArrayList tanPieces;

	private System.Collections.ArrayList brownPieces;

	private string selectedPiece;
	

	// Use this for initialization
	void Start () {
		tag = "TanTurn";

		tanPieces = new ArrayList ();
		brownPieces = new ArrayList ();

		brownParent = GameObject.Find ("Brown");
		tanParent = GameObject.Find ("Tan");

		foreach (Transform child in brownParent.transform) {
			child.tag = "Selected:False";
			brownPieces.Add (child);
			Debug.Log (child.name);
		}
		foreach (Transform child in tanParent.transform) {
			child.tag = "Selected:False";
			tanPieces.Add(child);
			Debug.Log (child.name);
		}
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
