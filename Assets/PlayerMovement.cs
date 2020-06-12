using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeedX = 5.0f;
    [SerializeField] private float jumpSpeed = 400.0f;

    [SerializeField] private Transform foot = null;
    [SerializeField] private Transform sprite = null;

    [SerializeField] private Animator animator = null;
    public bool isGrounded = false;

    private Rigidbody2D body = null;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float xMovement = Input.GetAxisRaw("Horizontal") * movementSpeedX;

        LayerMask groundLayer = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(foot.position, Vector3.down, 0.2f, groundLayer);
        if (hit.collider != null) {
            Debug.Log("Object hit: " + hit.collider.name);
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Vector3 jumpForce = new Vector3(0.0f, jumpSpeed, 0.0f);
            body.AddForce(jumpForce);
        }

        Vector3 moveVector = new Vector3(xMovement * Time.deltaTime, 0.0f, 0.0f);
        transform.Translate(moveVector);

        float direction = (xMovement < 0.0f) ? -1.0f : 1.0f;
        sprite.transform.localScale = new Vector3(direction, 1.0f, 1.0f);

        animator.SetBool("IsJumping", !isGrounded);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(xMovement));
    }
}
