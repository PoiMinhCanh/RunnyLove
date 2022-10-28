using Assets.Scripts.Interface;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    [Header("Health")]
    [SerializeField]
    private float _startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    //private bool invulnerable;

    //[Header("Component")]
    //[SerializeField] private Behaviour[] components;

    [Header("HealthbarUIReference")]
    [SerializeField]
    private HealthBar HealthBar;

    private void Awake()
    {
        currentHealth = _startingHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void BloodRecovery(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, _startingHealth);
        UpdateHealthBarImage();
    }

    public void TakeDamage(float damage)
    {
        //if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, _startingHealth);
        //Debug.Log(currentHealth);
        UpdateHealthBarImage();
        if (currentHealth > 0)
        {
            // player hurt
            animator.SetTrigger("hurt");
            // iframes
            //StartCoroutine(Invunerability());
        }
        else
        {
            // player dead
            if (!dead)
            {
                //animator.SetBool("grounded", true);
                animator.SetTrigger("die");

                // Deactivate all attached component classes
                //foreach (Behaviour component in components)
                //{
                //    component.enabled = false;
                //}

                dead = true;
            }
        }
    }

    public void UpdateHealthBarImage() {
        // update health Bar UI
        HealthBar.UpdateHealthBarImage(currentHealth / _startingHealth);
    }

    //private IEnumerator Invunerability()
    //{
    //    //invulnerable = true;
    //    //Physics2D.IgnoreLayerCollision(8, 9, true);
    //    for (int i = 0; i < numberOfFlashes; i++)
    //    {
    //        spriteRend.color = new Color(1, 1, 1, 0.9f);
    //        yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    //        spriteRend.color = Color.white;
    //        yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    //    }
    //    //Physics2D.IgnoreLayerCollision(8, 9, false);
    //    //invulnerable = false;
    //}

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        BloodRecovery(_startingHealth);
        dead = false;
        gameObject.SetActive(true);
    }

    public bool isDead()
    {
        return dead;
    }
}