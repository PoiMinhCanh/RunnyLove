using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Run()
    {
        anim.SetTrigger("run");
        transform.Translate(-speed, 0, 0);
    }
}
