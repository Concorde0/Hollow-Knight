using UnityEngine;
using System;
using System.Collections.Generic;


public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] private CollisionConfig collisionConfig;

    private Dictionary<CollisionDirection, bool> collisionStates = new Dictionary<CollisionDirection, bool>();


    public string CharacterName { get; private set; }

    public int Level { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public float AttackPower { get; private set; }
    public CharacterType CharacterType { get; private set; }
    
    public bool IsHit { get; private set; }
    public bool IsDead { get; private set; }
    public bool InVincibleDuration { get; private set; }
    public float VincibleTimer { get; private set; }


    protected virtual void Awake()
    {
        if (characterData != null)
        {
            LoadData(characterData);
        }
    }

    protected virtual void LoadData(CharacterData data)
    {
        CharacterName = data.characterName;
        CharacterType = data.characterType;
        MaxHealth = data.maxHealth;
        CurrentHealth = MaxHealth;
        MoveSpeed = data.moveSpeed;
        AttackPower = data.attackPower;
        VincibleTimer = data.vincibleTimer;
        IsHit = false;
        IsDead = false;
        InVincibleDuration = false;
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }


    public virtual void Move(Vector3 direction)
    {
        
    }

    protected abstract void Attack();
    protected abstract void Die();

    protected virtual void FixedUpdate()
    {
        UpdateCollisionStates();
    }

    private void UpdateCollisionStates()
    {
        if (collisionConfig == null) return;

        foreach (var check in collisionConfig.checks)
        {
            Vector2 worldOrigin = transform.TransformPoint(check.localOffset);
            Vector2 worldDir = transform.TransformDirection(check.localDirection).normalized;

            RaycastHit2D hit = Physics2D.Raycast(worldOrigin, worldDir, check.checkDistance, check.collisionMask);
            bool isHit = hit.collider != null;
            collisionStates[check.direction] = isHit;

            Debug.DrawLine(worldOrigin, worldOrigin + worldDir * check.checkDistance, isHit ? Color.red : Color.green);
            
        }

        
    }
    public bool IsColliding(CollisionDirection dir)
    {
        return collisionStates.ContainsKey(dir) && collisionStates[dir];
    }
}
