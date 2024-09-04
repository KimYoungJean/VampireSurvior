using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : BaseController
{
    public override bool Init()
    {
        if (base.Init())
        {
            return false;
        }

        objectType = Define.ObjectType.Interactable;

        return true;
    }
}
