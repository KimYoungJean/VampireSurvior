using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordChild : MonoBehaviour
{

    BaseController player;

    int damage;

    public void SetInfo(BaseController player, int damage)
    {
        this.player = player;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       MonsterController monsterController = other.GetComponent<MonsterController>();
       if(monsterController.IsValid() ==false)
        {
            return;
        }
        monsterController.OnDamaged(player,damage);
    }
}
