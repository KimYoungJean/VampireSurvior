using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region 재화
    public int Gold { get; set; }
    public int Gem { get; set; }
    #endregion

    #region 이동

    public event Action<Vector2> onMoveDirChanged; // 이벤트 선언 : 리턴 x, 파라미터 o
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
    public  GameObject Player;
    #endregion


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
