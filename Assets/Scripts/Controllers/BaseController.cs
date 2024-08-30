using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    //protected Set �� ��� ���� Ŭ���������� ��� ����
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

        // �ʱ�ȭ �۾�

        isInitialized = true;
        return true;
    }
    // �ʱ�ȭ�� �������� (isInitialized�� false�̸�) �ʱ�ȭ�� �ϰ� true�� ��ȯ�Ѵ�.
}
