using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SequenceSkill
{
    Rigidbody2D _rb;
    Coroutine _coroutine;

    public override void DoSkill(Action callBack = null)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(DashCoroutine(callBack));
    }

    float WaitTime { get; } = 1.0f;
    float Speed { get; } = 10.0f;

    string AnimationName { get; } = "Move";

    IEnumerator DashCoroutine(Action callBack)
    {
        _rb = GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(WaitTime);

        GetComponent<Animator>().Play(AnimationName);

        Vector3 dir = (GameManager.Instance.Player.transform.position - transform.position).normalized;

        Vector2 targetPos = GameManager.Instance.Player.transform.position + dir*UnityEngine.Random.Range(1,5);

        while (true)
        {
            if(Vector2.SqrMagnitude(targetPos - _rb.position) < 0.1f)
            {
                break;
            }


            Vector2 dirVec = targetPos - _rb.position; // 방향벡터
            Vector2 nextVec =  dirVec.normalized * Speed * Time.deltaTime; // 방향벡터 * 속도 * 시간 = 이동량

            _rb.MovePosition(_rb.position + nextVec); 

            yield return null;
        }

        callBack?.Invoke();

    }
}
