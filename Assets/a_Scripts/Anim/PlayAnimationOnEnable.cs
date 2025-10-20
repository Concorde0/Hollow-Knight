using Animancer;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

namespace HollowKnight.Anim
{
    public class PlayAnimationOnEnable : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private AnimationClip _Move;
        [SerializeField] private AnimationClip _Idle;

        private PlayerParam _param;

        [Inject]
        public void Construct(PlayerParam param)
        {
            _param = param;
        }

        private void Update()
        {
            if (animancer == null) return;
            animancer.Play(_param.inputDir.x != 0 ? _Move : _Idle);
        }
    }
}