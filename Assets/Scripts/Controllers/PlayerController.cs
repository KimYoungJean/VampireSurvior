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

    public Transform Indicator { get { return indicator; } }
    public Vector3 FireSocket { get { return fireSocket.position; } }
    public Vector3 ShootDir { get { return (fireSocket.position-transform.position).normalized; } }

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

     /*   StartProjectile();
        StartSword();*/

        // 스킬 세팅
        // 다음에 UI와 연동할수 있게 작업해야함.
        Skills.AddSkill<FireballSkill>(Indicator.position);
        Skills.AddSkill<SwordController>(transform.position, gameObject.transform);

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
            string coinName = gem.GetComponent<SpriteRenderer>().sprite.name;
            


            Vector3 dir = gemController.transform.position - transform.position;
            if (dir.sqrMagnitude < sqrDistance)
            {

                switch (coinName)
                {
                    case "Gem1":
                        GameManager.Instance.tempGold = 1;
                        break;
                    case "Gem2":
                        GameManager.Instance.tempGold = 5;
                        break;
                    case "Gem3":
                        GameManager.Instance.tempGold = 10;
                        break;
                    default:
                        GameManager.Instance.tempGold = 0;
                        break;
                }

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
/*
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
            pc.SetInfo(1, this, (fireSocket.position - indicator.position).normalized);

            pc.transform.rotation = indicator.rotation;

            yield return wait;
        }
    }
    #endregion
    #region Sword

    SwordController swordController;
    void StartSword()
    {
        if (swordController.IsValid())
            return;
        swordController = ObjectManager.instance.Spawn<SwordController>(GameManager.Instance.Player.transform.position, Define.SwordSkillID);
        swordController.transform.SetParent(GameManager.Instance.Player.transform);

        swordController.ActiveSkill();
    }
    #endregion*/
}
