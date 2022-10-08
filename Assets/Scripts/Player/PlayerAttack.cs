using Assets.Scripts.Interface;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Transform projectilePoint;

    [Header("Properties")]
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _timeThrowForce;

    #region variables
    private float timeThrowForceCounter;
    #endregion

    #region Component
    private Animator anim;
    private PlayerController playerController;
    #endregion

    private void Awake()
    {
        // get headquarters player controller 
        playerController = GetComponent<PlayerController>();
        // get component 
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timeThrowForceCounter += Time.deltaTime;
            // update power bar UI
            float rate = CalculateRateThrowForce(timeThrowForceCounter);
            PowerBarController.Instance.UpdatePower(rate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Attack();
            // reset timeThrowForceCounter
            timeThrowForceCounter = 0;
        }
    }

    private void Attack()
    {
        // set animation throw
        anim.SetTrigger("throw");

        // pool fireballs
        int index = FindProjectile();
        projectiles[index].transform.position = projectilePoint.position;
        projectiles[index].SetActive(true);
        IThrowWeapon weapon = projectiles[index].GetComponent<IThrowWeapon>();
        if (weapon != null)
        {
            // throw projectile
            float force = CalculateThrowForce(timeThrowForceCounter);
            weapon.throwProjectile(getThrowDirection(), force);
        }
    }

    public Vector2 getThrowDirection()
    {
        Vector2 direction = playerController.ShootDirectionController.direction;
        direction.x *= Mathf.Sign(transform.localScale.x);
        Debug.Log(direction);
        return direction;
    }

    // calculate rate when hold space to throw
    private float CalculateRateThrowForce(float timer)
    {
        // cycle is the number of times it is executed twice _timeThrowForce
        int cycles = (int)(timer / (2 * _timeThrowForce));
        // shortenedTime is the reduction time used to calculate the force
        float shortenedTime = timer - cycles * (2 * _timeThrowForce);
        // timeForce is the final time to calcute throw force
        float timeForce = shortenedTime < _timeThrowForce ? shortenedTime : 2 * _timeThrowForce - shortenedTime;
        // calculate rate
        float rate = timeForce / _timeThrowForce;
        return rate;
    }

    // calculate force to throw
    private float CalculateThrowForce(float timer)
    {
        float rate = CalculateRateThrowForce(timer);
        // force is between _minForce and _maxForce and add more based on timeForce
        float force = _minForce + (_maxForce - _minForce) * rate;
        return force;
    }

    private int FindProjectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

}
