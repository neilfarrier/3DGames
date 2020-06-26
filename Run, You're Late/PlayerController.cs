using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    private Rigidbody playerRigidBody;
    public LayerMask groundlayers;
    public CapsuleCollider playerCollider;
    private Animator animator;
    private AudioSource audioSource;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        Move();
        UpdateAnimator();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(xInput, 0, zInput) * moveSpeed;
        direction.y = playerRigidBody.velocity.y;

        playerRigidBody.velocity = direction;

        Vector3 facingDirection = new Vector3(xInput, 0, zInput);

        if (facingDirection.magnitude > 0)
        {
            transform.forward = facingDirection;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.AddScore(-2);
            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Pickup"))
        {
            GameManager.instance.AddScore(1);
            Destroy(other.gameObject);
            audioSource.Play();
        }
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.LevelEnd();
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y, playerCollider.bounds.center.z), playerCollider.radius * 0.9f, groundlayers);
    }
}
