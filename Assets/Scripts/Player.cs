using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject bulletGameObject;
    [SerializeField] GameObject death_vfx;
    [SerializeField] AudioClip sfx_bullet;
    [SerializeField] AudioClip sfx_jump;
    [SerializeField] AudioClip sfx_itemCollected;
    [SerializeField] AudioClip sfx_death;

    float nextFire;
    float shootCoolDown = 0.3f;
    public bool isPaused = false;

    Collider2D playerCollider;
    BoxCollider2D playerFeetCollider;
    Rigidbody2D playerRB;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    AudioSource playerAudio;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            MovePlayer();
            PlayerJump();
            PlayerFire();
        }
    }

    public void PlaySound(AudioClip sfx)
    {
        playerAudio.PlayOneShot(sfx);
    }

    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        if(horizontalMovement != 0)
        {
            playerAnimator.SetBool("RunState", true);
            if(horizontalMovement < 0)
            {
                playerSpriteRenderer.flipX = true;
            }
            else
            {
                playerSpriteRenderer.flipX = false;
            }
        }
        else
        {
            playerAnimator.SetBool("RunState", false);
        }
        playerRB.velocity = new Vector2(horizontalMovement * speed, playerRB.velocity.y);
    }

    void PlayerJump()
    {
        bool onTheGround = IsOnGround();

        if (onTheGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                PlaySound(sfx_jump);
                playerRB.velocity = new Vector2(0, jumpForce);
                playerAnimator.SetTrigger("TakeOff");
            }
            playerAnimator.SetBool("JumpState", false);
        }
        else
        {
            playerAnimator.SetBool("JumpState", true);
        }
    }

    void PlayerFire()
    {
        float movementDir = transform.localScale.x;

        if (Input.GetKeyDown(KeyCode.Z) && playerSpriteRenderer.flipX == false)
        {
            PlaySound(sfx_bullet);
            GameObject shotObject = Instantiate(bulletGameObject, transform.position + new Vector3(movementDir, -0.03f), transform.rotation);
            Shot myShot = shotObject.GetComponent<Shot>();

            myShot.Shoot(movementDir, speed * 2f);
            playerAnimator.SetLayerWeight(1, 1);

            nextFire = Time.time + shootCoolDown;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && playerSpriteRenderer.flipX == true)
        {
            PlaySound(sfx_bullet);
            GameObject shotObject = Instantiate(bulletGameObject, transform.position + new Vector3(-movementDir, -0.03f), transform.rotation);
            Shot myShot = shotObject.GetComponent<Shot>();

            myShot.Shoot(-movementDir, speed * 2f);
            playerAnimator.SetLayerWeight(1, 1);

            nextFire = Time.time + shootCoolDown;
        }
        else if(Time.time > nextFire)
        {
            playerAnimator.SetLayerWeight(1, 0);
        }
    }

    private bool IsOnGround()
    {
        return playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == "Item")
        {
            gameManager.ReduceItemCount();
            Destroy(collision.gameObject);
            PlaySound(sfx_itemCollected);
        }
        else if(tag == "EnemyBullet")
        {
            StartCoroutine(PlayerDeath());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Spikes" || tag == "Enemy" || tag == "FlyingEnemy")
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        playerAnimator.SetBool("isDead", true);
        isPaused = true;
        playerRB.isKinematic = true;
        playerRB.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        Instantiate(death_vfx, transform.position, transform.rotation);
        PlaySound(sfx_death);
        gameManager.LoseGame();
        Destroy(gameObject, 0.5f);
    }
}
