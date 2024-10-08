using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{

    #region StatePattern
    Define.MonsterState _state = Define.MonsterState.Move;
    public virtual Define.MonsterState State
    {
        get { return _state; }
        set
        {
            _state = value;            
            UpdateAnimation();
        }
    }

    protected Animator _animator;
    public virtual void UpdateAnimation()
    {

    }

    public override void UpdateController()
    {
        base.UpdateController();
        switch (State)
        {
            case Define.MonsterState.Idle:
                UpdateIdle();
                break;
            case Define.MonsterState.Move:
                UpdateMove();
                break;
            case Define.MonsterState.Attack:
                UpdateAttack();
                break;
            case Define.MonsterState.Death:
                UpdateDeath();
                break;
        }
    }
    protected virtual void UpdateIdle()
    {
        
    }
    protected virtual void UpdateMove()
    {

    }
    protected virtual void UpdateAttack()
    {

    }
    protected virtual void UpdateDeath()
    {

    }
    #endregion
    public override bool Init()
    {


        if (base.Init())
        {
            return false; // 이미 초기화를 했으므로 false를 반환한다.
        }

        State = Define.MonsterState.Move;

        objectType = Define.ObjectType.Monster;

        return true;
    }

    /*
   반환 값의 의미 차이
    true 반환: 초기화 작업이 실제로 이번에 처음으로 수행되었다는 것을 의미합니다. 즉, Init()이 호출되었을 때 객체가 아직 초기화되지 않았고, 이번 호출에서 초기화가 성공적으로 이루어졌다는 뜻입니다.

    false 반환: 이미 이전에 초기화가 완료된 상태이기 때문에 이번에는 초기화 작업이 수행되지 않았다는 것을 의미합니다. 즉, 이 호출에서는 아무런 초기화 작업이 필요하지 않았다는 뜻입니다.

    반환 값의 목적
    반환 값의 목적은 초기화가 수행되었는지 여부를 외부에서 알 수 있도록 하기 위함입니다. 이 정보는 외부 코드가 객체의 상태를 확인하고 적절한 조치를 취할 수 있게 해줍니다.

    예시 코드

    if (controller.Init())
    {
    // 초기화가 이번에 성공적으로 이루어졌을 때 수행할 작업
    Debug.Log("초기화 완료.");
    }
    else
    {
    // 이미 초기화된 경우에 수행할 작업
    Debug.Log("이미 초기화됨.");
    }
    위 코드에서, Init()이 true를 반환하면 "초기화 완료" 메시지를 출력하고, false를 반환하면 "이미 초기화됨" 메시지를 출력합니다. 이처럼 반환값을 이용해 초기화가 이번 호출에서 수행되었는지, 아니면 이미 완료된 상태였는지를 알 수 있습니다.

    왜 중요한가?
    상태 관리: 특정 상황에서는 객체가 초기화되었는지 여부에 따라 다른 로직을 실행해야 할 수 있습니다. Init()의 반환 값을 통해 이를 판단할 수 있습니다.

    효율성: 불필요한 초기화를 피하고, 로직을 효율적으로 관리할 수 있습니다. 이미 초기화된 객체라면 그에 맞는 다른 로직을 실행하도록 설계할 수 있습니다.

    명시성: true와 false의 반환을 통해 초기화 여부를 명시적으로 알 수 있어, 코드의 가독성과 유지보수성이 높아집니다.

    결론
    true와 false 모두 객체가 초기화된 상태를 나타내지만, true는 이번에 초기화가 이루어졌음을 의미하고, false는 이미 초기화된 상태였음을 나타냅니다. 이 차이를 통해 코드가 보다 명확하게 초기화 상태를 관리할 수 있게 됩니다.

     */

    //물리적인 계산은 FixedUpdate에서 처리
    private void FixedUpdate()
    {
        PlayerController player = ObjectManager.instance.Player;
        if (player == null)
            return;
        Vector3 dir = player.transform.position - transform.position;

        Vector3 pos = transform.position + (dir.normalized * Time.deltaTime * moveSpeed);

        GetComponent<Rigidbody2D>().MovePosition(pos);
        GetComponent<SpriteRenderer>().flipX = dir.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        //여기까지 됨

        if (target.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        if (_coDotDamage != null)
        {
            StopCoroutine(_coDotDamage);
        }

        _coDotDamage = StartCoroutine(CoDotDamage(target));


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        if (_coDotDamage != null)
        {
            StopCoroutine(_coDotDamage);
        }
        _coDotDamage = null;
    }
    Coroutine _coDotDamage;

    public IEnumerator CoDotDamage(PlayerController player)
    {

        while (true)
        {
            // 데미지 
            player.OnDamaged(this, 2);

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        if (_coDotDamage != null)
        {
            StopCoroutine(_coDotDamage);
        }
        _coDotDamage = null;

        GameManager.Instance.MonsterCount++; ;
        GemController gemController = ObjectManager.instance.Spawn<GemController>(transform.position);

        ObjectManager.instance.Despawn(this);
    }


}
