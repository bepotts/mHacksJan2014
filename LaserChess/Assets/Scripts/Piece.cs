using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

	public Sprite texture;
	public Sprite selectedTexture;
	public bool isSelected;

	private GameObject chessBoard;

	private string side;
	

	// Use this for initialization
	void Start () {
		isSelected = false;
		chessBoard = GameObject.Find ("0 - Chessboard");
		side = transform.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		Debug.Log (transform.parent.name);
		string currentTurn = chessBoard.tag;
		if (currentTurn.Equals("TanTurn") && side.Equals ("Brown")) {
			return;
		}

		if (currentTurn.Equals ("BrownTurn") && side.Equals ("Tan")) {
			return;
		}

		if (isSelected) {
			isSelected = false;
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
			sr.sprite = texture;
		}
		else {
			isSelected = true;
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
			sr.sprite = selectedTexture;
		}
	}
}
