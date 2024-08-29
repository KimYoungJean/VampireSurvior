using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 MoveDirection = Vector2.zero;
    float MoveSpeed = 2f;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;

        GameManager.Instance.onMoveDirChanged+=HandleMoveDirChanged;
        
    }
    void HandleMoveDirChanged(Vector2 dir)
    {
        Debug.Log("MoveDirChanged");
        MoveDirection = dir;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null) 
            GameManager.Instance.onMoveDirChanged -= HandleMoveDirChanged;
    }
    private void Update()
    {
      
        Move();
    }

    void Move()
    {
        Debug.Log("Move");   
        if (MoveDirection.x<0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else { GetComponent<SpriteRenderer>().flipX = false;}

        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
    }
}
