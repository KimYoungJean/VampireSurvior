using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureController : BaseController
{
    protected float moveSpeed = 1.0f;
   
    public int CurrentHp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;


    public virtual void OnDamaged(BaseController attacker, int Damage)
    {
        Debug.Log($"{attacker}가 {Damage}만큼 데미지를 입힘");
        CurrentHp -= Damage;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        
    }
}
