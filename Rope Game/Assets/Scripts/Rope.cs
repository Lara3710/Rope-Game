using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropePrefab;
    private List<GameObject> ropeParts = new();
    private int ropeListCount = 50;
    float segmentLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateRopeParts();
        segmentLength = ropePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateRopeParts()
    {
        for (int i = 0; i < ropeListCount; i++)
        {
            GameObject rope = Instantiate(ropePrefab);
            rope.SetActive(false);
            rope.transform.SetParent(this.transform);
            ropeParts.Add(rope);
        }
    }

    internal void DeactivateRopes()
    {
        for (int i = 0; i < ropeParts.Count; i++)
        {
            if (ropeParts[i].activeInHierarchy == true)
            {
                ropeParts[i].SetActive(false);
            }
        }
    }

    internal GameObject SpawnRopes(Vector2 mousePos, Vector2 playerPos)
    {
        DeactivateRopes();

        GameObject anchor = transform.GetChild(0).gameObject;
        Vector2 startPos = new Vector2(mousePos.x, 6f);
        anchor.transform.position = startPos;

        Vector2 direction = (playerPos - startPos).normalized;
        int ropesNeeded = Mathf.FloorToInt(Vector2.Distance(startPos, playerPos) / segmentLength);

        Rigidbody2D prevBody = anchor.GetComponent<Rigidbody2D>();

        for (int i = 0; i < ropesNeeded; i++)
        {
            GameObject rope = ropeParts[i];
            rope.SetActive(true);

            Vector2 segmentPos = startPos + direction * segmentLength * i;
            rope.transform.position = segmentPos;

            rope.transform.rotation = CalculateSegmentRotation(direction);

            ConfigueHinge(rope, prevBody);
            prevBody = rope.GetComponent<Rigidbody2D>();
        }

        return ropeParts[ropesNeeded - 1];
    }

    private Quaternion CalculateSegmentRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle - 90f); // Adjust as needed
    }

    private void ConfigueHinge(GameObject rope, Rigidbody2D prevBody)
    {
        HingeJoint2D hinge = rope.GetComponent<HingeJoint2D>();
        hinge.connectedBody = prevBody;

        // These align rope ends properly
        hinge.anchor = new Vector2(0, segmentLength / 2f);
        hinge.connectedAnchor = new Vector2(0, -segmentLength / 2f);
    }
}
