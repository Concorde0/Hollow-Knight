using System;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace a_Scripts
{
    [Serializable]
    public class AnimParam
    {
        public enum Type
        {
            Single,
            Group,
            InfoGroup,
        }

        public string name = "Anim";
        public Type type = Type.Single;
        public float enterTime;

        public ClipTransition clip;
        public ClipTransition[] clipGroup;
        public AnimInfo[] infoGroup;
    }

    [Serializable]
    public class AnimInfo
    {
        public ClipTransition clip;
        public float enterTime;
    }

    [CreateAssetMenu(fileName = "New Animancer Setting", menuName = "Game/Animation/Animancer Setting")]
    public class AnimancerSetting : ScriptableObject
    {
        public List<AnimParam> animParams;

        public AnimParam GetParam(string name)
        {
            return animParams.Find(p => p.name == name);
        }
    }
}