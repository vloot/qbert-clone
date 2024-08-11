using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelTiles levelTiles;
    [SerializeField] private PlayerController playerController; // TODO remove this ref

    [SerializeField] private TileColor[] tileColors;

    private void Start()
    {
        playerController.OnMovementCompleted += OnMovementCompleted;
        var allTiles = levelTiles.GetAllTiles();
        foreach (var item in allTiles)
        {
            item.UpdateTileColor(tileColors[0]);
        }
    }

    private void OnMovementCompleted(Vector3Int newPos)
    {
        var tile = levelTiles.GetTile(newPos);
        if (tile.tileColorIndex >= tileColors.Length) return;
        tile.UpdateTileColor(tileColors[tile.tileColorIndex]);
    }
}
