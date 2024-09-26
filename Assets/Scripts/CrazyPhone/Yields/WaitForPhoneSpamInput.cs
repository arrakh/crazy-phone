using CrazyPhone.Input;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CrazyPhone.Yields
{
    public class WaitForPhoneSpamInput : CustomYieldInstruction
    {
        private PhoneInput phoneInput;
        private string[] keys;
        private int maxCount, currentCount;

        public int MaxCount => maxCount;
        public int CurrentCount => currentCount;
        public float NormalizedAlpha => (float) currentCount / maxCount;
        
        public WaitForPhoneSpamInput(PhoneInput phoneInput, int count, params string[] keys)
        {
            this.phoneInput = phoneInput;
            this.keys = keys;
            maxCount = count;
            currentCount = 0;
        }

        public override bool keepWaiting => CheckUpdate();

        private bool CheckUpdate()
        {
            foreach (var key in keys)
                if (phoneInput.GetKeyDown(key))
                {
                    Debug.Log($"ADDED, {currentCount}");
                    currentCount++;
                }
            
            return currentCount < maxCount;
        }
    }
}