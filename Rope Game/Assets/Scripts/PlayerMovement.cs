using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float jumpForce = 15f;
    public float sideForce = 10f;
    public float gravity = -40f;

    private bool isOnGround;
    private bool isOnRope;

    private Rope rope;
    private float maxRopeDist = 6f;

    public PlatformSpawner spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = new Vector2(0, gravity);
        rope = GameObject.Find("RopeContainer").GetComponent<Rope>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        ProjectRope();
        MoveSideWays();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void MoveSideWays()
    {
        if (Input.GetKey(KeyCode.D) && isOnRope)
        {
            playerRb.AddForce(Vector2.right * sideForce, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.A) && isOnRope)
        {
            playerRb.AddForce(Vector2.left * sideForce, ForceMode2D.Force);
        }
    }

    private void ProjectRope()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isOnRope)
        {
            // använd rope script
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(Mathf.Min(transform.position.x + maxRopeDist, mousePos.x), mousePos.y);
            GameObject lastRope = rope.SpawnRopes(mousePos, new Vector2(transform.position.x + 0.5f, transform.position.y));

            isOnGround = false;
            isOnRope = true;

            ConnectPlayer(lastRope);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isOnRope = false;
            rope.DeactivateRopes();
            GetComponent<HingeJoint2D>().enabled = false;
        }
    }

    private void ConnectPlayer(GameObject lastRope)
    {
        HingeJoint2D hinge = GetComponent<HingeJoint2D>();
        hinge.connectedBody = lastRope.GetComponent<Rigidbody2D>();


        // Set the anchor point on the player (usually center)
        hinge.anchor = Vector2.zero;

        // Set the connection point on the rope (bottom of segment)
        float segmentLength = lastRope.GetComponent<SpriteRenderer>().bounds.size.y;
        hinge.connectedAnchor = new Vector2(0, -segmentLength / 2f);


        hinge.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            spawner.RemoveOutOfBounds(transform.position);
        }
    }
}
