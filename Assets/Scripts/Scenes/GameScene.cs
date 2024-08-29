using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject duckPrefab;
    public GameObject mushroomPrefab;
    public GameObject wolfPrefab;
    public GameObject pigPrefab;

    public GameObject joystricPrefab;

    public GameObject Player;
    private GameObject player;
    GameObject joystick;

    private void Awake()
    {
        player = Instantiate(Player);      
    }

    private void Start()
    {
        GameObject monster = new GameObject("Monster");
        GameObject duck = Instantiate(duckPrefab);
        GameObject mushroom = Instantiate(mushroomPrefab);
        GameObject wolf = Instantiate(wolfPrefab);
        GameObject pig = Instantiate(pigPrefab);

        joystick = Instantiate(joystricPrefab);       


        
        duck.transform.SetParent(monster.transform);  
        mushroom.transform.SetParent(monster.transform);
        wolf.transform.SetParent(monster.transform);
        pig.transform.SetParent(monster.transform);


        player.AddComponent<PlayerController>();

        Camera.main.GetComponent<CameraController>().target = player.transform;

    }
}
