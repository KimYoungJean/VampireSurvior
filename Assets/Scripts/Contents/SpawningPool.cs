using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    float spawnTime = 1.0f;
    int maxSpawnCount = 100;
    Coroutine spawnCoroutine;


    
    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

   
    IEnumerator SpawnCoroutine()
    {
    while(true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void Spawn()
    {
        int monsterCount = ObjectManager.instance.Monsters.Count;
        if (monsterCount >= maxSpawnCount)
        {
            return;
        }

        MonsterController monsterController = ObjectManager.instance.Spawn<MonsterController>(Random.Range(0, 4));
        monsterController.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
    }
}
 