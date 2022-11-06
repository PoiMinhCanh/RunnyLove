using System;
using UnityEngine;
using UnityEngine.UI;

public class LightningUIController : MonoBehaviour
{
    public static LightningUIController Instance { get; private set; }

    [Header("Reference")]
    [SerializeField] private LightningController LightningController;

    [Header("Component")]
    [SerializeField] private Text textQuantity;

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

    }

    private void Update()
    {
        updateQuantity();
        //updateImage();
    }

    //private void updateImage()
    //{
    //    //Debug.Log((float)LightningController.getQuantity() / LightningController.getMaxQuantity());
    //    image.fillAmount = ((float)LightningController.getQuantity() / LightningController.getMaxQuantity());
    //}

    private void updateQuantity()
    {
        textQuantity.text = LightningController.getQuantity().ToString();
    }

}