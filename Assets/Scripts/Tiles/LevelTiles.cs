using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class LevelTiles : MonoBehaviour
{
    [SerializeField] public Tilemap tilemap;

    private Dictionary<Vector3Int, CustomTile> tilesDict;

    private void Awake()
    {
        tilesDict = new Dictionary<Vector3Int, CustomTile>();

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(position);
            var customTile = new CustomTile(TileColor.None, position, tile, this);
            tilemap.SetTileFlags(position, TileFlags.None);

            if (tile != null)
            {
                tilesDict[position] = customTile;
            }
        }
    }

    public bool HasTile(Vector3Int tilePosition)
    {
        return tilesDict.ContainsKey(tilePosition);
    }

    public CustomTile GetTile(Vector3Int tilePositions)
    {
        if (tilesDict.ContainsKey(tilePositions))
        {
            return tilesDict[tilePositions];
        }

        return null;
    }

    /// <summary>
    /// Get tile using world position. Returs null if a tile at that position does not exist
    /// </summary>
    /// <param name="tileWorldPosition"></param>
    /// <returns></returns>
    public CustomTile GetTileByWorldPosition(Vector3 tileWorldPosition)
    {
        tileWorldPosition.z = 0;
        var tilePos = tilemap.WorldToCell(tileWorldPosition);
        return GetTile(tilePos);
    }

    public Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return tilemap.WorldToCell(worldPosition);
    }

    public Vector3 CellToWorld(Vector3Int position)
    {
        return tilemap.CellToWorld(position);
    }

    public int GetTilesCount()
    {
        return tilesDict.Count;
    }

    public List<CustomTile> GetAllTiles()
    {
        return tilesDict.Values.ToList();
    }
}
