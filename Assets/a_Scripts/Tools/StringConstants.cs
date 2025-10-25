namespace HollowKnight.Tools
{
    public class StringConstants : Singleton<StringConstants>
    {
        public struct StateName
        {
            
        }
        public struct AnimName
        {
            public const string Idle = "Idle";
            public const string Move = "Move";
            public const string Jump = "Jump";
            public const string JumpSoft = "JumpSoft";
            public const string JumpHard = "JumpHard";
            public const string JumpUp = "JumpUp";
            public const string JumpLoop = "JumpLoop";
        }
        
        public struct TimerName
        {
            public const string JumpLoadTime = "JumpLoadTime";
            public const string JumpUpTime = "JumpUpTime";
            public const string JumpSoftTime = "JumpSoftTime";
        }
    }
    
}