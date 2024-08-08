using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private Vector3Int pos;

    private void Start() {
        tilemap.SetColor(pos, Color.red);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            tilemap.SetTileFlags(pos, TileFlags.None);
            tilemap.SetColor(pos, Color.red);
            Debug.Log("Try change color");
        }
    }
}
