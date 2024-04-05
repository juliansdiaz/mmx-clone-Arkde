using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    //Variables
    float speed;
    bool moving = false;
    float movedir = 1f;

    Animator shotAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        shotAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set shot movement 
        if (moving)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime * movedir, 0, 0));
        }
    }

    public void Shoot(float dir, float _speed)
    {
        moving = true;
        speed = _speed;
        movedir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layer = LayerMask.LayerToName(collision.gameObject.layer);
        string tag = collision.gameObject.tag;

        if(layer == "Ground") // Check if the shot collides against the map
        {
            moving = false;
            shotAnimator.SetTrigger("CollisionState");
        }
        else if(tag == "Enemy") // Check if the shot hits an enemy
        {
            moving = false;
            shotAnimator.SetTrigger("CollisionState");

            collision.gameObject.GetComponent<StaticEnemy>().EnemyDeath();
        }
        else if(tag == "FlyingEnemy") // Check if the shot hits an enemy
        {
            moving = false;
            shotAnimator.SetTrigger("CollisionState");

            collision.gameObject.GetComponent<FlyingEnemy>().EnemyDeath();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
