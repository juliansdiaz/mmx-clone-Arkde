using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Variables
    [SerializeField] float speed;
    
    Rigidbody2D enemyBulletRB;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        enemyBulletRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Bullet movement
        transform.Translate(new Vector3(speed * Time.deltaTime * -1, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the bullet collides with the map and has to be destroyed
        string layer = LayerMask.LayerToName(collision.gameObject.layer);
        string tag = collision.gameObject.tag;

        if (layer == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
