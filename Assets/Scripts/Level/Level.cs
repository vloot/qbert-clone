using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LevelTiles levelTiles;
    [SerializeField] private LevelLoader levelLoader;

    private LevelScriptableObject _currentLevel;

    private void Awake()
    {
        playerController.OnMovementCompleted += OnMovementCompleted;
        levelLoader.OnLevelLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(LevelScriptableObject level)
    {
        _currentLevel = level;
    }

    private void OnMovementCompleted(Vector3Int newPos)
    {
        var tile = levelTiles.GetTile(newPos);
        if (tile.tileColorIndex >= _currentLevel.tileColors.Length) return;
        tile.UpdateTileColor(_currentLevel.tileColors[tile.tileColorIndex].tile);
    }
}
