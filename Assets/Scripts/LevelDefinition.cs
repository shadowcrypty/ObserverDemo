using UnityEngine;
[CreateAssetMenu(menuName = "Custom/Level Definition", fileName = "NewLevelDefinition")]
public class LevelDefinition : ScriptableObject
{
    public string levelName;
    public int numberOfEnemies;
    public bool hasPowerUps;
    public float enemySpawnDelay;
    public float enemyAngerDelay;
    public float enemyShotDelay;
    public float enemyShotSpeed;
    public int enemySpeed;
    public float powerUpMinimumWait;
    public float powerUpMaximumWait;

}
