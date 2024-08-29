using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 MoveDirection = Vector2.zero;
    float MoveSpeed = 2f;
    Animator animator;
    Define.PlayerState playerState;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;

        GameManager.Instance.onMoveDirChanged += HandleMoveDirChanged;
        animator = GetComponent<Animator>();

    }
    void HandleMoveDirChanged(Vector2 dir)
    {
        Debug.Log("MoveDirChanged");
        MoveDirection = dir;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.onMoveDirChanged -= HandleMoveDirChanged;
    }
    private void Update()
    {

        Move();
    }

    void Move()
    {
        Debug.Log("Move");
        if (MoveDirection.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else { GetComponent<SpriteRenderer>().flipX = false; }

        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);

        if (MoveDirection != Vector2.zero)
        {
            playerState = Define.PlayerState.Move;
            SetAnimator(playerState);
        }
        else
        {
            playerState = Define.PlayerState.Idle;
            SetAnimator(playerState);
        }
    }

    public void SetAnimator(Define.PlayerState state)
    {
        switch (state)
        {
            case Define.PlayerState.Idle:
                animator.SetBool("isMove", false);
                break;
            case Define.PlayerState.Move:
                animator.SetBool("isMove", true);
                break;
            case Define.PlayerState.Attack:
                break;
            case Define.PlayerState.Die:
                break;
        }
    }


}
