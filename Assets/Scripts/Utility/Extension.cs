using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Extension
{
    public static bool IsValid(this BaseController baseController)
    {
        return baseController != null && baseController.isActiveAndEnabled;
    }
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utility.GetOrAddComponent<T>(go);
    }

    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}
