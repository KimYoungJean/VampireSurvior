using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerEx : MonoBehaviour
{
    public static SceneManagerEx instance;

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
