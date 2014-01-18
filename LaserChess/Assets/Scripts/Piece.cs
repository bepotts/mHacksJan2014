using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

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
		//move
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
