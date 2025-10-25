
using UnityEngine;

namespace HollowKnight.Param
{
    public class PlayerParam
    {
        public Vector2 inputDir { get; set; }
        public int faceDir { get; set; }
        
        public bool jumpHeld { get; set; }
        
        public bool jumpDown { get; set; }

        public bool canJump { get; set; } = false;
    }
}

