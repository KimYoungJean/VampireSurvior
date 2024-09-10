using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utility
{
    // 컴포넌트를 가져오고, 없다면 추가.
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    //자식을 찾는 함수, recursive가 true면 모든 자식을 찾는다. false면 한단계만 찾는다.
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

        if (recursive == false) //recuresive가 false 면 자식만 찾고 손자는 찾지 않는다.
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

    public static Vector2 GenerateMonsterSpawnPosition(Vector2 characterPosition, float minDistance = 10.0f, float maxDistance =20.0f)
    {
        //캐릭터를 기준으로 몬스터가 스폰되도록 함
        float angle = UnityEngine.Random.Range(0,360)*Mathf.Deg2Rad; 
        float distance = UnityEngine.Random.Range(minDistance, maxDistance);
        //Deg2Rad는 각도를 라디안으로 변환해주는 상수        

        float xDistance = Mathf.Cos(angle) * distance;
        float yDistance = Mathf.Sin(angle) * distance;

        Vector2 spawnPosition = characterPosition + new Vector2(xDistance, yDistance);

        return spawnPosition;

    }
}

