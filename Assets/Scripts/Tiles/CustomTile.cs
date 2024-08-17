using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTile
{
    public TileColor tileColor;
    public Vector3Int pos;
    public TileBase tileBase;
    public int tileColorIndex = 1;

    private readonly LevelTiles levelTilesRef;

    public CustomTile(TileColor tileColor, Vector3Int pos, TileBase tileBase, LevelTiles levelTiles)
    {
        this.tileColor = tileColor;
        this.pos = pos;
        this.tileBase = tileBase;
        levelTilesRef = levelTiles;
    }

    public void UpdateTileColor(Tile newTile)
    {
        levelTilesRef.tilemap.SetTile(pos, newTile);
        tileColorIndex++;
    }
}
