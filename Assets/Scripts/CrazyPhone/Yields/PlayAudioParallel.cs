using UnityEngine;

namespace CrazyPhone.Yields
{
    public static class PlayAudioParallelExtension
    {
        public static CustomYieldInstruction PlayAudioParallel(this CustomYieldInstruction wrappedInstruction,
            AudioSource audioSource, AudioClip audioClip, float delay) =>
            new PlayAudioParallel(wrappedInstruction, audioSource, audioClip, delay);
    }
    
    public class PlayAudioParallel : CustomYieldInstruction
    {
        private CustomYieldInstruction wrappedInstruction;
        private float delay;
        private float currentTimer = 0;
        private AudioSource audioSource;
        private AudioClip audioClip;

        public PlayAudioParallel(CustomYieldInstruction wrappedInstruction, AudioSource audioSource, AudioClip audioClip, float delay)
        {
            this.wrappedInstruction = wrappedInstruction;
            this.delay = delay + audioClip.length;
            this.audioSource = audioSource;
            audioSource.clip = audioClip;
        }

        public override bool keepWaiting => CheckShouldKeepWaiting();

        private bool CheckShouldKeepWaiting()
        {
            if (!wrappedInstruction.keepWaiting)
            {
                audioSource.Stop();
                return false;
            }
            
            currentTimer += Time.deltaTime;
            if (currentTimer < delay) return true;
            currentTimer = 0f;
            
            if (!audioSource.isPlaying) audioSource.Play();

            return true;
        }
    }
}