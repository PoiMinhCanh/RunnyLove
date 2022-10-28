using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLayerManagement : MonoBehaviour
{
    public static EnemyLayerManagement Instance { get; private set; }

    public float EAGLE_LAYER { get; private set; }
    public float BAT_LAYER { get; private set; }

    [Header("Reference")]
    [SerializeField] private Transform eagleTransform;
    [SerializeField] private Transform batTransform;


    private void Awake()
    {
        Instance = this;
        // Keep this object even when we go to new scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Destroy duplicate gameobjects
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        EAGLE_LAYER = eagleTransform.position.y;
        BAT_LAYER = batTransform.position.y;
    }

}

