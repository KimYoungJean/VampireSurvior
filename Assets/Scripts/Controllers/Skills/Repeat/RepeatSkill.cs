using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepeatSkill : SkillBase
{
    public float CoolTime { get; set; } = 1.0f;

    public RepeatSkill() : base(Define.SkillType.Repeat)
    {

    }

    #region 반복 스킬 구현
    Coroutine _coSkill;

    public override void ActiveSkill()
    {
        if (_coSkill != null)
        {
            StopCoroutine(_coSkill);
        }

        _coSkill = StartCoroutine(CoSkill());
    }

    protected abstract void DoSkillJob();
    
    protected virtual IEnumerator CoSkill()
    {
        WaitForSeconds coolTime = new WaitForSeconds(CoolTime);

        while (true)
        {
            DoSkillJob();

            yield return coolTime;

        }
    }

    #endregion


}
