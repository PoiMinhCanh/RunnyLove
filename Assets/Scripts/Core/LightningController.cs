using UnityEngine;

public class LightningController : MonoBehaviour
{
    [SerializeField] private int quantity;
    private int _maxQuantity;

    private void Start()
    {
        _maxQuantity = quantity;
    }

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
                Debug.Log("Game Over! You lose!");
                GameStateController.Instance.endGame();
            }
        }
    }

    public int getQuantity()
    {
        return quantity;
    }

    public int getMaxQuantity()
    {
        return _maxQuantity;
    }

}
