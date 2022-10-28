using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    [Header("Play Controller")]
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Transform playerTransform; 

    [Header("Enemy Controller")]
    [SerializeField] private Spawner Spawner;
    [SerializeField] private GameObject BossEnemy;

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

    private void Update()
    {
        if (BossEnemy.GetComponent<Health>().isDead())
        {
            gameState = GameState.WON;
            Debug.Log(gameState);
            endGame();
        }
    }

    public void endGame()
    {
        gameState = GameState.LOST;
        SceneManager.LoadScene("EndScene");
    }

    private void playerTurn()
    {
        PlayerController.toggleComponents(true);
    }

    public void enemyTurn()
    {
        PlayerController.toggleComponents(false);
        StartCoroutine(spawnEnemyAndAttack());
    }

    private IEnumerator spawnEnemyAndAttack()
    {
        yield return new WaitForSeconds(2f);

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
            if (spawnObj.GetComponent<EnemyController>().type == EnemyType.EAGLE
             || spawnObj.GetComponent<EnemyController>().type == EnemyType.BAT)
            {
                // make fly "slow" a little bit, there is 0.2f seconds
                float timer = 0.01f;
                yield return new WaitUntil(() => {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        timer = 0.01f;
                        return spawnObj.GetComponent<EnemyMovement>().flyHigh();
                    }
                    return false;
                });
            }
            // enemy will excute first move
            spawnObj.GetComponent<EnemyMovement>().isFirstMove = true;

            // time to excute one function
            // random nums of run
            float randomSecondsForFirstMove = Random.Range(0.3f, 2f);
            yield return new WaitForSeconds(randomSecondsForFirstMove);
            spawnObj.GetComponent<EnemyMovement>().isFirstMove = false;
        }

        // enemies run
        yield return new WaitForSeconds(0.25f);

        this.gameState = GameState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        this.gameState = GameState.PLAYERTURN;

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