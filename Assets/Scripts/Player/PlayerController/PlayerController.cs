using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ShootDirectionController ShootDirectionController;

    private void Awake()
    {
        ShootDirectionController = GetComponent<ShootDirectionController>();
    }

}
