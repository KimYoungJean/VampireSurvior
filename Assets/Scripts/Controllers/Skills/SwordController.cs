using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : RepeatSkill
{



    [SerializeField]
    List<GameObject> swordChilds = new List<GameObject>();

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
        
        
        for (int i = 0; i < transform.childCount; i++)
        {
            swordChilds.Add(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < swordChilds.Count; i++)
        { 
            swordChilds[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < swordChilds.Count; i++)
        {
            swordChilds[i].gameObject.GetOrAddComponent<SwordChild>().SetInfo(ObjectManager.instance.Player, 100);
        }

        return true;
    }

    public override void ActiveSkill()
    {
        StartCoroutine(CoSword());
    }

    float CooldownTime = 2.0f;

    IEnumerator CoSword()
    {
        while (true)
        {
            yield return new WaitForSeconds(CooldownTime);



            for (int i = 0; i < swordChilds.Count; i++)
            {
                swordChilds[i].gameObject.SetActive(true);
                swordChilds[i].GetComponent<Animator>().SetTrigger("IsAttack");
            }
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < swordChilds.Count; i++)
            {
                swordChilds[i].gameObject.SetActive(false);
            }
        }
    }

    protected override void DoSkillJob()
    {
        throw new System.NotImplementedException();
    }
}
