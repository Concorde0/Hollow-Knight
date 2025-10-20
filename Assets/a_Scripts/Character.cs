using UnityEngine;
using System.Collections.Generic;
using Zenject;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CollisionConfig collisionConfig;
    [Inject] private CharacterData _data;

    private Dictionary<CollisionDirection, bool> collisionStates = new();

    #region Data
    public string CharacterName { get; protected set; }
    public int Level { get; protected set; }
    public float MaxHealth { get; protected set; }
    public float CurrentHealth { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public float AttackPower { get; protected set; }
    public CharacterType CharacterType { get; protected set; }

    public bool IsHit { get; protected set; }
    public bool IsDead { get; protected set; }
    public bool InVincibleDuration { get; protected set; }
    public float VincibleTimer { get; protected set; }
    #endregion

    public virtual void Init(CharacterData data)
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
    
    public virtual void Awake()
    {
        Init(_data);
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public virtual void Move(Vector3 direction) { }

    protected abstract void Attack();
    protected abstract void Die();

    protected virtual void FixedUpdate() => UpdateCollisionStates();

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

    protected bool IsColliding(CollisionDirection dir) =>
        collisionStates.ContainsKey(dir) && collisionStates[dir];
}
