using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3Int startingTilePos;
    [SerializeField] private LevelTiles levelTiles;

    [SerializeField] private PlayerControls playerControls;

    [Header("Anim")]
    [SerializeField] private float jumpDuration;
    [SerializeField] private bool canMove = true;
    [SerializeField] private Vector3[] animPath;
    [SerializeField] private float jumpAnimOffset;

    [Header("Movement")]
    [SerializeField] private bool isMoving;
    [SerializeField] private PlayerMove currentPlayerMove;

    // events
    public delegate void OnMovementCompletedDelegate(Vector3Int newPos);
    public OnMovementCompletedDelegate OnMovementCompleted;
    private Dictionary<PlayerMove, Vector3Int> movementTileDict;
    private Dictionary<PlayerMove, Vector3> movementPositionDict;
    public Vector3Int playerTile;

    private void Awake()
    {
        playerControls = new PlayerControls();

        // Init movement dictionary
        movementTileDict = new Dictionary<PlayerMove, Vector3Int>();
        movementTileDict[PlayerMove.DownLeft] = new(-2, -1, 0);
        movementTileDict[PlayerMove.DownRight] = new(-1, -2, 0);
        movementTileDict[PlayerMove.UpLeft] = new(1, 2, 0);
        movementTileDict[PlayerMove.UpRight] = new(2, 1, 0);

        movementPositionDict = new Dictionary<PlayerMove, Vector3>();
        movementPositionDict[PlayerMove.DownLeft] = new(-0.5f, -0.75f);
        movementPositionDict[PlayerMove.DownRight] = new(0.5f, -0.75f);
        movementPositionDict[PlayerMove.UpLeft] = new(-0.5f, 0.75f);
        movementPositionDict[PlayerMove.UpRight] = new(0.5f, 0.75f);
    }

    private void Start()
    {
        transform.position = levelTiles.CellToWorld(startingTilePos);
        playerTile = startingTilePos;
    }

    private void Update()
    {
        if (isMoving)
        {
            Move(movementPositionDict[currentPlayerMove], movementTileDict[currentPlayerMove], currentPlayerMove);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Movement.MoveDownLeft.performed += MoveDownLeftPerformed;
        playerControls.Movement.MoveDownRight.performed += MoveDownRightPerformed;
        playerControls.Movement.MoveUpLeft.performed += MoveUpLeftPerformed;
        playerControls.Movement.MoveUpRight.performed += MoveUpRightPerformed;

        playerControls.Movement.MoveDownLeft.canceled += MoveDownLeftCanceled;
        playerControls.Movement.MoveDownRight.canceled += MoveDownRightCanceled;
        playerControls.Movement.MoveUpLeft.canceled += MoveUpLeftCanceled;
        playerControls.Movement.MoveUpRight.canceled += MoveUpRightCanceled;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Movement.MoveDownLeft.performed -= MoveDownLeftPerformed;
        playerControls.Movement.MoveDownRight.performed -= MoveDownRightPerformed;
        playerControls.Movement.MoveUpLeft.performed -= MoveUpLeftPerformed;
        playerControls.Movement.MoveUpRight.performed -= MoveUpRightPerformed;

        playerControls.Movement.MoveDownLeft.performed -= MoveDownLeftCanceled;
        playerControls.Movement.MoveDownRight.performed -= MoveDownRightCanceled;
        playerControls.Movement.MoveUpLeft.performed -= MoveUpLeftCanceled;
        playerControls.Movement.MoveUpRight.performed -= MoveUpRightCanceled;
    }

    private void Move(Vector3 direction, Vector3Int tileDirection, PlayerMove playerMove)
    {
        if (!canMove) return;

        var newTile = playerTile + tileDirection;
        var hasTile = levelTiles.HasTile(newTile);

        if (hasTile)
        {
            canMove = false;
            var newPos = transform.position + direction;

            animPath = new Vector3[2];
            animPath[1] = newPos;
            animPath[0] = (transform.position + newPos) / 2;
            animPath[0].y += jumpAnimOffset;

            transform.DOPath(animPath, jumpDuration).onComplete += () =>
            {
                playerTile = newTile;
                OnMovementCompleted?.Invoke(playerTile);
                canMove = true;
            };
        }
        else
        {
            print("Jumped down");
        }
    }

    private void StopMovement()
    {
        // print("isMoving = false");
        isMoving = false;
    }

    #region Move Performed

    private void MoveUpRightPerformed(InputAction.CallbackContext context)
    {
        if (isMoving) return;
        isMoving = true;
        currentPlayerMove = PlayerMove.UpRight;
    }

    private void MoveUpLeftPerformed(InputAction.CallbackContext context)
    {
        if (isMoving) return;
        isMoving = true;
        currentPlayerMove = PlayerMove.UpLeft;
    }

    private void MoveDownLeftPerformed(InputAction.CallbackContext context)
    {
        if (isMoving) return;
        isMoving = true;
        currentPlayerMove = PlayerMove.DownLeft;
    }

    private void MoveDownRightPerformed(InputAction.CallbackContext context)
    {
        if (isMoving) return;
        isMoving = true;
        currentPlayerMove = PlayerMove.DownRight;
    }

    #endregion

    #region Move Cancelled

    private void MoveDownLeftCanceled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    private void MoveDownRightCanceled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    private void MoveUpLeftCanceled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    private void MoveUpRightCanceled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    #endregion

}