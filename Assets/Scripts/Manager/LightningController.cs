using UnityEngine;

public class LightningController : MonoBehaviour
{
    [SerializeField] private int quantity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collider is enemy
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyDisappearController>().disappear();
            quantity--;
            Debug.Log(quantity);
            if (quantity <= 0)
            {
                Debug.Log("Game Over!");
            }
        }
    }

}
