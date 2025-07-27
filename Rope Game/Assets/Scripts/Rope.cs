using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropePrefab;
    private List<GameObject> ropeParts = new();
    private int ropeListCount = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateRopeParts();
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

    private void DeactivateRopes()
    {
        for (int i = 0; i < ropeParts.Count; i++)
        {
            if (ropeParts[i].activeInHierarchy == true)
            {
                ropeParts[i].SetActive(false);
            }
        }
    }

    // ist spawna från ceiling ner så vill jag spawna från player upp !!!!

    internal void SpawnFirstRope(Vector2 mousePos, Vector2 playerPos)
    {
        DeactivateRopes();
        Vector2 ropePos = new Vector2(mousePos.x, 4.5f); // todo fixa y
        GameObject firstRope = ropeParts[0];
        firstRope.transform.position = ropePos;
        firstRope.GetComponent<Rigidbody2D>().gravityScale = 0;
        firstRope.SetActive(true);

        int ropesNeeded = Mathf.RoundToInt(CalcDistPlayerRope(ropePos, playerPos));

        SpawnRopes(ropesNeeded);
    }

    private float CalcDistPlayerRope(Vector2 ropePos, Vector2 playerPos)
    {
        float dx = ropePos.x - playerPos.x;
        float dy = ropePos.y - playerPos.y;

        float dist = Mathf.Sqrt(dx * dx + dy * dy);

        return dist / ropePrefab.transform.localScale.y;
    }

    private void SpawnRopes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject ropePart = ropeParts[i + 1];
            ropePart.transform.position = new Vector2(ropeParts[i].transform.position.x, ropeParts[i].transform.position.y - ropeParts[i].transform.localScale.y);
            ropePart.GetComponent<HingeJoint2D>().connectedBody = ropeParts[i].GetComponent<Rigidbody2D>();
            ropePart.GetComponent<FixedJoint2D>().connectedBody = ropeParts[i].GetComponent<Rigidbody2D>();
            ropePart.SetActive(true);
        }
    }
}
