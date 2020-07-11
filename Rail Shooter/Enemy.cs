using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 10;

    ScoreBoard scoreBoard;

    private void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits <= 0)
            KillEnemy();
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits -= 1;
    }

    private void KillEnemy()
    {
        GameObject explosionFX = Instantiate(deathFX, transform.position, Quaternion.identity);
        explosionFX.transform.parent = parent;
        Destroy(gameObject);
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }
}
