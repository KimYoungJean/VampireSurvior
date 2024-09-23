using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : RepeatSkill
{
    public FireballSkill()
    {
    }

    protected override void DoSkillJob()
    {
        if (GameManager.Instance.Player.IsValid() == false)
            return;        

        Vector3 spawnPos = GameManager.Instance.Player.GetOrAddComponent<PlayerController>().FireSocket;
        Vector3 dir = GameManager.Instance.Player.GetOrAddComponent<PlayerController>().ShootDir;

     
        GenerateProjectile(20, Owner, spawnPos, dir, Vector3.zero);

    }
}
