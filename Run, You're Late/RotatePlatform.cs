using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField] Vector3 offsetEndPosition;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    public int rotateSpeed;
    public int moveSpeed;

    void Awake()
    {
        startPosition = transform.position;
        targetPosition = startPosition + offsetEndPosition;
    }


    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            if (targetPosition == startPosition)
            {
                targetPosition = startPosition + offsetEndPosition;
            }
            else if (targetPosition == startPosition + offsetEndPosition)
            {
                targetPosition = startPosition;
            }
        }
    }
}
