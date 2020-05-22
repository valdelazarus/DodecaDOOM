using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int scoreValue;
    public float attackRange;
    public float attackRate;
    public GameObject[] meleeHitColliders;
    public GameObject hitColliderParentObj;

    const float spriteRotationFacingCam = 60f;

    Transform player;
    Transform sprite;

    GameManager gameManager;

    EnemySound enemySound;

    Animator anim;

    bool canAttack = true;

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

        EnsureAllCollidersAtCorrectPositions();
        DetectAttack();
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

    void EnsureAllCollidersAtCorrectPositions()
    {
        hitColliderParentObj.transform.localRotation = Quaternion.Euler(0 - transform.rotation.eulerAngles.x, 0 - transform.rotation.eulerAngles.y, 0 - transform.rotation.eulerAngles.z);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void AddScore()
    {
        gameManager.AddScore(scoreValue);
    }

    public void OnDead()
    {
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

}
