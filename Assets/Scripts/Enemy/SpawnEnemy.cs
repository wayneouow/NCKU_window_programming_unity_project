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
        //10���
        yield return new WaitForSeconds(10f);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Spawn_Enemy2();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnEnemies_3()
    {
        //20���
        yield return new WaitForSeconds(20f);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Spawn_Enemy3();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void Spawn_Enemy1()
    {
        // �b���a�����H���ͦ��ĤH����m
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        // ���� Y �b���סA�i�H�ھڹ�ڱ��p�վ�
        randomPosition.y = 0f;

        // �ͦ��ĤH
        Instantiate(enemy1Prefab, randomPosition, Quaternion.identity);
    }
    void Spawn_Enemy2()
    {
        // �b���a�����H���ͦ��ĤH����m
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        // ���� Y �b���סA�i�H�ھڹ�ڱ��p�վ�
        randomPosition.y = 0f;

        // �ͦ��ĤH
        Instantiate(enemy2Prefab, randomPosition, Quaternion.identity);
    }
    void Spawn_Enemy3()
    {
        // �b���a�����H���ͦ��ĤH����m
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        // ���� Y �b���סA�i�H�ھڹ�ڱ��p�վ�
        randomPosition.y = 0f;

        // �ͦ��ĤH
        Instantiate(enemy3Prefab, randomPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
