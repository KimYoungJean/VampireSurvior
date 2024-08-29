using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
