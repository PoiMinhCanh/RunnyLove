using UnityEngine;
using static Unity.VisualScripting.Member;

public class TurnPlayController : MonoBehaviour
{
    public static TurnPlayController Instance { get; private set; }

    private bool playerTurn;
    [Header("Play Controller")]
    [SerializeField] private PlayerController PlayerController;
    [Header("Enemy Controller")]
    [SerializeField] private Spawner Spawner;

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
        playerTurn = true;
    }

    public bool getTurn()
    {
        return playerTurn;
    }

    private void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            playerTurn = !playerTurn;
            // toggle player controller
            PlayerController.toggleComponents(playerTurn);

            //Debug.Log(playerTurn);
        }
    }
    public void changeTurn(bool flag)
    {
        playerTurn = flag;
        Debug.Log(playerTurn);
        PlayerController.toggleComponents(flag);
        // turn of enemy
        if (!flag) spawnEnemy();
    }

    private void spawnEnemy()
    {
        Spawner.Run();
        Spawner.Spawn(Random.Range(1, 5));
        // change to player turn
        changeTurn(true);
    }
}