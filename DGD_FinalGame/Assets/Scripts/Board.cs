using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile tilePrefab;
    public int size = 10;
    public Tile[,] tiles;

    public GameManager gm;
    public bool isPlayer1Board;

    void Start()
    {
        tiles = new Tile[size, size];
        Generate();
    }

    void Generate()
    {
        float startX = transform.position.x;
        float startY = transform.position.y;

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                Tile t = Instantiate(tilePrefab, new Vector3(startX + c, startY - r, 0), Quaternion.identity);
                t.row = r;
                t.col = c;
                t.parentBoard = this;
                tiles[r, c] = t;
            }
        }
    }

    public void TileClicked(Tile tile)
    {
        gm.HandleTileClick(tile, this);
    }
}