using System;
using UnityEngine;

namespace CrazyPhone.Input
{
    public class WaitForPhoneNumber : CustomYieldInstruction
    {
        private PhoneInput phoneInput;
        private string target;
        private int numberIndex = 0;
        private Action<WaitForPhoneNumber, bool> onUpdateState;

        public WaitForPhoneNumber(PhoneInput phoneInput, string target, Action<WaitForPhoneNumber, bool> onUpdateState = null)
        {
            this.phoneInput = phoneInput;
            this.target = target;
            this.onUpdateState = onUpdateState;
            phoneInput.onKeyDown += OnKeyDown;
        }

        private void OnKeyDown(string key)
        {
            if (!CheckForWaiting()) return;
            string targetKey = target[NumberIndex].ToString();
            bool isCorrect = key.Equals(targetKey);
            if (isCorrect) numberIndex = NumberIndex + 1;
            else numberIndex = 0;
            
            onUpdateState?.Invoke(this, isCorrect);
        }

        public override bool keepWaiting => CheckForWaiting();

        public int NumberIndex => numberIndex;

        public string CurrentProgress => target.Substring(0, numberIndex);

        public void Clear() => target = string.Empty;

        private bool CheckForWaiting()
        {
            return NumberIndex < target.Length;
        }
    }
}