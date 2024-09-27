using DG.Tweening;
using UnityEngine;

namespace TenSecondsReplay.Utilities
{
    public class ShakePositionAnimation : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool playOnStart = true;
        [SerializeField] private Vector3 shakeStrength;
        [SerializeField] private float duration;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private Ease ease;
        [SerializeField] private int loopCount;
        [SerializeField] private LoopType loopType;

        [Header("Options")]
        [SerializeField] private bool randomizeDuration;
        [SerializeField] private float randomDurationRange;
        
        private Tween tween;
        
        private void Start()
        {
            if (playOnStart) StartAnimation();
        }

        public void StartAnimation()
        {
            StopAnimation();
            
            var dur = randomizeDuration
                ? Random.Range(duration - randomDurationRange, duration + randomDurationRange)
                : duration;
            
            tween = target.DOShakePosition(dur, shakeStrength, vibrato)
                .SetEase(ease)
                .SetLoops(loopCount, loopType);
        }

        public void StopAnimation()
        {
            if (tween != null) tween.Kill();
        }
    }
}