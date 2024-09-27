using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrazyPhone.Utilities
{
    public class ScaleAnimation : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool playOnStart = true;
        [SerializeField] private Vector3 from, to;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private int loopCount;
        [SerializeField] private LoopType loopType;

        [Header("Options")]
        [SerializeField] private bool randomizeDuration;
        [SerializeField] private float randomDurationRange;
        
        private Tween tween;
        public float Duration => duration;
        
        private void Start()
        {
            if (playOnStart) StartAnimation();
        }

        public void StartAnimation()
        {
            StopAnimation();

            target.transform.localScale = from;
            
            var dur = randomizeDuration
                ? Random.Range(duration - randomDurationRange, duration + randomDurationRange)
                : duration;
            
            tween = target.DOScale(to, dur)
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