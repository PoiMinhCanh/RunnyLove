using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Run()
    {
        anim.SetTrigger("run");
        transform.Translate(-1, 0, 0);
    }
}
