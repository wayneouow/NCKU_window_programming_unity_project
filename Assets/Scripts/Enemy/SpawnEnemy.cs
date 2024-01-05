using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    //Prefab
    [SerializeField] GameObject enemy1Prefab;
    [SerializeField] GameObject enemy2Prefab;
    [SerializeField] GameObject enemy3Prefab;
    [SerializeField] GameObject enemy4Prefab;
    [SerializeField] GameObject enemy5Prefab;

    public int n1= 20;
    public int n2= 20;
    public int n3 = 20;
    public int n4 = 10;
    public int n5 = 1;


    public float spawnInterval = 10f;
    public float spawnRadius = 10f;
    public int wave = 0;
    public int score = 0;

    public AudioClip BossComing;
    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        score = 0;
        //Wave_Enemy();
        StartCoroutine(SpawnEnemies_1());
        StartCoroutine(SpawnEnemies_2());
        StartCoroutine(SpawnEnemies_3());
        StartCoroutine(SpawnEnemies_4());
        StartCoroutine(SpawnEnemies_5());
    }

    IEnumerator SpawnEnemies_1()
    {
        for (int i = 0; i < n1; i++)
        {
            Spawn_Enemy1();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnEnemies_2()
    {
        yield return new WaitForSeconds(10f);
        for (int i = 0; i < n2; i++)
        {
            Spawn_Enemy2();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnEnemies_3()
    {
        yield return new WaitForSeconds(20f);
        for (int i = 0; i < n3; i++)
        {
            Spawn_Enemy3();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnEnemies_4()
    {
        yield return new WaitForSeconds(30f);
        for (int i = 0; i < n4; i++)
        {
            Spawn_Enemy4();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnEnemies_5()
    {
        yield return new WaitForSeconds(50f);
        for (int i = 0; i < n5; i++)
        {
            Spawn_Enemy5();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void Spawn_Enemy1()
    {

        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        randomPosition.y = 1f;
        GameObject newEn1 = Instantiate(enemy1Prefab, randomPosition, Quaternion.identity);
        newEn1.gameObject.SetActive(true);
    }
    void Spawn_Enemy2()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        randomPosition.y = 1f;

        GameObject newEn2 = Instantiate(enemy2Prefab, randomPosition, Quaternion.identity);
        newEn2.gameObject.SetActive(true);
    }
    void Spawn_Enemy3()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        randomPosition.y = 1f;

        GameObject newEn3 = Instantiate(enemy3Prefab, randomPosition, Quaternion.identity);
        newEn3.gameObject.SetActive(true);
    }
    void Spawn_Enemy4()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        randomPosition.y = 1f;

        GameObject newEn4 = Instantiate(enemy4Prefab, randomPosition, Quaternion.identity);
        newEn4.gameObject.SetActive(true);
    }
    void Spawn_Enemy5()
    {


        GetComponent<AudioSource>().PlayOneShot(BossComing);
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        //randomPosition += PlayerManager.instance.player.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        randomPosition += player.transform.position;
        randomPosition.y = 40f;

        GameObject newEn5 = Instantiate(enemy5Prefab, randomPosition, Quaternion.identity);
        newEn5.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
