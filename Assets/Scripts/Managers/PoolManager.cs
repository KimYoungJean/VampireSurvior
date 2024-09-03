using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

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

    Dictionary<string , Pool> pools = new Dictionary<string, Pool>();

    public GameObject Pop(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab.name) )
        {
            CreatePool(prefab);
        }
        return pools[prefab.name].Pop();
    }
    
    void CreatePool(GameObject prefab)
    {
        Pool pool = new Pool(prefab);
        pools.Add(prefab.name, pool); 
    }

    public bool Push(GameObject go)
    {
        if(!pools.ContainsKey(go.name))
        {
            return false;
        }

        pools[go.name].Push(go);
        return true;
    }
}

class Pool
{
    GameObject prefab;
    IObjectPool<GameObject> pool;

    Transform root;
    Transform Root
    {
        get
        {
            if (root == null)
            {
                GameObject go = new GameObject() { name = $"{prefab.name} Root" };
                root = go.transform;
            }
            return root;
        }
    }

    public Pool(GameObject prefab)
    {
        this.prefab = prefab;
        pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
        
    }

    public GameObject Pop() //²¨³»±â
    {
        return pool.Get(); 
    }
    public void Push(GameObject go) //³Ö±â
    {
        pool.Release(go);
    }
    #region Funcs

    GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(Root);
        go.name = prefab.name;
        return go;
    }
    void OnGet(GameObject go)
    {
        go.SetActive(true);
    }
    void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }
    void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }
    #endregion
}

