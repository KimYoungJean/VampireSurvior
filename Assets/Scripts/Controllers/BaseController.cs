using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    //protected Set 는 상속 받은 클래스에서만 사용 가능
    public Define.ObjectType objectType { get; protected set; } // 오브젝트 타입은 {플레이어,몬스터,발사체,환경변수 }로 구분
    
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

    private void Update()
    {
        UpdateController();
    }

    public virtual void UpdateController()
    {

    }

}
