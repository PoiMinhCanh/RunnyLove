using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    private void Awake()
    {
        // ignore layer collision for CameraConfiner Layer (CameraConfinerObject)
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Player, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Ground, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.PlayerWeapon, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Enemy, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Gate, true);

    }
}