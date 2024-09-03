using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public PlayerController Player { get; private set; } //플레이어

    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>(); //몬스터 스폰 목록
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>(); //발사체 스폰 목록
    
    
    public T Spawn<T>(int templateID = 0) where T : BaseController
    {
        System.Type type = typeof(T); //T의 타입을 가져온다.

        if (type == typeof(PlayerController))
        {
            GameObject player = ResourceManager.Instance.Instantiate("Player.prefab",pooling:true);
            player.name = "Player";

            PlayerController playerController = player.GetOrAddComponent<PlayerController>();
            Player = playerController;

            return playerController as T;
        }
        else if (type == typeof(MonsterController))
        {
            string monsterName;

            switch(templateID)
            {
                case 0:
                    monsterName = "Duck";
                    break;
                case 1:
                    monsterName = "Mushroom";
                    break;
                case 2:
                    monsterName = "Wolf";
                    break;
                case 3:
                    monsterName = "Pig";
                    break;
                default:
                    monsterName = "Duck";
                    break;
            }

            GameObject monster = ResourceManager.Instance.Instantiate($"{monsterName}.prefab",pooling:true);          

            MonsterController monsterController = monster.GetOrAddComponent<MonsterController>();
            Monsters.Add(monsterController);

            return monsterController as T;

        }

        return null;

    }

    public void Despawn<T>(T target) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {

        }
        else if (type == typeof(MonsterController))
        {
            Monsters.Remove(target as MonsterController);
            ResourceManager.Instance.Destroy(target.gameObject);
        }
        else if(type == typeof(ProjectileController))
        {
            Projectiles.Remove(target as ProjectileController);
            ResourceManager.Instance.Destroy(target.gameObject);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
}
