using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class PowerBarController : MonoBehaviour
{
    public static PowerBarController Instance { get; private set; }
    [SerializeField]
    private Image fillBar;

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
   
    public void UpdatePower(float rate)
    {
        fillBar.fillAmount = rate;
    }
}



