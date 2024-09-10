using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 MoveDirection = Vector2.zero;

    [SerializeField]
    Transform indicator;
    [SerializeField]
    Transform fireSocket;
    Animator animator;
    Define.PlayerState playerState;

    float interactDistance { get; set; } = 0.5f;

    public override bool Init()

    {
        if (base.Init() == false)
            return false;

        moveSpeed = 3.0f;


        GameManager.Instance.onMoveDirChanged += HandleMoveDirChanged;
        GameManager.Instance.Player = this.gameObject;
        animator = GetComponent<Animator>();

        StartProjectile();

        return true;
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
        Interact();
    }

    void Move()
    {

        GetComponent<SpriteRenderer>().flipX = MoveDirection.x < 0;

        Vector3 dir = MoveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(dir);

        if (MoveDirection != Vector2.zero)
        {
            playerState = Define.PlayerState.Move;
            SetAnimator(playerState);
            indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
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


        var findGems = GameObject.Find("Grid").GetComponent<GridController>().GatherObjects(transform.position, interactDistance + 0.7f);

        foreach (var gem in findGems)
        {
            GemController gemController = gem.GetComponent<GemController>();

            Vector3 dir = gemController.transform.position - transform.position;
            if (dir.sqrMagnitude < sqrDistance)
            {
                GameManager.Instance.Gem += 1;
                ObjectManager.instance.Despawn(gemController);
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
        if (target == null)
        {
            return;
        }
    }
    public override void OnDamaged(BaseController attacker, int Damage)
    {

        base.OnDamaged(attacker, Damage);

        Debug.Log($"Player OnDamaged {Damage},{CurrentHp}");

        CreatureController monster = attacker as CreatureController;

        monster?.OnDamaged(this, 10000);
    }

    #region Fireball
    Coroutine coFireball;
    void StartProjectile()
    {
        if (coFireball != null)
        {
            StopCoroutine(coFireball);
        }

        coFireball = StartCoroutine(CoStartProjectile());
    }

    IEnumerator CoStartProjectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            ProjectileController pc = ObjectManager.instance.Spawn<ProjectileController>(fireSocket.position, 1);
            pc.SetInfo(1, this, (fireSocket.position-indicator.position).normalized);

            pc.transform.rotation= indicator.rotation;

            yield return wait;
        }
    }
    #endregion

}
