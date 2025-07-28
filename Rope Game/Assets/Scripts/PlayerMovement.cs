using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float jumpForce = 20f;
    public float gravity = -40f;

    private Rope rope;

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
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ProjectRope()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // använd rope script
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject lastRope = rope.SpawnRopes(mousePos, new Vector2(transform.position.x + 0.5f, transform.position.y));

            ConnectPlayer(lastRope);
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
}
