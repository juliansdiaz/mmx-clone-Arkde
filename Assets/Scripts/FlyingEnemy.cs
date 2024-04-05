using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    //Variables
    [SerializeField] AudioClip sfx_enemyDeath;

    bool canMove = true;

    Animator flyingEnemyAnimator;
    CircleCollider2D flyingEnemyCollider;
    AIPath flyingEnemyAIPath;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        flyingEnemyAnimator = GetComponent<Animator>();
        flyingEnemyCollider = GetComponent<CircleCollider2D>();
        flyingEnemyAIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (canMove)
        {
            flyingEnemyAIPath.enabled = Physics2D.OverlapCircle(transform.position, 13f, LayerMask.GetMask("PlayerLayer"));
        }
    }

    public void EnemyDeath()
    {
        AudioSource.PlayClipAtPoint(sfx_enemyDeath, Camera.main.transform.position);
        flyingEnemyAIPath.enabled = false;
        canMove = false;
        flyingEnemyCollider.enabled = false;
        flyingEnemyAnimator.SetTrigger("Dead");
    }

    public void RemoveEnemySprite()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() //Draw the player detection area
    {
        Gizmos.color = new Color(255, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, 13f);
    }
}
