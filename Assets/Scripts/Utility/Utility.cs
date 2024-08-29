using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utility
{
    // ������Ʈ�� ��������, ���ٸ� �߰�.
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    //�ڽ��� ã�� �Լ�, recursive�� true�� ��� �ڽ��� ã�´�. false�� �Ѵܰ踸 ã�´�.
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
        {
            return null;
        }

        return transform.gameObject;
    }


    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Component
    {
        if (go == null)
        {
            return null;
        }

        if (recursive == false) //recuresive�� false �� �ڽĸ� ã�� ���ڴ� ã�� �ʴ´�.
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }
}

