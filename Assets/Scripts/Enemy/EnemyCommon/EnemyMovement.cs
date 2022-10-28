using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Animator anim;
    private Rigidbody2D rb;

    public bool isFirstMove;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFirstMove = true;
    }

    private void Update()
    {
        if (GameStateController.Instance.gameState == GameState.ENEMYTURN || isFirstMove)
        {
            Run();
        }
    }

    public void Run()
    {
        anim.SetTrigger("run");
        rb.MovePosition(rb.position +  Vector2.left * speed * Time.deltaTime);
    }

    public bool flyHigh()
    {
        anim.SetTrigger("run");
        float height = 0;
        switch (gameObject.GetComponent<EnemyController>().type)
        {
            case EnemyType.EAGLE:
                // get height(constraint by EAGLE_LAYER)
                height = Mathf.Min(rb.position.y + speed * Time.deltaTime, EnemyLayerManagement.Instance.EAGLE_LAYER);
                rb.position = new Vector2(rb.position.x, height);

                return (rb.position.y == EnemyLayerManagement.Instance.EAGLE_LAYER);
            case EnemyType.BAT:
                // get height(constraint by BAT_LAYER)
                height = Mathf.Min(rb.position.y + speed * Time.deltaTime, EnemyLayerManagement.Instance.BAT_LAYER);
                rb.position = new Vector2(rb.position.x, height);

                return (rb.position.y == EnemyLayerManagement.Instance.BAT_LAYER);
            default:
                return false;
        }
    }
}
