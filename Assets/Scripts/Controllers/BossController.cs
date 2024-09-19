using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();
        _animator = GetComponent<Animator>();

        State = Define.MonsterState.Move;
        MaxHp = 10000;
        CurrentHp = MaxHp;
        return true;
    }
    
    public override void UpdateAnimation()
    {
     
        switch (State)
        {
            case Define.MonsterState.Idle:
                _animator.Play("Idle"); 
                break;
            case Define.MonsterState.Move:
                _animator.Play("Move"); 
                break;
            case Define.MonsterState.Attack:
                _animator.Play("Attack");
                break;
            case Define.MonsterState.Death:
                _animator.Play("Death");
                break;
        }
    }
    
    float range = 5.0f;
    protected override void UpdateMove()
    {
        PlayerController pc = GameManager.Instance.Player.GetComponent<PlayerController>();
        if(pc.IsValid() == false)
        {
            return;
        }

        Vector2 dir = pc.transform.position - transform.position;

        if(dir.sqrMagnitude < range * range)
        {
            State = Define.MonsterState.Attack;

            float animLength = _animator.GetCurrentAnimatorStateInfo(0).length; // 현재 애니메이션의 길이
            Wait(animLength); // 애니메이션 길이만큼 대기
        }       
    }

    protected override void UpdateAttack()
    {
        if(_coroutine ==null)
        {
            State = Define.MonsterState.Move;
        }
        

    }
    protected override void UpdateDeath()
    {
        if (_coroutine == null)
        {
            ObjectManager.instance.Despawn(this);
        }
    }

    #region WaitCoroutine
    Coroutine _coroutine;

    void Wait(float waitSeconds)
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(WaitCoroutine(waitSeconds));    
    }

    IEnumerator WaitCoroutine(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _coroutine = null; // 코루틴이 끝나면 null로 초기화
    }
    #endregion


    public override void OnDamaged(BaseController attacker, int Damage)
    {
      base.OnDamaged(attacker, Damage);
    }

    protected override void OnDead()
    {
        State = Define.MonsterState.Death;
        Wait(2.0f);
       
    }
}
