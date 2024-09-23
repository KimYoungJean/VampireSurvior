using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public List<SkillBase> Skills { get; } = new List<SkillBase>();

    public List<RepeatSkill> RepeatSkills { get; } = new List<RepeatSkill>();
    public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();

    public T AddSkill<T>(Vector3 position, Transform parent = null) where T : SkillBase
    {
        System.Type type = typeof(T);

        if (type == typeof(SwordController))
        {
            var sword = ObjectManager.instance.Spawn<SwordController>(position, Define.SwordSkillID);
            sword.transform.SetParent(parent);
            sword.ActiveSkill();

            Skills.Add(sword);
            RepeatSkills.Add(sword);

            return sword as T;
        }
        else if (type == typeof(FireballSkill))
        {
            /*            var fireball =  ObjectManager.instance.Spawn<FireballSkill>(position, Define.FireballSkillID);
             *            
            */
            var fireball = new GameObject().GetOrAddComponent<FireballSkill>();

            fireball.transform.SetParent(parent);

            fireball.ActiveSkill();

            Skills.Add(fireball);
            RepeatSkills.Add(fireball);

            return fireball as T;

        }
        else
        {

        }

        return null;
    }
}
