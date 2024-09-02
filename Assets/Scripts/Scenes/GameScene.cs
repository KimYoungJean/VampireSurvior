using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{
   

    private void Start()
    {
        ResourceManager.Instance.LoadAllAsync<GameObject>("Prefabs", (key, index, totalCount) =>
        {
            Debug.Log($"Loading {key} {index}/{totalCount}");

            if (index == totalCount)
            {
                Init();

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
        var player = ObjectManager.instance.Spawn<PlayerController>();

        Camera.main.GetComponent<CameraController>().target = player.gameObject;

        var joystick = ResourceManager.Instance.Instantiate("Joystick.prefab");


        for(int i=0; i < 10; i++)
        {
            MonsterController monsterController = ObjectManager.instance.Spawn<MonsterController>(Random.Range(0,4));
            monsterController.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        }
        


    }
}
