using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    public float distance = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x + distance, transform.position.y, transform.position.z);
    }
}
