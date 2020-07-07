using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilattor : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private float period = 2f;

    float movementFactor;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon)
            return;

        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f;

        Vector3 offset = movementFactor * moveDirection;
        transform.position = startingPosition + offset;
    }
}
