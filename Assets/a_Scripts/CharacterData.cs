using UnityEngine;


public enum CharacterType
{
    Player,Npc,Enemy
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("基础属性")]
    public string characterName;
    public string characterID;
    public CharacterType characterType;
    public float maxHealth;
    public float moveSpeed;

    [Header("战斗属性")]
    public float attackPower;
    
    [Header("跳跃参数")]
    public bool SnapInput = true;
    public float VerticalDeadZoneThreshold = 0.3f;
    public float HorizontalDeadZoneThreshold = 0.1f;
    public float GroundingForce = -1.5f;
    public float FallAcceleration = 110;
    public float JumpEndEarlyGravityModifier = 3;
    public float CoyoteTime = 0.15f;
    public float JumpBuffer =0.2f;
    public float MaxFallSpeed = 40;
    public float JumpPower = 36;
    public float GroundDeceleration = 60;
    public float MaxSpeed = 14;
    public float AirDeceleration = 30;
    public float Acceleration = 120;

    
    

    [Header("其他")] 
    public float vincibleTimer;
}