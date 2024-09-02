using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PullManager : MonoBehaviour
{
    public static PullManager instance;

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

class Pool
{
    GameObject prefab;
    IObjectPool<GameObject> pool;

    public Pool (GameObject prefab)
    {
        this.prefab = prefab;
        //pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }
}

