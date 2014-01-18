using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	/*
	private GameObject brownParent;
	private GameObject tanParent;

	private System.Collections.ArrayList tanPieces;

	private System.Collections.ArrayList brownPieces;
	*/

	private Piece selectedPiece;
	private ArrayList validGrids;
	

	// Use this for initialization
	void Start () {
		tag = "TanTurn";
		selectedPiece = null;
		validGrids = new ArrayList ();
		/*
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
		*/
	}

	public void notifyPieceSelected(Piece piece) {
		if (selectedPiece != null) {
			selectedPiece.deselectPiece();
		}
		selectedPiece = piece;
		updateValidGrids ();
	}
	public void notifyPieceDeselected(Piece piece) {
		selectedPiece = null;
		//update valid squares
	}

	void updateValidGrids() {
		if (selectedPiece == null) {
			validGrids = new ArrayList ();
			return;
		}
		float pieceX = selectedPiece.renderer.bounds.center.x;
		float pieceY = selectedPiece.renderer.bounds.center.y;
		checkEightSurroundingGrids (pieceX, pieceY);
	}

	void checkEightSurroundingGrids(float x, float y) {
		float boardScaledWidth = renderer.bounds.extents.x;
		float boardScaledHeight = renderer.bounds.extents.y;

		float stepSizeX = boardScaledWidth / 4;
		float stepSizeY = boardScaledWidth / 4;
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				float offsetX = x + stepSizeX * i;
				float offsetY = y + stepSizeY * j;
				//detect if there is a raycast collision at the offset coordinates
				RaycastHit2D hit = Physics2D.Raycast(new Vector3(offsetX, offsetY, float.MaxValue),
				                                     new Vector3(offsetX, offsetY, float.MinValue));
				if (hit) {
					if (hit.collider.name.Equals("0 - Chessboard")) {
						//if we collide with the chessboard then it is an unoccupied space,
						//and therefore valid
						Vector2 grid = getCenterOfGridAtVector(new Vector3(offsetX, offsetY, 0));
						Debug.Log ("Adding grid to valid list at X: " + grid.x + " Y: " + grid.y);
						validGrids.Add(grid);
					}
				}
			}
		}


	}

	bool isValidGrid(Vector2 vector) {
		foreach (Vector2 validVector in validGrids) {
			if (Mathf.Abs (vector.x - validVector.x) < 0.1 &&
			    Mathf.Abs (vector.y - validVector.y) < 0.1)
			{
				Debug.Log ("Vectors are equal");
				return true;
			}
		}
		Debug.Log ("Vectors unequal");
		return false;
	}

	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown ()
	{
		Vector2 grid = getCenterOfGridAtMouse ();
		if (selectedPiece != null) {
			if (selectedPiece.cannotMove) {
				selectedPiece.deselectPiece();
				return;
			}
			if (isValidGrid (grid)) {
				selectedPiece.move (grid);
				selectedPiece.deselectPiece();
			}
			else {
				selectedPiece.deselectPiece ();
			}
		}
		Debug.Log ("Center X: " + grid.x + " Y: " + grid.y);
	}

	Vector2 getCenterOfGridAtVector(Vector3 vector) 
	{
		float xUnscaledSize = renderer.bounds.extents.x;
		float yUnscaledSize = renderer.bounds.extents.y;
		//take into account the chessboard's transform scale by dividing by 8
		float xScaledSize = (xUnscaledSize / 8);
		float yScaledSize = (yUnscaledSize / 8);
		
		////////////////////////////////////////
		
		float rawX = vector.x;
		float rawY = vector.y;
		
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
