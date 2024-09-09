using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//
public class SkillController : BaseController
{
    public Define.SkillType SkillType { get;  set; }
    public Data.SkillData SkillData { get; protected set; }

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
