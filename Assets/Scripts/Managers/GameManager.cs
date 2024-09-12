using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region 재화
    
    int _gem = 0;

    public event Action<int> onGemChanged;
    

    public int Gem
    {
        get { return _gem; }
        set 
        {
            _gem = value;
            onGemChanged?.Invoke(value);
        }
    }

    int _tempGold = 0; 
    
    public event Action<int> onGoldChanged;
    public int tempGold
    {
        get { return _tempGold; }
        set
        {
            _tempGold = value;
            onGoldChanged?.Invoke(value);
        }
    }
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
    public GameObject Player;
    #endregion


    #region 몬스터처치
    int _monsterCount = 0;
    public event Action<int> onMonsterCountChanged;
    public int MonsterCount
    {
        get { return _monsterCount; }
        set
        {
            _monsterCount = value;
            onMonsterCountChanged?.Invoke(value);

        }
    }
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
