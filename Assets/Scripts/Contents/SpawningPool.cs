using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    float spawnTime = 0.1f;
    int maxSpawnCount = 100;
    Coroutine spawnCoroutine;
    public bool SpawningPause { get; set; } = false; 



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
        if(SpawningPause==true)
        {
            return;
        }

        int monsterCount = ObjectManager.instance.Monsters.Count;
        if (monsterCount >= maxSpawnCount)
        {
            return;
        }

        Vector3 randPos = Utility.GenerateMonsterSpawnPosition(GameManager.Instance.Player.transform.position, 10.0f, 15.0f);
        MonsterController monsterController = ObjectManager.instance.Spawn<MonsterController>(randPos,Random.Range(0, 4));
        
    }
}
 