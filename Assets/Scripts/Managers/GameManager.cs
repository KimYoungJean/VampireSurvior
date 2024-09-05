using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Gold { get; set; }
    public int Gem { get; set; }

    public event Action<Vector2> onMoveDirChanged; // �̺�Ʈ ���� : ���� x, �Ķ���� o
    private Vector2 moveDir;
        

    public Vector2 MoveDir
    {
        get
        {
            return moveDir;
        }
        set
        {
            moveDir = value;

            onMoveDirChanged?.Invoke(moveDir); 
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
