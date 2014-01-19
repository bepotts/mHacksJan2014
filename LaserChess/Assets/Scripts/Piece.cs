using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

	public enum PieceType {Triangle, Square, Splitter, King, Laser};

	public PieceType type;
	public Sprite texture;
	public Sprite selectedTexture;
	public bool isSelected;
	public bool cannotMove;
	public string side;

	private GameController chessBoard;
	

	// Use this for initialization
	void Start () {
		isSelected = false;
		chessBoard = GameObject.Find ("0 - Chessboard").GetComponent<GameController>();
		side = transform.parent.name;
	}

	public void selectPiece() {
		isSelected = true;
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = selectedTexture;
		chessBoard.notifyPieceSelected(GetComponent<Piece>());
	}
	public void deselectPiece() {
		isSelected = false;
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = texture;
		chessBoard.notifyPieceDeselected(GetComponent<Piece>());
	}

	public void move(Vector2 vector) {
        Move moveScript = GetComponent<Move>();
        moveScript.move(vector);
	}

	public void kill() {
		//kill
	}

	//This method assumes that the incoming laser is not fatal, so do not call this method
	//before calling determineIfFatalHit(). This also does not handle splitter logic, but will
	//return the straight branch of the splitter if the given piece is a splitter.
	public float determineReflectedLaserPath(float incomingDirection) {
		Debug.Log ("Determining reflected laser path from incoming: " + incomingDirection);
		float facing = transform.rotation.eulerAngles.z;
		if (type == PieceType.Square) {
			Debug.Log ("Square detected, reflecting back");
			return ((int)(incomingDirection - 180) % 360);
		}
		if (type == PieceType.Triangle) {
			if (Mathf.Abs(((int)(incomingDirection + 180) % 360) - facing) < 2) {
				Debug.Log ("Triangle detected, adding 90 degrees to trajectory path");
				//add 90 to incoming direction
				return ((int)incomingDirection + 90) % 360;
			}
			else {
				Debug.Log ("Triangle detected, subtracting 90 degrees to trajectory path");
				//subtract 90
				return ((int)incomingDirection - 90 + 360) % 360;
			}
		}
		if (type == PieceType.Splitter) {
			return incomingDirection;
		}
		return 0;
	}

	//Only call this method on splitter types
	public float determineSplitLaserPath(float incomingDirection) {
		float facing = transform.rotation.eulerAngles.z;
		if ((facing < 2 || facing - 180 < 2) && (incomingDirection < 2 || incomingDirection - 180 < 2)) {
			float splitDirection = (incomingDirection + 270) % 360;
			Debug.Log ("Creating split laser path by subtracting 90, new direction: " + splitDirection);
			return splitDirection;
		}
		else {
			//is at the 0 or 180 degree position
			float splitDirection = (incomingDirection + 90) % 360;
			Debug.Log ("Creating split laser path by adding 90, new direction: " + splitDirection);
			return splitDirection;
		}
	}

	public bool determineIfFatalHit(float incomingDirection) {
		int facing = (int)transform.rotation.eulerAngles.z;
		switch (type)
		{
		case (PieceType.Triangle):
			if (Mathf.Abs((int)(facing + 180) % 360 - incomingDirection) > 2 &&
			    Mathf.Abs((int)(facing + 90) % 360 - incomingDirection) > 2) {

				//Incoming laser isn't directly opposite the mirror or to its right, so it dies
				return true;
			}
			break;
		case (PieceType.Square):
			if (Mathf.Abs(((incomingDirection - 180) % 360) - facing) > 2) {
				//Incoming laser isn't directly opposite the mirror, so it dies
				return true;
			}
			break;
		case(PieceType.King):
			return true;
		case(PieceType.Laser):
			return false;
		case(PieceType.Splitter):
			return false;
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		string currentTurn = chessBoard.tag;
		if (currentTurn.Equals("TanTurn") && side.Equals ("Brown")) {
			return;
		}
		if (currentTurn.Equals ("BrownTurn") && side.Equals ("Tan")) {
			return;
		}
		if (isSelected) {
			deselectPiece();
		}
		else {
			selectPiece ();
		}
	}
}
