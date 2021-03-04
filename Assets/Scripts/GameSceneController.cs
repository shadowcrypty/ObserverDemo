using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{ public event EnemyDestroyedHandler UpdateToKill;
    #region Field Declarations

    [Header("Enemy & Power Prefabs")]
    [Space]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private PlayerController playerShip;
    [SerializeField] private PowerupController[] powerUpPrefabs;
	
	[Header("Level Definitions")]
    [Space]
    public List<LevelDefinition> levels;
    [HideInInspector] public LevelDefinition currentLevel;

    [Header("Player ship settings")][Space]
    [Range(3, 8)]
    public float playerSpeed = 5;
    [Range(1, 10)]
    public float shieldDuration = 3;

    private int totalPoints;
    private int lives = 3;

    private int currentLevelIndex = 0;
    private WaitForSeconds shipSpawnDelay = new WaitForSeconds(2);

    #endregion

    #region Startup

    void Start()
    {
        StartLevel(currentLevelIndex);
    }

    #endregion

    #region Level Management

    private void StartLevel(int levelIndex)
	{
     	currentLevel = levels[levelIndex];

        StartCoroutine(SpawnShip(false));
        StartCoroutine(SpawnEnemies());

        if (currentLevel.hasPowerUps)
            StartCoroutine(SpawnPowerUp());
    }

    private void EndLevel()
    {
        currentLevelIndex++;
        StopAllCoroutines();

        //If last level the game over
        if (currentLevelIndex < levels.Count)
        {
            //TODO: Clean up
            StartLevel(currentLevelIndex);
        }
    }

    #endregion

    #region Spawning

    private IEnumerator SpawnShip(bool delayed)
    {
        if(delayed)
            yield return shipSpawnDelay;

        PlayerController ship = Instantiate(playerShip, new Vector2(0, -4.67f), Quaternion.identity);
        ship.speed = playerSpeed;
        ship.shieldDuration = shieldDuration;

        yield return null;
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(currentLevel.enemySpawnDelay);
        yield return wait;

        for (int i = 0; i < currentLevel.numberOfEnemies; i++)
        {
            Vector2 spawnPosition = ScreenBounds.RandomTopPosition();

            EnemyController enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
            enemy.shotSpeed = currentLevel.enemyShotSpeed;
            enemy.speed = currentLevel.enemySpeed;
            enemy.shotdelayTime = currentLevel.enemyShotDelay;
            enemy.angerdelayTime = currentLevel.enemyAngerDelay;
            enemy.EnemyDestroyed += Enemy_EnemyDestroyed;
 
            yield return wait;
        }
    }

    private void Enemy_EnemyDestroyed(int pointValue)
    {
        totalPoints = pointValue;
        if (UpdateToKill!=null)
        {
            UpdateToKill(totalPoints);

        }
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            int index = UnityEngine.Random.Range(0, powerUpPrefabs.Length);
            Vector2 spawnPosition = ScreenBounds.RandomTopPosition();
            Instantiate(powerUpPrefabs[index], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(currentLevel.powerUpMinimumWait,currentLevel.powerUpMaximumWait));
        }
    }

    #endregion
}
