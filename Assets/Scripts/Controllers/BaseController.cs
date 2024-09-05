using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    //protected Set �� ��� ���� Ŭ���������� ��� ����
    public Define.ObjectType objectType { get; protected set; } // ������Ʈ Ÿ���� {�÷��̾�,����,�߻�ü,ȯ�溯�� }�� ����
    
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

    private void Update()
    {
        UpdateController();
    }

    public virtual void UpdateController()
    {

    }

}
