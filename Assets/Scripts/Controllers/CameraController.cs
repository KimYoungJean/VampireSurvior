using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject target;
    [SerializeField]
    private float smoothSpeed = 0.0125f;

    
    private void LateUpdate() //카메라의 위치는 모든 Update가 끝난 후에 업데이트 되어야 한다.
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
