using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        DevScene,
        GameScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Die,
    }
    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Death,
    }
    public enum ObjectType
    {
        Player,
        Monster,
        Projectile,
        Interactable,
    }

    public enum SkillType
    {
        None,
        Sequence,//돌진 공격
        Repeat,//반복공격
        Etc,
        Projectile,
        Melee,

    }

    public enum StageType
    {
        Normal,
        Boss
    }
    

    public const int SwordSkillID = 10;
    public const int BOSS01_ID = 1001;
}
