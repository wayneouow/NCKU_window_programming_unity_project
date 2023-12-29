using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    //Prefab
    [SerializeField] GameObject enemy1Prefab;
    [SerializeField] GameObject enemy2Prefab;
    [SerializeField] GameObject enemy3Prefab;


    [SerializeField] int numberOfEnemiesToSpawn;
    public float spawnInterval = 20f;
    public float spawnRadius = 10f;
    public int wave = 0;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        score = 0;
        //Wave_Enemy();
        StartCoroutine(SpawnEnemies_1());
        StartCoroutine(SpawnEnemies_2());
        StartCoroutine(SpawnEnemies_3());
    }

    IEnumerator SpawnEnemies_1()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Spawn_Enemy1();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnEnemies_2()
    {
        //10秒後
        yield return new WaitForSeconds(10f);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Spawn_Enemy2();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnEnemies_3()
    {
        //20秒後
        yield return new WaitForSeconds(20f);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Spawn_Enemy3();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void Spawn_Enemy1()
    {
        // 在玩家附近隨機生成敵人的位置
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        // 限制 Y 軸高度，可以根據實際情況調整
        randomPosition.y = 0f;

        // 生成敵人
        Instantiate(enemy1Prefab, randomPosition, Quaternion.identity);
    }
    void Spawn_Enemy2()
    {
        // 在玩家附近隨機生成敵人的位置
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        // 限制 Y 軸高度，可以根據實際情況調整
        randomPosition.y = 0f;

        // 生成敵人
        Instantiate(enemy2Prefab, randomPosition, Quaternion.identity);
    }
    void Spawn_Enemy3()
    {
        // 在玩家附近隨機生成敵人的位置
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        // 限制 Y 軸高度，可以根據實際情況調整
        randomPosition.y = 0f;

        // 生成敵人
        Instantiate(enemy3Prefab, randomPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
