using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        // tinatawag tuwing magsisimula ang scene o pag-spawn ng player using spwanID
        string spawnID = PlayerPositionData.nextSpawnPointID;
        var spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        foreach (var sp in spawnPoints)
        {
            if (sp.spawnID == spawnID)
            {
                transform.position = sp.transform.position;
                break;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Ensure this method is used by linking it to the Input System in Unity's Inspector  
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
