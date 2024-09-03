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

        //TODO: pooling ����
        if(pooling) // ������ƮǮ���� ���ٸ� Ǯ���Ŵ������� ȣ���ϰ� ����.
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

    // �񵿱�� ���ҽ��� �о� ���� �Լ��Դϴ�. ���� �̹� ��ųʸ��� ���ҽ��������Ѵٸ� �ݹ��Լ����� ȣ���ϰ�
    // �׷��� �ʴٸ� Addressables.LoadAssetAsync<T>�� ���� ���ҽ��� �о�ɴϴ�.
    // ���⼭ x�� 
    public void LoadAsync<T>(string key, Action<T> callback = null) where T: Object
    {
        if(_resources.TryGetValue(key, out Object resource))  // ��ųʸ��� Key�� �����Ѵٸ�  resourece�� Object�������� ��ȯ.
        {
            callback?.Invoke(resource as T); // �ݹ��Լ��� ���ҽ��� TŸ������ ��ȯ�Ͽ� ����
            return;
        }

        Addressables.LoadAssetAsync<T>(key).Completed += (x) =>
        {
            _resources.Add(key, x.Result);
            callback?.Invoke(x.Result);
        };
        // completed�� �񵿱� �۾��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �̺�Ʈ�Դϴ�. 
        // �ݹ��Լ���� ���� �����մϴ�.
        // x�� ����� resoure�� �Ǵ°ž�?
        
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
