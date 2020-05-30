using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int scoreValue;
    public int attackAmount;
    public float attackRange;
    public float attackRate;
    public float attackRange2;
    public float attackRate2;
    public GameObject[] meleeHitColliders;
    public GameObject[] childGroupObjs;
    public GameObject projectile;
    public Transform[] projectileSpawnPos;

    const float spriteRotationFacingCam = 60f;

    Transform player;
    Transform sprite;

    GameManager gameManager;

    EnemySound enemySound;

    Animator anim;

    bool canAttack = true;
    bool canAttack2 = true;

    void Start()
    {
        sprite = transform.GetChild(0);
        anim = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").transform;
        gameManager = FindObjectOfType<GameManager>();

        enemySound = GetComponent<EnemySound>();
        enemySound.PlaySoundEffect(EnemySound.EffectType.APPEAR);
    }

    
    void Update()
    {
        sprite.transform.rotation = Quaternion.Euler(spriteRotationFacingCam, 0f, 0f);

        EnsureAllChildObjsAtCorrectPositions();

        switch (attackAmount)
        {
            case 1:
                DetectAttack();
                break;
            case 2:
                DetectRangedAndMelee();
                break;
            case 3:
                break;
        }
    }

    void DetectAttack()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange && canAttack)
        {
            canAttack = false;
            anim.SetTrigger("attack");
            StartCoroutine(ResetAttack());
        }
    }

    void DetectRangedAndMelee()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange2 && canAttack2)
        {
            canAttack2 = false;
            anim.SetTrigger("attack2");
            StartCoroutine(ResetAttack2());
        } else if (Vector3.Distance(transform.position, player.position) <= attackRange && canAttack)
        {
            canAttack = false;
            anim.SetTrigger("attack");
            StartCoroutine(ResetAttack());
        }
    }

    void EnsureAllChildObjsAtCorrectPositions()
    {
        foreach(GameObject g in childGroupObjs)
        {
            g.transform.localRotation = Quaternion.Euler(0 - transform.rotation.eulerAngles.x, 0 - transform.rotation.eulerAngles.y, 0 - transform.rotation.eulerAngles.z);
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
    IEnumerator ResetAttack2()
    {
        yield return new WaitForSeconds(attackRate2);
        canAttack2 = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange2);
    }

    public void AddScore()
    {
        gameManager.AddScore(scoreValue);
    }

    public void OnDead()
    {
        GetComponent<PickupSpawner>().SpawnPickup();
        Destroy(gameObject);
    }

    public void EnableHitColliderDown()
    {
        meleeHitColliders[0].SetActive(true);
    }
    public void DisableHitColliderDown()
    {
        meleeHitColliders[0].SetActive(false);
    }
    public void EnableHitColliderUp()
    {
        meleeHitColliders[1].SetActive(true);
    }
    public void DisableHitColliderUp()
    {
        meleeHitColliders[1].SetActive(false);
    }
    public void EnableHitColliderLeft()
    {
        meleeHitColliders[2].SetActive(true);
    }
    public void DisableHitColliderLeft()
    {
        meleeHitColliders[2].SetActive(false);
    }
    public void EnableHitColliderRight()
    {
        meleeHitColliders[3].SetActive(true);
    }
    public void DisableHitColliderRight()
    {
        meleeHitColliders[3].SetActive(false);
    }

    string GetFacingDirection()
    {
        string direction = "down";
        if (anim.GetFloat("lastMoveX") >= 1f)
        {
            direction = "right";
        }
        else if (anim.GetFloat("lastMoveX") <= -1f)
        {
            direction = "left";
        }
        else if (anim.GetFloat("lastMoveY") >= 1f)
        {
            direction = "up";
        }
        else if (anim.GetFloat("lastMoveY") <= -1f)
        {
            direction = "down";
        }
        return direction;
    }

    public void SpawnProjectile()
    {
        string direction = GetFacingDirection();
        GameObject spawned = null;
        switch (direction)
        {
            case "down":
                spawned = Instantiate(projectile, projectileSpawnPos[0].position, Quaternion.identity);
                break;
            case "up":
                spawned = Instantiate(projectile, projectileSpawnPos[1].position, Quaternion.identity);
                break;
            case "left":
                spawned = Instantiate(projectile, projectileSpawnPos[2].position, Quaternion.identity);
                break;
            case "right":
                spawned = Instantiate(projectile, projectileSpawnPos[3].position, Quaternion.identity);
                break;
        }
        spawned?.GetComponent<EnemyProjectile>().UpdateAnimation(anim.GetFloat("lastMoveX"), anim.GetFloat("lastMoveY"));
    }
}
