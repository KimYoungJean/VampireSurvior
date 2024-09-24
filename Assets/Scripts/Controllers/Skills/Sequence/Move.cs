using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SequenceSkill
{
    Rigidbody2D _rb;
    Coroutine _coroutine;

    private void Awake()
    {

    }

    public override void DoSkill(Action callBack = null)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        StartCoroutine(MoveCoroutine(callBack));
    }

    float move { get; } = 2.0f;
    string AnimationName { get; } = "Move";

    IEnumerator MoveCoroutine(Action callBack)
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().Play(AnimationName);

        float elapsedTime = 0.0f; // 경과 시간

        while (true)
        {

            elapsedTime += Time.deltaTime;

            if (elapsedTime > 5.0f)
            {
                break;
            }

            Vector3 dir = ((Vector2)GameManager.Instance.Player.transform.position - _rb.position).normalized;
            Vector2 targetPos = GameManager.Instance.Player.transform.position + dir * move;

            if (Vector2.SqrMagnitude(_rb.position - targetPos) < 0.01f)
            {
                continue;
            }

            Vector2 dirVec = targetPos - _rb.position; // 방향벡터
            Vector2 nextVec = dirVec.normalized * move * Time.deltaTime; // 방향벡터 * 속도 * 시간 = 이동량

            _rb.MovePosition(_rb.position + nextVec);

            yield return null;


        }
    }
}