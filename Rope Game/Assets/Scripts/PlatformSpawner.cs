using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    private Queue<GameObject> platforms = new Queue<GameObject>();
    private int platformCount = 10;
    private float platformDist = 6.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePlatforms();
        // InvokeRepeating("SpawnPlatforms", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreatePlatforms()
    {
        float prevXPos = -12.5f;
        for (int i = 0; i < platformCount; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.transform.position = GenerateSpawnPoint(prevXPos);
            platforms.Enqueue(platform);

            prevXPos = platform.transform.position.x;
        }
    }

    // private void SpawnPlatforms()
    // {
    //     GameObject platform = platforms.Dequeue();
    //     platform.transform.position = GenerateSpawnPoint();
    //     platform.SetActive(true);
    // }

    private Vector2 GenerateSpawnPoint(float prevXPos)
    {
        float yPos = Random.Range(-5f, -3f);
        return new Vector2(prevXPos + platformDist, yPos);
    }

    internal void RemoveOutOfBounds(Vector2 playerPos)
    {
        if (platforms.Peek().transform.position.x <= playerPos.x - 2 * platformDist)
        {
            // platforms.i =
            // platforms.ElementAt(i)
            GameObject platform = platforms.Dequeue();
            platform.transform.position = GenerateSpawnPoint(platforms.Last().transform.position.x);
            platforms.Enqueue(platform);
        }
    }
}
