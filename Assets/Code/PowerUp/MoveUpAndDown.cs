using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public float moveDistance = 0.5f; // The distance to move up and down
    public float moveSpeed = 0.01f; // The speed of the movement

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;

        // Start the smooth up and down movement
        StartCoroutine(MoveUpDown());
    }

    private IEnumerator MoveUpDown()
    {
        while (true)
        {
            // Move up
            yield return MoveToPosition(targetPosition, moveSpeed);

            // Move down
            yield return MoveToPosition(initialPosition, moveSpeed);
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        float distance = Vector3.Distance(transform.position, target);

        while (distance > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target);
            yield return null;
        }
    }
}
