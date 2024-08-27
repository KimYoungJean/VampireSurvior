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
        
    }

    private void Update()
    {
        //UpdateInput();
        Move();
    }
    void UpdateInput()
    {

        MoveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection.y += 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            MoveDirection.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveDirection.x -= 1;
            GetComponent<SpriteRenderer>().flipX = true;

        }      

        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection.x += 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        MoveDirection.Normalize();

    }

    void Move()
    {
        MoveDirection = GameManager.Instance.MoveDir;
        if (MoveDirection.x<0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else { GetComponent<SpriteRenderer>().flipX = false;}

        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
    }
}
