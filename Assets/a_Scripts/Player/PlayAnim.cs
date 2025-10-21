using Animancer;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

namespace HollowKnight.Anim
{
    public class PlayAnim : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private AnimationClip _Move;
        [SerializeField] private AnimationClip _Idle;
        [SerializeField] private AnimationClip _JumpUp;
        [SerializeField] private AnimationClip _JumpLoop;
        [SerializeField] private AnimationClip _JumpSoft;
        [SerializeField] private AnimationClip _JumpHard;

        private PlayerParam _param;

        [Inject]
        public void Construct(PlayerParam param)
        {
            _param = param;
        }

        private void Update()
        {
            if (animancer == null) return;
            MoveAnim();
        }

        private void MoveAnim()
        {
            animancer.Play(_param.inputDir.x != 0 ? _Move : _Idle);
        }

        
    }
}