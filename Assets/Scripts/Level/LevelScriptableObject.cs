using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Game/LevelScriptableObject", order = 0)]
public class LevelScriptableObject : ScriptableObject
{
    /*
        disks
    */

    public TileMapper[] tileColors;

    public Tile leftTile;
    public Tile rightTile;
}
