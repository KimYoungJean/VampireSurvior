using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region ��ȭ
    
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

    #region �̵�

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
    public GameObject Player;
    #endregion


    #region ����óġ
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
