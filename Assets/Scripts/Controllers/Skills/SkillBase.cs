using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//
public class SkillBase : BaseController
{
    public CreatureController Owner { get;set; }
    public Define.SkillType SkillType { get;  set; } = Define.SkillType.None;
    public Data.SkillData SkillData { get; protected set; }

    public int SkillLevel { get; set; } = 0;
    public bool isLearnSkill
    {
        get
        {
            return SkillLevel > 0;
        }
    }
    public int SkillDamage { get; set; } = 100;

    public SkillBase(Define.SkillType skillType)
    {
        SkillType = skillType;
    }

    public virtual void ActiveSkill()
    {
        
    }
    protected virtual void GenerateProjectile(int templateID, CreatureController owner, Vector3 startPos, Vector3 dir, Vector3 targetPos)
    {
        ProjectileController projectile = ObjectManager.instance.Spawn<ProjectileController>(startPos, templateID);

        projectile.SetInfo(templateID,owner,dir);
    }

    #region Destroy

    Coroutine coDestroy;

    public void StartDestroy(float delaySecond)
    {
        StopDestroy();
        coDestroy = StartCoroutine(CoDestroy(delaySecond));
    }

    public void StopDestroy()
    {
        if (coDestroy != null)
        {
            StopCoroutine(coDestroy);
            coDestroy = null;
        }
    }

    IEnumerator CoDestroy(float delaySecond)
    {
        yield return new WaitForSeconds(delaySecond);
        
        if (this.IsValid())
        {
            
            ObjectManager.instance.Despawn(this);

        }
    }
    #endregion
}
