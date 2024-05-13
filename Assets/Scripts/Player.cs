using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private GameInput gameInput;
    
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    [SerializeField] private bool IsTesting = false; 
    
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    
    private KitchenObject kitchenObject;
    
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangeEventArgs> onSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }
    
    private void GameInputOnOnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are multiple Player Instances");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInputOnOnInteractAction;
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();

        if (IsTesting && Input.GetKeyDown(KeyCode.L)) {
            Destroy(kitchenObject.gameObject);
            ClearKitchenObject();
        }
    }

    private void HandleMovement() { 
        // Get Input Vector
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Set Movement Vector 
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        // Check for collisions
        float movementDistance = movementSpeed * Time.deltaTime;
        float playerHeight = 2.0f;
        float playerRadius = 0.7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, 
            playerRadius, moveDirection, movementDistance);

        if (!canMove) {
            // Cannot move towards MovementDirection
            // Attempt only X Movement

            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, 
                playerRadius, moveDirX, movementDistance);

            if (canMove) {
                // Can only move on the X
               moveDirection = moveDirX;
            }
            else {
                // Cannot move only on the X
                // Attempt Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, 
                    playerRadius, moveDirZ, movementDistance);

                if (canMove) {
                    moveDirection = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }
            
        if (canMove) {
                transform.position += moveDirection * movementDistance;
        }

        // Used in movement animation 
        isWalking = moveDirection != Vector3.zero;

        // Rotate to the direction we are moving
        float rotationSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 2.0f;

        if (moveDirection != Vector3.zero) {
            lastInteractDirection = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, interactDistance, counterLayerMask)) {
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // Has ClearCounter
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        
        onSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectDisplayPoint() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}