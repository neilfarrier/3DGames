using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    //use the waypoint system from the RPG

    [SerializeField] float speed = 2f;
    [SerializeField] Vector3 offsetEndPosition;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Awake()
    {
        startPosition = transform.position;
        targetPosition = startPosition + offsetEndPosition;
    }

    void Update()
    {
        Vector3 targetDirection = targetPosition - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(newDirection);

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
