using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public InputSystem_Actions inputs;

    public GameObject InteractText;


    public IInteractable interactableObject;
    public Vector2 moveInput;
    public float moveSpeed;
    private void Awake()
    {
        inputs = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.started += OnMove;
        inputs.Player.Move.performed += OnMove;
        inputs.Player.Move.canceled += OnMove;
        inputs.Player.Interact.performed += OnInteract;
        inputs.Player.Interact.started += OnInteract;

    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        print("try to interact");
        if (interactableObject != null)
            interactableObject.Interact(gameObject);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void OnDisable()
    {
        inputs.Player.Move.started -= OnMove;
        inputs.Player.Move.performed -= OnMove;
        inputs.Player.Move.canceled -= OnMove;
        inputs.Disable();
    }

    void Start()
    {
        
    }
  
    void Update()
    {
        transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            InteractText.SetActive(true);
            interactableObject = collision.gameObject.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractText.SetActive(false);
        interactableObject = null;
    }
}
