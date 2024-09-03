using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Object = UnityEngine.Object;
using Unity.VisualScripting;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private Dictionary<string, Object> _resources = new Dictionary<string, Object>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            return resource as T;
        }
        return null;

    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}"); 
        if(prefab == null)
        {
            Debug.Log($"{key} is not found");
            return null;
        }

        //TODO: pooling 구현
        if(pooling) // 오브젝트풀링이 들어간다면 풀링매니저에서 호출하고 리턴.
        {
            return PoolManager.instance.Pop(prefab);
        }

        GameObject poolingObject = Object.Instantiate(prefab, parent);
        poolingObject.name = prefab.name;
        return poolingObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if(gameObject == null)
        {
            return;
        }

        if(PoolManager.instance.Push(gameObject))
        {
            return;
        }

        Object.Destroy(gameObject);

    }

    // 비동기로 리소스를 읽어 오는 함수입니다. 만약 이미 딕셔너리에 리소스가존대한다면 콜백함수만을 호출하고
    // 그렇지 않다면 Addressables.LoadAssetAsync<T>를 통해 리소스를 읽어옵니다.
    // 여기서 x는 
    public void LoadAsync<T>(string key, Action<T> callback = null) where T: Object
    {
        if(_resources.TryGetValue(key, out Object resource))  // 딕셔너리에 Key가 존재한다면  resourece를 Object형식으로 반환.
        {
            callback?.Invoke(resource as T); // 콜백함수에 리소스를 T타입으로 변환하여 전달
            return;
        }

        Addressables.LoadAssetAsync<T>(key).Completed += (x) =>
        {
            _resources.Add(key, x.Result);
            callback?.Invoke(x.Result);
        };
        // completed는 비동기 작업이 완료되었을 때 호출되는 이벤트입니다. 
        // 콜백함수라고 봐도 무방합니다.
        // x가 ㅇ어떻게 resoure가 되는거야?
        
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int > callback) where T : Object
    {
        var operationHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        operationHandle.Completed += (operation) =>
        {
            int loadCount = 0;
            int totalCount = operation.Result.Count;

            foreach (var result in operation.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
                
            }
        };
    }
}
