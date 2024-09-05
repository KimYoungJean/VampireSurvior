using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 MoveDirection = Vector2.zero;
    
    Animator animator;
    Define.PlayerState playerState;

    float interactDistance { get; set; } = 1.0f;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
      

        GameManager.Instance.onMoveDirChanged += HandleMoveDirChanged;
        animator = GetComponent<Animator>();

    }
    void HandleMoveDirChanged(Vector2 dir)
    {
        
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

        GetComponent<SpriteRenderer>().flipX = MoveDirection.x < 0;

        transform.Translate(MoveDirection * moveSpeed * Time.deltaTime);

        if (MoveDirection != Vector2.zero)
        {
            playerState = Define.PlayerState.Move;
            SetAnimator(playerState);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerState = Define.PlayerState.Idle;
            SetAnimator(playerState);
        }
    }
    void Interact()
    {
        float sqrDistance = interactDistance * interactDistance;

        List<GemController> gemList = ObjectManager.instance.Gems.ToList(); //���� ����� �����´�.
        foreach (GemController gem in gemList)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if(dir.sqrMagnitude < sqrDistance)
            {
                GameManager.Instance.Gem += 1;
              ObjectManager.instance.Despawn(gem);
            }
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if(target == null)
        {
            return;
        }
    }
    public override void OnDamaged(BaseController attacker, int Damage)
    {
        
        base.OnDamaged(attacker, Damage);

        Debug.Log($"Player OnDamaged {Damage},{CurrentHp}");

        CreatureController monster = attacker as CreatureController;

        monster?.OnDamaged(this,10000);
    }
 }
