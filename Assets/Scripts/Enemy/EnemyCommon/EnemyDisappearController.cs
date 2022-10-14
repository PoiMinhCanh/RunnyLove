using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappearController : MonoBehaviour
{

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void disappear()
    {
        anim.SetTrigger("disappear");
    }

}
