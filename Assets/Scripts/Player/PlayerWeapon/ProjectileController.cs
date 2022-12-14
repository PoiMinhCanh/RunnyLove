using Assets.Scripts.Interface;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : PlayerDamage, IThrowWeapon 
// Enherit PlayerDamage will damage the player every time they touch
{
    [Header("Reference")]
    [SerializeField] private Animator anim;
    [SerializeField] private CircleCollider2D circleCollider2D;

    #region Component
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        // set gameobject inactive
        gameObject.SetActive(false);
    }

    public void ActivateProjectile()
    {
        gameObject.SetActive(true);
        circleCollider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Excute logic from parent script first

        // if collider is gate
        if (collision.tag == "Gate" || collision.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
 
    public void throwProjectile(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    // use this when end of die animation
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}