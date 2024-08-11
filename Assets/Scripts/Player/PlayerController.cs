using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3Int startingTilePos;
    [SerializeField] private LevelTiles levelTiles;

    [SerializeField] private PlayerControls playerControls;

    [Header("Anim")]
    [SerializeField] private float jumpDuration;
    [SerializeField] private bool canMove = true;

    // events
    public delegate void OnMovementCompletedDelegate(Vector3Int newPos);
    public OnMovementCompletedDelegate OnMovementCompleted;

    private Vector3Int DownLeft = new(-2, -1, 0);
    private Vector3Int DownRight = new(-1, -2, 0);
    private Vector3Int UpLeft = new(1, 2, 0);
    private Vector3Int UpRight = new(2, 1, 0);

    public Vector3Int playerTile;



    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        transform.position = levelTiles.CellToWorld(startingTilePos);
        playerTile = startingTilePos;
    }

    private void Update()
    {
        //   0   2
        // -0.5 1.25
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Movement.MoveDownLeft.performed += MoveDownLeftPerformed;
        playerControls.Movement.MoveDownRight.performed += MoveDownRightPerformed;
        playerControls.Movement.MoveUpLeft.performed += MoveUpLeftPerformed;
        playerControls.Movement.MoveUpRight.performed += MoveUpRightPerformed;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Movement.MoveDownLeft.performed -= MoveDownLeftPerformed;
        playerControls.Movement.MoveDownRight.performed -= MoveDownRightPerformed;
        playerControls.Movement.MoveUpLeft.performed -= MoveUpLeftPerformed;
        playerControls.Movement.MoveUpRight.performed -= MoveUpRightPerformed;
    }

    private void MoveUpRightPerformed(InputAction.CallbackContext context)
    {
        print("MoveUpRight");
        Move(new Vector3(0.5f, 0.75f), UpRight);
    }

    private void MoveUpLeftPerformed(InputAction.CallbackContext context)
    {
        print("MoveUpLeft");
        Move(new Vector3(-0.5f, 0.75f), UpLeft);
    }

    private void MoveDownLeftPerformed(InputAction.CallbackContext context)
    {
        print("MoveDownLeft");
        Move(new Vector3(-0.5f, -0.75f), DownLeft);
    }

    private void MoveDownRightPerformed(InputAction.CallbackContext context)
    {
        print("MoveDownRight");
        Move(new Vector3(0.5f, -0.75f), DownRight);
    }

    private void Move(Vector3 direction, Vector3Int tileDirection)
    {
        var newTile = playerTile + tileDirection;

        if (levelTiles.HasTile(newTile) && canMove)
        {
            canMove = false;
            var newPos = transform.position + direction;
            transform.DOMove(newPos, jumpDuration).onComplete += () =>
            {
                playerTile = newTile;
                OnMovementCompleted?.Invoke(playerTile);
                canMove = true;
            };
        }
    }
}