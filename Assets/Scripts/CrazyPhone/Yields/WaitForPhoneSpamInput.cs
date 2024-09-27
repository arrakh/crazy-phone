using System;
using System.Collections.Generic;
using CrazyPhone.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CrazyPhone.Yields
{
    public class WaitForPhoneSpamInput : CustomYieldInstruction
    {
        private HashSet<string> keys;
        private int maxCount, currentCount;

        public int MaxCount => maxCount;
        public int CurrentCount => currentCount;
        public float NormalizedAlpha => (float) currentCount / maxCount;

        private Action<WaitForPhoneSpamInput, bool> onUpdateState;

        public WaitForPhoneSpamInput(PhoneInput phoneInput, int count, params string[] keys)
        {
            this.keys = keys.ToHashSet();
            maxCount = count;
            currentCount = 0;

            phoneInput.onKeyDown += OnKeyDown;
        }

        public WaitForPhoneSpamInput(PhoneInput phoneInput, int count, Action<WaitForPhoneSpamInput, bool> onUpdateState, params string[] keys)
        {
            this.keys = keys.ToHashSet();
            this.onUpdateState = onUpdateState;
            maxCount = count;
            currentCount = 0;
            
            phoneInput.onKeyDown += OnKeyDown;
        }

        public override bool keepWaiting => currentCount < maxCount;
        
        private void OnKeyDown(string msg)
        {
            bool correct = keys.Contains(msg);
            if (correct) currentCount++;
            
            onUpdateState?.Invoke(this, correct);
        }
    }
}