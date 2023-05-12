using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour
{
    void Start()
    {
        /*for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }*/
    }

    public BoundsInt GetBounds() 
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        return bounds;
    }

    public TileBase[] GetTileBase() 
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        TileBase[] allTiles = tilemap.GetTilesBlock(GetBounds());
        return allTiles;
    }
}