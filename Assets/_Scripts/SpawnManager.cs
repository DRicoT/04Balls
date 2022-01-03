using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyWave = 3;
    [SerializeField] int enemyCount;
    
    [SerializeField] GameObject powerUpPrefab;

    
    private float spawnRange = 9;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
    }
    /// <summary>
    /// Método que genera una posición aleatoria 
    /// </summary>
    /// <returns>Vector3 randomPos</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
   /// <summary>
   /// Método que genera 3 enemigos en pantalla
   /// <param name="numberOfEnemies">Número de enemigos a spawnear</param>
   /// </summary>
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
