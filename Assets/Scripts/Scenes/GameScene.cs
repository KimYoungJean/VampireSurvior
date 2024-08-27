using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject goblinPrefab;
    public GameObject chickenPrefab;
    public GameObject wizardPrefab;
    public GameObject joystickPrefab;

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
        GameObject goblin = Instantiate(goblinPrefab);
        GameObject chicken = Instantiate(chickenPrefab);
        GameObject wizard = Instantiate(wizardPrefab);

        joystick = Instantiate(joystickPrefab);       


        
        goblin.transform.SetParent(monster.transform);  
        chicken.transform.parent = monster.transform;
        wizard.transform.SetParent(monster.transform);

        player.AddComponent<PlayerController>();

        Camera.main.GetComponent<CameraController>().target = player.transform;

    }
}
