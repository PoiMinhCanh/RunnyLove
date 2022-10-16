using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // if collider is enemy
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
