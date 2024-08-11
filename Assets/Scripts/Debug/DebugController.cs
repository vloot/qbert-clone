using UnityEngine;
using DebugTools;
using UnityEngine.Tilemaps;

public class DebugController : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private LevelTiles levelTiles;

    private void Start()
    {
        ScreenLogger.Instance.AddLine("tile", "");
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        var mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var tilePos = tilemap.WorldToCell(mouseWorldPos);
        tilePos.z = 0;

        if (levelTiles.HasTile(tilePos))
            ScreenLogger.Instance.UpdateLine("tile", tilePos.ToString());
    }
}
