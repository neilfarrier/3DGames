using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] float bobSpeed = 1f;
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] float bobHeight = 1f;
    public int currentScore = 0;
    public int score = 1;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    public static PickupController instance;

    void Awake()
    {
        instance = this;
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, bobHeight, 0);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, bobSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);

        if (transform.position == targetPosition)
        {
            if (targetPosition == startPosition)
            {
                targetPosition = startPosition + new Vector3(0, bobHeight, 0);
            }
            else if (targetPosition == startPosition + new Vector3(0, bobHeight, 0))
            {
                targetPosition = startPosition;
            }
        }
    }
}
