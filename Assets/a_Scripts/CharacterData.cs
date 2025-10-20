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

    [Header("其他")] 
    public float vincibleTimer;
}