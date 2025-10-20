using UnityEngine;

public class Player : Character
{
    protected override void Update()
    {
        base.Update();
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (IsColliding(CollisionDirection.Ground))
        {
            Debug.Log("玩家站在地面上");
        }

        if (IsColliding(CollisionDirection.Left))
        {
            Debug.Log("左边有墙");
        }
        
    }

    protected override void Attack()
    {
        
    }

    protected override void Die()
    {
        
    }
}
