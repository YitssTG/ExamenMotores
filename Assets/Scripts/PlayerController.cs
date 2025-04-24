using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerController player;
    private Rigidbody _rigidbody;
    public float _horizontal;
    public float _transversal;

    public float speed;
    [Header("Jump Mechanic")]
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private int jumpsAvailable;
    [SerializeField] private float jumpForce;

    [Header("Raycast Mechanic")]
    [SerializeField] private Transform _origin;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask layersInteractions;

    [Header("Draw Properties")]
    [SerializeField] private Color RayCollision = Color.green;
    [SerializeField] private Color RayNotCollision = Color.white;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        jumpsAvailable = maxJumps;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _transversal = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        DoRayCast(_direction);
    }
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector3(_horizontal * player.speed,   0, _transversal * player.speed);
    }
    public void DoRayCast(Vector3 _direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _direction, out hit, _distance, layersInteractions))
        {
            Debug.DrawRay(_origin.position, Vector2.down * hit.distance, RayCollision);
            jumpsAvailable = maxJumps;
        }
        else
        {
            Debug.DrawRay(_origin.position, Vector2.down * _distance, RayNotCollision);
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        float _horizontal = context.ReadValue<float>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumpeando");

        if (context.performed && jumpsAvailable > 0)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0 *_rigidbody.linearVelocity.y);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            jumpsAvailable--;
        }
    }
}
