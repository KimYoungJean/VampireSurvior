using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;

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

    UI_Base baseUI;

    Stack<UI_Base> UIStack = new Stack<UI_Base>();

    public T ShowPopup<T>() where T : UI_Base
    {
        string key = typeof(T).Name + ".prefab";

        T ui = ResourceManager.Instance.Instantiate(key, pooling: true).GetOrAddComponent<T>();

        UIStack.Push(ui);

        return ui;
    }

    public void ClosePopup()
    {
        if (UIStack.Count == 0) return;

        UI_Base ui = UIStack.Pop();
        ResourceManager.Instance.Destroy(ui.gameObject);
    }

    public T GetSceneUI<T>() where T : UI_Base
    {
        return baseUI as T;
    }

    public T ShowSceneUI<T>() where T : UI_Base
    {
        if (baseUI != null)
        {
           return GetSceneUI<T>();  
        }

        string key = typeof(T).Name + ".prefab";

        T ui = ResourceManager.Instance.Instantiate(key, pooling: true).GetOrAddComponent<T>();
        baseUI = ui;

        return ui;
    }
}
