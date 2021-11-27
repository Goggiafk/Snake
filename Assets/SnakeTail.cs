using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;
    public float spacing;

    private List<Transform> snakeParts = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        positions.Add(SnakeHead.position);
    }

    private void Update()
    {
        float distance = (SnakeHead.position - positions[0]).magnitude;

        if (distance > spacing)
        {
           
            Vector3 direction = (SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * spacing);
            positions.RemoveAt(positions.Count - 1);

            distance -= spacing;
        }

        for (int i = 0; i < snakeParts.Count; i++)
        {
            snakeParts[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / spacing);
        }
    }

    public void AddCircle()
    {
        Transform circle = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeParts.Add(circle);
        positions.Add(circle.position);
    }

    public void RemoveCircle()
    {
        Destroy(snakeParts[0].gameObject);
        snakeParts.RemoveAt(0);
        positions.RemoveAt(1);
    }
}
