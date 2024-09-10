using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaptileController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Camera camera = collision.GetComponent<Camera>();
        if (camera == null) return;

        Debug.Log("충돌했습니다");        
        Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;

        float dirX = dir.x<0 ? -1 : 1;
        float dirY = dir.y < 0 ? -1 : 1;

        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            transform.Translate(Vector3.right * dirX * 28);
        }
        else
        {
           transform.Translate(Vector3.up * dirY * 40);
        }

    }
}
