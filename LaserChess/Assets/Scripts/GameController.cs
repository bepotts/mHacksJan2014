using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    /*
    private GameObject brownParent;
    private GameObject tanParent;

    private System.Collections.ArrayList tanPieces;

    private System.Collections.ArrayList brownPieces;
    */
    private static float NORTH = 0f;
    private static float WEST = 90f;
    private static float SOUTH = 180f;
    private static float EAST = 270f;

    private Piece selectedPiece;
    private ArrayList validGrids;

    GameObject brownLaser;
    GameObject tanLaser;

    private DrawLine beam;

    // Use this for initialization
    void Start()
    {
        tag = "TanTurn";
        selectedPiece = null;
        validGrids = new ArrayList();

        brownLaser = GameObject.Find("brownLaser");

beam = GameObject.Find("brownBeam").GetComponent<DrawLine>();
        getLaserPath(brownLaser.transform, brownLaser.transform.rotation.eulerAngles.z, new ArrayList());
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

    public void notifyPieceSelected(Piece piece)
    {
        if (selectedPiece != null)
        {
            beam.fireLaser(selectedPiece.transform, piece.transform);
            selectedPiece.deselectPiece();
        }
        selectedPiece = piece;
        updateValidGrids();
    }
    public void notifyPieceDeselected(Piece piece)
    {
        selectedPiece = null;
        updateValidGrids();
    }

    void updateValidGrids()
    {
        if (selectedPiece == null)
        {
            validGrids = new ArrayList();
            return;
        }
        float pieceX = selectedPiece.renderer.bounds.center.x;
        float pieceY = selectedPiece.renderer.bounds.center.y;
        checkEightSurroundingGrids(pieceX, pieceY);
    }

    void checkEightSurroundingGrids(float x, float y)
    {
        float boardScaledWidth = renderer.bounds.extents.x;
        float boardScaledHeight = renderer.bounds.extents.y;

        float stepSizeX = boardScaledWidth / 4;
        float stepSizeY = boardScaledWidth / 4;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                float offsetX = x + stepSizeX * i;
                float offsetY = y + stepSizeY * j;
                //detect if there is a raycast collision at the offset coordinates
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(offsetX, offsetY, float.MaxValue),
                                                     new Vector3(offsetX, offsetY, float.MinValue));
                if (hit)
                {
                    if (hit.collider.name.Equals("0 - Chessboard"))
                    {
                        //if we collide with the chessboard then it is an unoccupied space,
                        //and therefore valid
                        Vector2 grid = getCenterOfGridAtVector(new Vector3(offsetX, offsetY, 0));
                        Debug.Log("Adding grid to valid list at X: " + grid.x + " Y: " + grid.y);
                        validGrids.Add(grid);
                    }
                }
            }
        }


    }

    bool isValidGrid(Vector2 vector)
    {
        foreach (Vector2 validVector in validGrids)
        {
            if (Mathf.Abs(vector.x - validVector.x) < 0.1 &&
                Mathf.Abs(vector.y - validVector.y) < 0.1)
            {
                Debug.Log("Vectors are equal");
                return true;
            }
        }
        Debug.Log("Vectors unequal");
        return false;
    }

    //returns a list of transforms that make up a side's laser trajectory
    ArrayList getLaserPath(Transform transform, float projectileDirection, ArrayList transforms)
    {
        Debug.Log("Firing raycast");
        Vector3 origin = transform.position;
        RaycastHit2D hit = fireRayCast(origin, projectileDirection);
        if (hit)
        {
            Debug.Log("Hit " + hit.collider.name);
            transforms.Add(hit.collider.transform);
            Piece piece = hit.collider.transform.GetComponent<Piece>();
            if (piece.determineIfFatalHit(projectileDirection))
            {
                Debug.Log("Piece killed");
                //do nothing for now, will kill during the actual laser firing
                return transforms;
            }
            if (piece.type == Piece.PieceType.Laser)
            {
                //do nothing
                return transforms;
            }
            if (piece.type == Piece.PieceType.Splitter)
            {
                //handle splitter
                Debug.Log("Hit splitter");
                return transforms;
            }
            float redirectedProjectile = piece.determineReflectedLaserPath(projectileDirection);
            Debug.Log("Got redirected projectile with angle: " + redirectedProjectile);
            getLaserPath(hit.collider.transform, redirectedProjectile, transforms);
        }
        return transforms;
    }

    RaycastHit2D fireRayCast(Vector3 origin, float facing)
    {
        if (Mathf.Abs(facing - EAST) < 2)
        {
            Debug.Log("Firing east");
            //Facing in the positive X direction
            //offset the origin from the originating piece so the raycast doesn't hit it
            origin.x = origin.x + renderer.bounds.extents.x / 4;
            Vector3 direction = new Vector3(Screen.width, origin.y, origin.z);
            return Physics2D.Raycast(origin, direction, float.MaxValue);
        }
        else if (Mathf.Abs(facing - NORTH) < 2)
        {
            Debug.Log("Firing north");
            //Facing in the positive Y direction
            //offset the origin from the originating piece so the raycast doesn't hit it
            origin.y = origin.y + renderer.bounds.extents.y / 4;
            Vector3 direction = new Vector3(origin.x, Screen.height, origin.z);
            return Physics2D.Raycast(origin, direction, float.MaxValue);
        }
        else if (Mathf.Abs(facing - SOUTH) < 2)
        {
            Debug.Log("Firing south");
            //Facing in the negative Y direction
            //offset the origin from the originating piece so the raycast doesn't hit it
            origin.y = origin.y - renderer.bounds.extents.y / 4;
            Vector3 direction = new Vector3(origin.x, -Screen.height, origin.z);
            return Physics2D.Raycast(origin, direction, float.MaxValue);
        }
        else
        {
            Debug.Log("Firing west");
            //facing in the negative X direction
            //offset the origin from the originating piece so the raycast doesn't hit it
            origin.x = origin.x - renderer.bounds.extents.x / 4;
            Vector3 direction = new Vector3(-Screen.width, origin.y, origin.z);
            return Physics2D.Raycast(origin, direction, float.MaxValue);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Vector2 grid = getCenterOfGridAtMouse();
        if (selectedPiece != null)
        {
            if (selectedPiece.cannotMove)
            {
                selectedPiece.deselectPiece();
                return;
            }
            if (isValidGrid(grid))
            {
                selectedPiece.move(grid);
                selectedPiece.deselectPiece();
            }
            else
            {
                selectedPiece.deselectPiece();
            }
        }
        Debug.Log("Center X: " + grid.x + " Y: " + grid.y);
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
        float floorX = Mathf.Floor(scaledX);
        float floorY = Mathf.Floor(scaledY);
        //add 0.5 to get the center of the grid...
        floorX += 0.5f;
        floorY += 0.5f;
        //and return it with the original scale
        return new Vector2(floorX * ratio, floorY * ratio);
    }


    Vector2 getCenterOfGridAtMouse()
    {
        float xUnscaledSize = renderer.bounds.extents.x;
        float yUnscaledSize = renderer.bounds.extents.y;
        //take into account the chessboard's transform scale by dividing by 8
        float xScaledSize = (xUnscaledSize / 8);
        float yScaledSize = (yUnscaledSize / 8);

        ////////////////////////////////////////
        Vector3 pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float rawX = pointer.x;
        float rawY = pointer.y;

        float ratio = xScaledSize / 4;

        //Gives us coordinates in an even (-4, 4) scale
        float scaledX = rawX / 8 / ratio;
        float scaledY = rawY / 8 / ratio;
        //which we use to calculate the position of the innermost edge of the grid at the mouse position
        float floorX = Mathf.Floor(scaledX);
        float floorY = Mathf.Floor(scaledY);
        //add 0.5 to get the center of the grid...
        floorX += 0.5f;
        floorY += 0.5f;
        //and return it with the original scale
        return new Vector2(floorX * ratio, floorY * ratio);
    }
}
