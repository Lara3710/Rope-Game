using UnityEngine;

public class InfiniteFloor : MonoBehaviour
{
    public Transform cameraTransform;
    public float tileWidth = 1f;
    public int tileCount = 10;

    private Transform[] tiles;
    private float totalWidth;

    void Start()
    {
        // Cache all child tile transforms
        tiles = new Transform[transform.childCount];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = transform.GetChild(i);
        }

        // Sort tiles by X position (left to right)
        System.Array.Sort(tiles, (a, b) => a.position.x.CompareTo(b.position.x));

        // Calculate total width of all tiles
        totalWidth = tileCount * tileWidth;
    }

    void Update()
    {
        Transform leftMost = tiles[0];
        Transform rightMost = tiles[tiles.Length - 1];

        float camLeftEdge = cameraTransform.position.x - (Camera.main.orthographicSize * Camera.main.aspect);
        float camRightEdge = cameraTransform.position.x + (Camera.main.orthographicSize * Camera.main.aspect);

        // If left tile goes off-screen to the left, move it to the right end
        if (leftMost.position.x + tileWidth < camLeftEdge)
        {
            leftMost.position = new Vector3(rightMost.position.x + tileWidth, leftMost.position.y, leftMost.position.z);

            // Reorder the array
            for (int i = 0; i < tiles.Length - 1; i++)
                tiles[i] = tiles[i + 1];
            tiles[tiles.Length - 1] = leftMost;
        }

        // If right tile goes off-screen to the right, move it to the left end
        if (rightMost.position.x - tileWidth > camRightEdge)
        {
            rightMost.position = new Vector3(leftMost.position.x - tileWidth, rightMost.position.y, rightMost.position.z);

            // Reorder the array
            for (int i = tiles.Length - 1; i > 0; i--)
                tiles[i] = tiles[i - 1];
            tiles[0] = rightMost;
        }
    }
}
