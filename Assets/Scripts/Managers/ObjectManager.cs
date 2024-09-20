using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public PlayerController Player { get; private set; } //플레이어

    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>(); //몬스터 스폰 목록
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>(); //발사체 스폰 목록
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>(); //보석 스폰 목록


    public T Spawn<T>(Vector3 position, int templateID = 0) where T : BaseController
    {
        System.Type type = typeof(T); //T의 타입을 가져온다.

        if (type == typeof(PlayerController))
        {
            GameObject player = ResourceManager.Instance.Instantiate("Player.prefab", pooling: true);
            player.name = "Player";
            player.transform.position = position;

            PlayerController playerController = player.GetOrAddComponent<PlayerController>();
            Player = playerController;
            playerController.Init();

            return playerController as T;
        }
        else if (type == typeof(MonsterController))
        {
            string monsterName;

            switch (templateID)
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
                case Define.BOSS01_ID:
                    monsterName = "Boss01";
                    break;
                default:
                    monsterName = "Duck";
                    break;
            }

            GameObject monster = ResourceManager.Instance.Instantiate($"{monsterName}.prefab", pooling: true);
            monster.transform.position = position;

            MonsterController monsterController = monster.GetOrAddComponent<MonsterController>();
            Monsters.Add(monsterController);
            monsterController.Init();

            return monsterController as T;

        }
        else if (type == typeof(GemController))
        {
            GameObject gemObject = ResourceManager.Instance.Instantiate("Gem.prefab", pooling: true);
            gemObject.transform.position = position;

            GemController gemController = gemObject.GetOrAddComponent<GemController>();
            Gems.Add(gemController);
            gemController.Init();

            int random = Random.Range(1, 4); // 랜덤으로 1~3까지의 숫자를 생성한다.
            string Key = $"Gem{random}.sprite";

            Sprite gemSprite = ResourceManager.Instance.Load<Sprite>(Key);
            gemObject.GetComponent<SpriteRenderer>().sprite = gemSprite;


            GameObject.Find("Grid").GetComponent<GridController>().Add(gemObject);
            return gemController as T;
        }
        else if (type == typeof(ProjectileController))
        {
            GameObject projectileObject = ResourceManager.Instance.Instantiate("Fireball.prefab", pooling: true);
            projectileObject.transform.position = position;

            ProjectileController pc = projectileObject.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(pc);
            pc.Init();

            return pc as T;

        }
        else if (typeof(T).IsSubclassOf(typeof(SkillBase)))
        {
            
            if (DataManager.Instance.SkillDic.TryGetValue(templateID, out Data.SkillData data) == false)
            {
                Debug.LogError($"Skill Controller SetInfo Failed ID:{templateID}");
                return null;
            }

            GameObject skillObject = ResourceManager.Instance.Instantiate($"{data.name}.prefab", pooling: true);
            skillObject.transform.position = position;

            T t = skillObject.GetOrAddComponent<T>();
          

            return t;
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
        else if(type == typeof(BossController))
        {
            Monsters.Remove(target as BossController);
            ResourceManager.Instance.Destroy(target.gameObject);
        }
        else if (type == typeof(GemController))
        {
            Gems.Remove(target as GemController);
            ResourceManager.Instance.Destroy(target.gameObject);

            GameObject.Find("Grid").GetComponent<GridController>().Remove(target.gameObject);
        }
        else if(type == typeof(ProjectileController))
        {            
            Projectiles.Remove(target as ProjectileController);
            ResourceManager.Instance.Destroy (target.gameObject);
        }
        else if (type == typeof(SkillBase))
        {            
            Projectiles.Remove(target as ProjectileController);
            ResourceManager.Instance.Destroy(target.gameObject);
        }

    }
    public void DespawnAllMonster()
    {
        var monsters = Monsters.ToList();
        foreach (var monster in monsters)
        {
            Despawn(monster);

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
