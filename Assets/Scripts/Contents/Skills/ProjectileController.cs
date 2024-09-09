using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : SkillController
{
    CreatureController player;
    Vector3 moveDir;
    float spped = 10.0f;
    float lifeTime = 10.0f;

    public override bool Init()
    {
        base.Init();

        StartDestroy(lifeTime);

        return true;

    }
    public void SetInfo(int templateId,CreatureController player, Vector3 moveDir )
    {
        if(DataManager.Instance.SkillDic.TryGetValue(templateId, out Data.SkillData data)==false)
        {
            Debug.LogError("Projectile Controller SetInfo Failed");
        }

        this.player = player;
        this.moveDir = moveDir;
        SkillData = data;



    }


    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += moveDir * spped * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController monsterController = collision.GetComponent<MonsterController>();
        if (monsterController.IsVaild() == false)
            return;
        if(this.IsVaild() == false)
                return;
        monsterController.OnDamaged(player, SkillData.damage);

        StopDestroy();
        ObjectManager.instance.Despawn(this);

    }
}
