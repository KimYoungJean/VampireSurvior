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
        UImanager.instance.ShowSceneUI<UI_GameScene>();

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


        GameManager.Instance.onGemChanged += HandleOnGemCountChanged;        
        GameManager.Instance.onGoldChanged += HandleOnGoldChanged;  
        GameManager.Instance.onMonsterCountChanged += HandleOnMonsterCountChanged;

    }
    int collectedGem = 0;
    int requiredGem = 10;
    int level = 1;
    int collectGold = 0;
    int killCount;

    public void HandleOnGemCountChanged(int value)
    {
        collectedGem++;        
        if (collectedGem == requiredGem)
        {

            UImanager.instance.ShowPopup<UI_SkillChoice>();
            collectedGem = 0;
            requiredGem *= 2;
            level++;
        }

        UImanager.instance.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)collectedGem/requiredGem); //UI_GameScene의 SetGemCountRatio 함수를 호출
        UImanager.instance.GetSceneUI<UI_GameScene>().SetLevel(level);
    }
    public void HandleOnGoldChanged(int value)
    {
        collectGold += value;
        Debug.Log($"{value}추가하여 {collectGold}가 되었습니다.");
        UImanager.instance.GetSceneUI<UI_GameScene>().SetGold(collectGold);

    }
    public void HandleOnMonsterCountChanged(int value)
    {
        killCount++;
        if(killCount==10)
        {
            //보스등장
            Debug.Log("Boss");
        }
        UImanager.instance.GetSceneUI<UI_GameScene>().SetKillCount(killCount);
    }
    
    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        GameManager.Instance.onGemChanged -= HandleOnGemCountChanged;

    }

}
