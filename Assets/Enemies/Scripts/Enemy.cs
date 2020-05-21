using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int scoreValue;

    NavMeshAgent agent;
    Transform player;
    Transform sprite;

    GameManager gameManager;

    EnemySound enemySound;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = transform.GetChild(0);
        player = GameObject.FindWithTag("Player").transform;
        gameManager = FindObjectOfType<GameManager>();

        enemySound = GetComponent<EnemySound>();
        enemySound.PlaySoundEffect(EnemySound.EffectType.APPEAR);
    }

    
    void Update()
    {
        agent.destination = player.position;
        sprite.transform.rotation = Quaternion.Euler(60f, 0f, 0f);
    }

    public void AddScore()
    {
        gameManager.AddScore(scoreValue);
    }
}
