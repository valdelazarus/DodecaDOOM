using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float meleeAttackRate;//every ? seconds
    public float rangedAttackRate; //every ? seconds
    public GameObject projectile;
    public Transform[] projectileSpawnPos;
    public GameObject[] meleeHitColliders;

    Rigidbody myRB;
    Animator myAnim;

    bool canMelee = true, canRanged = true;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        ProcessMovement();
        ProcessAbility();
    }

    private void ProcessMovement()
    {
        float vert = CrossPlatformInputManager.GetAxisRaw("Vertical");
        float horz = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        myRB.velocity = new Vector3(
                horz,
                myRB.velocity.y,
                vert
            ) * speed;

        myAnim.SetFloat("moveX", myRB.velocity.x);
        myAnim.SetFloat("moveY", myRB.velocity.z);

        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") >= 1 ||
            CrossPlatformInputManager.GetAxisRaw("Horizontal") <= -1||
            CrossPlatformInputManager.GetAxisRaw("Vertical") >= 1||
            CrossPlatformInputManager.GetAxisRaw("Vertical") <= -1)
        {
            myAnim.SetFloat("lastMoveX", CrossPlatformInputManager.GetAxisRaw("Horizontal"));
            myAnim.SetFloat("lastMoveY", CrossPlatformInputManager.GetAxisRaw("Vertical"));
        }
    }

    void ProcessAbility()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && canMelee)
        {
            canMelee = false;
            myAnim.SetTrigger("melee");
            StartCoroutine(ResetMelee());
        }
        else if (CrossPlatformInputManager.GetButtonDown("Fire2") && canRanged)
        {
            canRanged = false;
            myAnim.SetTrigger("ranged");
            StartCoroutine(ResetRanged());
        }
    }

    IEnumerator ResetMelee()
    {
        yield return new WaitForSeconds(meleeAttackRate);
        canMelee = true;
    }

    IEnumerator ResetRanged()
    {
        yield return new WaitForSeconds(rangedAttackRate);
        canRanged = true;
    }

    string GetFacingDirection()
    {
        string direction = "down";
        if (myAnim.GetFloat("lastMoveX") == 1f)
        {
            direction = "right";
        }
        if (myAnim.GetFloat("lastMoveX") == -1f)
        {
            direction = "left";
        }
        if (myAnim.GetFloat("lastMoveY") == 1f)
        {
            direction = "up";
        }
        if (myAnim.GetFloat("lastMoveY") == -1f)
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
                spawned = Instantiate(projectile, projectileSpawnPos[0].position, Quaternion.identity);                break;
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
        spawned?.GetComponent<PlayerProjectile>().UpdateAnimation(myAnim.GetFloat("lastMoveX"), myAnim.GetFloat("lastMoveY"));
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
