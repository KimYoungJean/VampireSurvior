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
    public enum ObjectType
    {
        Player,
        Monster,
        Projectile,
        Environment,
    }
}
