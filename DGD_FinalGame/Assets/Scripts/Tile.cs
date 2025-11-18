using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row;
    public int col;

    public bool hasShip = false;
    public bool isHit = false;

    public Board parentBoard;

    private void OnMouseDown()
    {
        parentBoard.TileClicked(this);
    }
}