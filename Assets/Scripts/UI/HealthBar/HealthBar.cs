using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    private Image currentHealthBar;

    public void UpdateHealthBarImage(float rate)
    {
        currentHealthBar.fillAmount = rate;
    }
}
