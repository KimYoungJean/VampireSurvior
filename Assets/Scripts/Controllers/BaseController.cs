using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    //protected Set 는 상속 받은 클래스에서만 사용 가능
    public Define.ObjectType objectType { get; protected set; }
    bool isInitialized = false;

    private void Awake()
    {
        Init(); 
    }

    public virtual bool Init()
    {        
        if(isInitialized)
        {
            //
            return false;
        }

        // 초기화 작업

        isInitialized = true;
        return true;
    }
    // 초기화를 안했으면 (isInitialized가 false이면) 초기화를 하고 true를 반환한다.
}
