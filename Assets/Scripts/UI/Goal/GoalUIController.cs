using System;
using UnityEngine;
using UnityEngine.UI;

public class GoalUIController : MonoBehaviour
{
    public static GoalUIController Instance { get; private set; }

    [Header("Reference")]
    [SerializeField] private GameStateController GameStateController;

    [Header("Component")]
    [SerializeField] private Text textQuantity;
    [SerializeField] private Image image;

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
        updateImage();
    }

    private void updateImage()
    {
        //Debug.Log((float)LightningController.getQuantity() / LightningController.getMaxQuantity());
        image.fillAmount = ((float)GameStateController.getQuantity() / GameStateController.getMaxQuantity());
    }

    private void updateQuantity()
    {
        textQuantity.text = GameStateController.getQuantity().ToString();
    }

}