using DG.Tweening;
using UnityEngine;

namespace CrazyPhone.Utilities
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool playOnStart = true;
        [SerializeField] private Vector3 from, to;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private int loopCount;
        [SerializeField] private LoopType loopType;
        [SerializeField] private bool preserveOriginalRotation = true;
        
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

            Vector3 fromFinal = from;
            Vector3 toFinal = to;

            if (preserveOriginalRotation)
            {
                var angle = target.transform.eulerAngles;
                fromFinal = angle + from;
                toFinal = angle + to;
            }

            target.transform.eulerAngles = fromFinal;
            
            var dur = randomizeDuration
                ? Random.Range(duration - randomDurationRange, duration + randomDurationRange)
                : duration;
            
            tween = target.DORotate(toFinal, dur)
                .SetEase(ease)
                .SetLoops(loopCount, loopType);
        }

        public void StopAnimation()
        {
            if (tween != null) tween.Kill();
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!target) target = transform;
        }
#endif
    }
}