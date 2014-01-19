using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Move : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public int speed = 5;

    // 2 - Store the movement
    private Vector2 movement;

	private Vector2 destination;

	private GameController controller;

    private int i_direction;
    private int j_direction;

    float travel;

    void Update()
    {
        //  float chessboard = GameObject.Find("0 - Chessboard").renderer.bounds.extents.x;
        // travel = chessboard / 8;
        // 3 - Retrieve axis information
        ///  float inputX = Input.GetAxis("Horizontal");
        //  float inputY = Input.GetAxis("Vertical");

        // 4 - Movement per direction
        //   movement = new Vector2(1, 0);

        //   Debug.Log(transform.position);

    }


    void Start()
    {
		GameObject chessBoard = GameObject.Find ("0 - Chessboard");
		controller = chessBoard.GetComponent<GameController> ();
    }

    void FixedUpdate()
    {
		float currentX = renderer.bounds.center.x / 8;
		float currentY = renderer.bounds.center.y / 8;

		if (i_direction > 0 && j_direction == 0)
		{
			//moving right
			if (currentX >= destination.x) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction > 0 && j_direction < 0)
		{
			//moving bottom right
			if (currentX >= destination.x && currentY <= destination.y) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction == 0 && j_direction < 0)
		{
			//moving bottom
			if (currentY <= destination.y) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				Debug.Log ("Snapping to X: " + destination.x + " Y: " + destination.y);
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction < 0 && j_direction < 0)
		{
			//moving bottom left
			if (currentX <= destination.x && currentY <= destination.y) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction < 0 && j_direction == 0)
		{
			//moving left
			if (currentX <= destination.x) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction < 0 && j_direction > 0)
		{
			//moving top left
			if (currentX <= destination.x && currentY >= destination.y) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction == 0 && j_direction > 0)
		{
			//moving top
			if (currentY >= destination.y) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
		else if (i_direction > 0 && j_direction > 0)
		{
			//moving top right
			if (currentX >= destination.x && currentY >= destination.y) {
				rigidbody2D.velocity = new Vector2(0, 0);
				//snap it to the exact destination
				destination.x = destination.x * 8;
				destination.y = destination.y * 8;
				transform.position = destination;
				i_direction = 0;
				j_direction = 0;
				doneMoving ();
			}
		}
    }

    public void move (Vector2 destination)
    {
		controller.playerCantSelect = true;
		this.destination = destination;
        float startX = renderer.bounds.center.x;
        float startY = renderer.bounds.center.y;
        Debug.Log("Moving piece from start X: " + startX + " Y: " + startY);
        float xDistance = destination.x * 8 - startX;
        float yDistance = destination.y * 8 - startY;
        Debug.Log("Moving piece to destination X: " + destination.x + " Y: " + destination.y);
        i_direction = (int)xDistance;
        j_direction = (int)yDistance;

        if (i_direction > 0 && j_direction == 0)
        {
            //moving right
            rigidbody2D.velocity = new Vector2(speed, 0);
        }
        else if (i_direction > 0 && j_direction < 0)
        {
            //moving bottom right
            rigidbody2D.velocity = new Vector2(speed, -speed);
        }
        else if (i_direction == 0 && j_direction < 0)
        {
            //moving bottom
            rigidbody2D.velocity = new Vector2(0, -speed);
        }
        else if (i_direction < 0 && j_direction < 0)
        {
            //moving bottom left
            rigidbody2D.velocity = new Vector2(-speed, -speed);
        }
        else if (i_direction < 0 && j_direction == 0)
        {
            //moving left
            rigidbody2D.velocity = new Vector2(-speed, 0);
        }
        else if (i_direction < 0 && j_direction > 0)
        {
            //moving top left
            rigidbody2D.velocity = new Vector2(-speed, speed);
        }
        else if (i_direction == 0 && j_direction > 0)
        {
            //moving top
            rigidbody2D.velocity = new Vector2(0, speed);
        }
        else if (i_direction > 0 && j_direction > 0)
        {
            //moving top right
            rigidbody2D.velocity = new Vector2(speed, speed);
        }
    }

	public void doneMoving() {
		Debug.Log ("Done moving");
		controller.signalTurnOver ();
	}
}