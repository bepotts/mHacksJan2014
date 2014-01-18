using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Move : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    // 2 - Store the movement
    private Vector2 movement;

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
        float chessboard = GameObject.Find("0 - Chessboard").renderer.bounds.extents.x;
        travel = chessboard / 8;
        Debug.Log(travel);
        // 3 - Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        rigidbody2D.velocity = new Vector2(10, 0);
    }

    void FixedUpdate()
    {
        Debug.Log(transform.position / 8);
        // 5 - Move the game object    
        if (transform.position.x / 8 > travel)
        {
            rigidbody2D.velocity = new Vector2(0 , 0 );
           // movement = new Vector2(1, 0);
        }
    }
}