using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    [Header("Play Controller")]
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Transform playerTransform; 

    [Header("Enemy Controller")]
    [SerializeField] private Spawner Spawner;

    [Header("Camera Controller")]
    [SerializeField] private CameraController CameraController;
    [SerializeField] private Transform gateTransform;

    public GameState gameState;

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

    private void Start()
    {
        playerTurn();
    }

    private void playerTurn()
    {
        gameState = GameState.PLAYERTURN;

        PlayerController.toggleComponents(true);
    }

    public void enemyTurn()
    {
        gameState = GameState.ENEMYTURN;

        PlayerController.toggleComponents(false);
        StartCoroutine(spawnEnemyAndAttack());
    }

    private IEnumerator spawnEnemyAndAttack()
    {
        // set camera focus on gate
        Vector3 gatePosition = gateTransform.position;
        gatePosition.z = -10;

        yield return new WaitUntil(() => CameraController.moveToPosition(gatePosition));

        // open gate animation
        Spawner.openGateAnimation();

        yield return new WaitForSeconds(1f);

        // spawn enemies

        List<GameObject> listSpawn = Spawner.Spawn(Random.Range(1, 5));

        foreach (var spawnObj in listSpawn)
        {
            // "Respawn" game obj
            Spawner.resetSpawnObj(spawnObj);
            // random nums of run
            int numsOfRun = Random.Range(1, 4);
            // time to excute one function
            float timer = 1f;

            yield return new WaitUntil(() => {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 1f;
                    return Spawner.firstRun(spawnObj, ref numsOfRun);
                }
                else return false;
            });
            yield return new WaitForSeconds(0.5f);
        }

        // enemies run
        yield return new WaitForSeconds(0.5f);

        Spawner.Run();

        yield return new WaitForSeconds(1f);

        // close gate animation
        Spawner.closeGateAnimation();

        yield return new WaitForSeconds(1.5f);

        // set camera back to player
        Vector3 playerPosition = playerTransform.position;
        playerPosition.z = -10;

        yield return new WaitUntil(() => CameraController.moveToPosition(playerPosition));

        // change to player turn
        playerTurn();

    }

}