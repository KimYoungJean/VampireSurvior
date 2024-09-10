using Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    SpawningPool spawningPool;

    private void Start()
    {
        ResourceManager.Instance.LoadAllAsync<GameObject>("Prefabs", (key, index, totalCount) =>
        {
            Debug.Log($"Loading {key} {index}/{totalCount}");

            if (index == totalCount)
            {
                ResourceManager.Instance.LoadAllAsync<TextAsset>("Data", (key, index, totalCount) =>
                {
                    Debug.Log($"Loading {key} {index}/{totalCount}");
                    if (index == totalCount)
                    {

                        ResourceManager.Instance.LoadAllAsync<Sprite>("Sprite", (key, index, totalCount) =>
                        {
                            Debug.Log($"Loading {key} {index}/{totalCount}");
                            if (index == totalCount)
                                Init();
                        });                       
                    
                    }
                });
            }
        });


    }


    /* void Init()
     {

         var player = ResourceManager.Instance.Instantiate("Player.prefab");
         player.AddComponent<PlayerController>();
         Camera.main.GetComponent<CameraController>().target = player.transform;

        var joystick = ResourceManager.Instance.Instantiate("Joystick.prefab");

        var  monster = new GameObject("Monsters");
        var duck = ResourceManager.Instance.Instantiate("Duck.prefab", monster.transform);
        var mushroom = ResourceManager.Instance.Instantiate("Mushroom.prefab", monster.transform);
        var wolf = ResourceManager.Instance.Instantiate("Wolf.prefab", monster.transform);
        var pig = ResourceManager.Instance.Instantiate("Pig.prefab", monster.transform);


     }*/

    void Init()
    {
        DataManager.Instance.Init();

        var player = ObjectManager.instance.Spawn<PlayerController>(Vector3.zero);
        

        spawningPool = gameObject.GetOrAddComponent<SpawningPool>();

        for (int i = 0; i < 10; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
            MonsterController monsterController = ObjectManager.instance.Spawn<MonsterController>(randPos, Random.Range(0, 4));            
        }


        Camera.main.GetComponent<CameraController>().target = player.gameObject;

        var joystick = ResourceManager.Instance.Instantiate("Joystick.prefab");
        var map = ResourceManager.Instance.Instantiate("Map_Spring.prefab");


        foreach (var data in DataManager.Instance.PlayerDic.Values)
        {
            
            Debug.Log($"level:{data.level} maxHp:{data.maxHp} attack:{data.attack} totalExp:{data.totalExp}");
        }
        foreach (var data in DataManager.Instance.SkillDic.Values)
        {
            //
            Debug.Log($"templateID ={data.templateID}, name = {data.name}, type = {data.type},prefab = {data.prefab}, damage = {data.damage}");
            
        }

    }

    
}
