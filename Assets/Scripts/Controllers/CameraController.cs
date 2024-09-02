using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject target;
    [SerializeField]
    private float smoothSpeed = 0.0125f;

    
    private void LateUpdate() //ī�޶��� ��ġ�� ��� Update�� ���� �Ŀ� ������Ʈ �Ǿ�� �Ѵ�.
    {
        if (target == null)
            return;

        transform.position = 
            new Vector3(
                Mathf.Lerp(transform.position.x,target.transform.position.x,smoothSpeed),
                Mathf.Lerp(transform.position.y,target.transform.position.y, smoothSpeed),
                transform.position.z);

    }

    
}
