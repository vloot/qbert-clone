using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject level;

    [Header("Side tilemaps")]
    [SerializeField] private Tilemap leftTilemap;
    [SerializeField] private Tilemap rightTilemap;

    [Header("Top tilemap")]
    [SerializeField] private LevelTiles levelTiles;

    // events
    public delegate void OnLevelLoadedDelegate(LevelScriptableObject level);
    public OnLevelLoadedDelegate OnLevelLoaded;

    private void Start()
    {
        SetTiles(leftTilemap, level.leftTile);
        SetTiles(rightTilemap, level.rightTile);
        SetTiles(levelTiles.tilemap, level.tileColors[0].tile);

        OnLevelLoaded?.Invoke(level);
    }

    private void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(position);

            if (tile != null)
            {
                tilemap.SetTile(position, tileToSet);
            }
        }
    }
}
