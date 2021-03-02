using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public int Health = 10;
    int currHealth;
    public SpriteRenderer spriteRenderer;
    
    BoxCollider2D Hitbox;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = Health;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BulletScript>())
        {
            currHealth--;
            spriteRenderer.color = new Color( (1f/Health) * (Health - currHealth), (1f / Health) * currHealth, 0, 1);
            Destroy(collision.gameObject);
            if(currHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
