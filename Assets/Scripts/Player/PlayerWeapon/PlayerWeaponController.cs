using Assets.Scripts.Interface;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWeaponController : MonoBehaviour, IThrowWeapon
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            gameObject.SetActive(false);
        }    
    }

    public void throwProjectile(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
