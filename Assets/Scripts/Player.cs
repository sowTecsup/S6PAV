using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity , IDamageable
{
    public InputSystem_Actions inputs;
    public GameObject InteractText;
    public IInteractable interactableObject;
    public Rigidbody2D rb;


    public Vector2 moveInput;
    public Vector2 lookInput;

    public float moveSpeed;
    public float PlayerHp;
    public float KnockbackForce;

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

        inputs.Player.Look.started += Onlook;
        inputs.Player.Look.performed += Onlook;
        inputs.Player.Look.canceled += Onlook;
    }

    private void Onlook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
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

    public void TakeDamage(int damage, Vector3 origin)//->aparte de recibir da;o debo saber desde que direccion me han goleado
    {
        PlayerHp = Math.Max(0, PlayerHp - damage);

        Vector2 knockBackDir = (transform.position - origin).normalized;

        rb.AddForce(knockBackDir * KnockbackForce, ForceMode2D.Impulse);

        print("he recibido da;o");
    }
}
