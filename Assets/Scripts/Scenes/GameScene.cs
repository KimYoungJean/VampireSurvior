using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    GameObject duck;
    GameObject mushroom;
    GameObject wolf;
    GameObject pig;
    GameObject joystick;

    GameObject player;


    /*  private void Awake()
      {
          // ���ҽ� �Ŵ����� ���� �񵿱�� �÷��̾� �������� �о�ɴϴ�.
          // ���⼭ ���ٽ��� ���ڴ� ���ҽ� �Ŵ������� �о�� ���ҽ��� �ǹ��մϴ�.
          ResourceManager.Instance.LoadAsync<GameObject>("Player.prefab",
              (x) =>
              {
                  Player = x;
                  player = Instantiate(Player);

                  player.AddComponent<PlayerController>();

                  Camera.main.GetComponent<CameraController>().target = player.transform;
              });


      }*/

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

    void Init()
    {

        player = ResourceManager.Instance.Instantiate("Player.prefab");
        player.AddComponent<PlayerController>();
        Camera.main.GetComponent<CameraController>().target = player.transform;

       joystick = ResourceManager.Instance.Instantiate("Joystick.prefab");

       GameObject monster = new GameObject("Monsters");
       duck = ResourceManager.Instance.Instantiate("Duck.prefab", monster.transform);
       mushroom = ResourceManager.Instance.Instantiate("Mushroom.prefab", monster.transform);
       wolf = ResourceManager.Instance.Instantiate("Wolf.prefab", monster.transform);
       pig = ResourceManager.Instance.Instantiate("Pig.prefab", monster.transform);


    }
}
