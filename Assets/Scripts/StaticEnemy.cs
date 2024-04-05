using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject enemyBullet;
    [SerializeField] AudioClip sfx_enemyDeath;

    float enemyNextFire;
    float enemyFireCoolDown = 1f;
    bool canFire = true;

    Animator enemyAnimator;
    BoxCollider2D enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the time range between each shot
        if (DetectPlayer() && Time.time > enemyNextFire && canFire)
        {
            FireEnemy();
        }
    }

    bool DetectPlayer()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDestination = Vector2.left;
        RaycastHit2D enemyRayCast = Physics2D.Raycast(rayOrigin, rayDestination, 13f, LayerMask.GetMask("PlayerLayer"));
        Debug.DrawRay(rayOrigin, new Vector3(-13f, 0), Color.red);

        return (enemyRayCast.collider != null);
    }

    void FireEnemy()
    {
        Instantiate(enemyBullet, transform.position + new Vector3(0, 0.3f), transform.rotation);
        enemyNextFire = Time.time + enemyFireCoolDown;
    }

    public void EnemyDeath()
    {
        canFire = false;
        AudioSource.PlayClipAtPoint(sfx_enemyDeath, Camera.main.transform.position);
        enemyAnimator.SetTrigger("Dead");
    }

    public void RemoveEnemySprite()
    {
        Destroy(gameObject);
    }
}
