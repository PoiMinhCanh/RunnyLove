using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GameManagementController : MonoBehaviour
{
    private void Awake()
    {
        // ignore layer collision for CameraConfiner Layer (CameraConfinerObject)
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Player, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Ground, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.PlayerWeapon, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Enemy, true);
        Physics2D.IgnoreLayerCollision(StaticProperties.CameraConfiner, StaticProperties.Gate, true);

        // Enemy Ignore Enemy in the Game
        Physics2D.IgnoreLayerCollision(StaticProperties.Enemy, StaticProperties.Enemy, true);

    }

}