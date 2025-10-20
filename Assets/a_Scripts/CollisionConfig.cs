using UnityEngine;

public enum CollisionDirection
{
    Ground,
    Ceiling,
    Left,
    Right,
    Up,
    Down
}

[System.Serializable]
public class CollisionCheck
{
    public CollisionDirection direction;
    public Vector3 localOffset;      // 射线起点相对角色的位置
    public Vector3 localDirection;   // 射线方向
    public float checkDistance = 0.2f;
    public LayerMask collisionMask;  // 检测层
}

[CreateAssetMenu(fileName = "CollisionConfig", menuName = "Game/Collision Config")]
public class CollisionConfig : ScriptableObject
{
    public CollisionCheck[] checks;
}