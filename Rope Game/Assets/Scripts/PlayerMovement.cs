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
            rope.SpawnFirstRope(mousePos, transform.GetChild(0).position);
        }
    }


}
