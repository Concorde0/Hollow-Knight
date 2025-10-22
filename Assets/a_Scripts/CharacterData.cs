using UnityEngine;

public enum CharacterType
{
    Player, 
    Npc,  
    Enemy   
}


[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("基础属性")]
    public string characterName;         // 角色名称（显示用）
    public string characterID;           // 角色唯一标识（用于存档、识别）
    public CharacterType characterType;  // 角色类型（玩家 / NPC / 敌人）
    public float maxHealth;              // 最大生命值
    public float moveSpeed;              // 基础移动速度（可用于非物理移动）

    [Header("战斗属性")]
    public float attackPower;            // 攻击力

    [Header("跳跃参数")]
    public bool SnapInput = true;                    // 是否吸附输入方向（用于方向归一化）
    public float VerticalDeadZoneThreshold = 0.3f;   // 垂直死区阈值（小于该值视为无输入）
    public float HorizontalDeadZoneThreshold = 0.1f; // 水平死区阈值
    public float GroundingForce = -1.5f;             // 地面吸附力（贴地时向下施加的力）
    public float FallAcceleration = 110f;            // 下落加速度（自由落体加速）
    public float JumpEndEarlyGravityModifier = 3f;   // 提前松开跳跃键时的重力倍率（快速下坠）
    public float CoyoteTime = 0.15f;                 // 土狼时间（离地后允许短时间跳跃）
    public float JumpBuffer = 0.2f;                  // 跳跃缓冲时间（提前按跳跃键后延迟触发）
    public float MaxFallSpeed = 40f;                 // 最大下落速度（防止无限加速）
    public float JumpPower = 36f;                    // 跳跃力度（向上施加的初始力）
    public float GroundDeceleration = 60f;           // 地面减速率（松开方向键时减速）
    public float MaxSpeed = 14f;                     // 最大水平移动速度
    public float AirDeceleration = 30f;              // 空中减速率（空中控制灵敏度）
    public float Acceleration = 120f;                // 加速度（起步或变向时的加速）

    [Header("其他")]
    public float vincibleTimer;                      // 受伤后无敌时间（单位：秒）
}