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
            return false; // �̹� �ʱ�ȭ�� �����Ƿ� false�� ��ȯ�Ѵ�.
        }

        State = Define.MonsterState.Move;

        objectType = Define.ObjectType.Monster;

        return true;
    }

    /*
   ��ȯ ���� �ǹ� ����
    true ��ȯ: �ʱ�ȭ �۾��� ������ �̹��� ó������ ����Ǿ��ٴ� ���� �ǹ��մϴ�. ��, Init()�� ȣ��Ǿ��� �� ��ü�� ���� �ʱ�ȭ���� �ʾҰ�, �̹� ȣ�⿡�� �ʱ�ȭ�� ���������� �̷�����ٴ� ���Դϴ�.

    false ��ȯ: �̹� ������ �ʱ�ȭ�� �Ϸ�� �����̱� ������ �̹����� �ʱ�ȭ �۾��� ������� �ʾҴٴ� ���� �ǹ��մϴ�. ��, �� ȣ�⿡���� �ƹ��� �ʱ�ȭ �۾��� �ʿ����� �ʾҴٴ� ���Դϴ�.

    ��ȯ ���� ����
    ��ȯ ���� ������ �ʱ�ȭ�� ����Ǿ����� ���θ� �ܺο��� �� �� �ֵ��� �ϱ� �����Դϴ�. �� ������ �ܺ� �ڵ尡 ��ü�� ���¸� Ȯ���ϰ� ������ ��ġ�� ���� �� �ְ� ���ݴϴ�.

    ���� �ڵ�

    if (controller.Init())
    {
    // �ʱ�ȭ�� �̹��� ���������� �̷������ �� ������ �۾�
    Debug.Log("�ʱ�ȭ �Ϸ�.");
    }
    else
    {
    // �̹� �ʱ�ȭ�� ��쿡 ������ �۾�
    Debug.Log("�̹� �ʱ�ȭ��.");
    }
    �� �ڵ忡��, Init()�� true�� ��ȯ�ϸ� "�ʱ�ȭ �Ϸ�" �޽����� ����ϰ�, false�� ��ȯ�ϸ� "�̹� �ʱ�ȭ��" �޽����� ����մϴ�. ��ó�� ��ȯ���� �̿��� �ʱ�ȭ�� �̹� ȣ�⿡�� ����Ǿ�����, �ƴϸ� �̹� �Ϸ�� ���¿������� �� �� �ֽ��ϴ�.

    �� �߿��Ѱ�?
    ���� ����: Ư�� ��Ȳ������ ��ü�� �ʱ�ȭ�Ǿ����� ���ο� ���� �ٸ� ������ �����ؾ� �� �� �ֽ��ϴ�. Init()�� ��ȯ ���� ���� �̸� �Ǵ��� �� �ֽ��ϴ�.

    ȿ����: ���ʿ��� �ʱ�ȭ�� ���ϰ�, ������ ȿ�������� ������ �� �ֽ��ϴ�. �̹� �ʱ�ȭ�� ��ü��� �׿� �´� �ٸ� ������ �����ϵ��� ������ �� �ֽ��ϴ�.

    ��ü�: true�� false�� ��ȯ�� ���� �ʱ�ȭ ���θ� ��������� �� �� �־�, �ڵ��� �������� ������������ �������ϴ�.

    ���
    true�� false ��� ��ü�� �ʱ�ȭ�� ���¸� ��Ÿ������, true�� �̹��� �ʱ�ȭ�� �̷�������� �ǹ��ϰ�, false�� �̹� �ʱ�ȭ�� ���¿����� ��Ÿ���ϴ�. �� ���̸� ���� �ڵ尡 ���� ��Ȯ�ϰ� �ʱ�ȭ ���¸� ������ �� �ְ� �˴ϴ�.

     */

    //�������� ����� FixedUpdate���� ó��
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
        //������� ��

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
            // ������ 
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
