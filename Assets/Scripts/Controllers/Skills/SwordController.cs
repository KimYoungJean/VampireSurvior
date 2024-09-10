using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : SkillController
{

    [SerializeField]
    Animator[] animators;

    protected enum AttackDir
    {
        Front,
        Back,
        Up,
        Down
    }

    public override bool Init()
    {
        base.Init();

        for(int i = 0; i < animators.Length; i++)
        {
            animators[i].gameObject.SetActive(false);            
        }
        for(int i = 0; i < animators.Length; i++)
        {
            animators[i].gameObject.GetOrAddComponent<SwordChild>().SetInfo(ObjectManager.instance.Player, 100);
        }
        return true;
    }

    public void ActiveSkill()
    {
        StartCoroutine(CoSword());
    }

    float CooldownTime = 2.0f;
    
    IEnumerator CoSword()
    {
        while (true)
        {
            yield return new WaitForSeconds(CooldownTime);

            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].gameObject.SetActive(true);
                animators[i].SetTrigger("Attack");
            }
            yield return null;
        }
    }
}
