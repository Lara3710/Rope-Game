using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 4.4f)
        {
            speed *= -1;
        }
        else if (transform.position.x <= -4.4f)
        {
            speed = 2f;
        }

        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
