using CrazyPhone.Input;
using UnityEngine;

namespace CrazyPhone.Yields
{
    public class WaitForAnyPhoneInput : CustomYieldInstruction
    {
        private string input;

        public WaitForAnyPhoneInput(PhoneInput phoneInput)
        {
            phoneInput.onKeyDown += OnKeyDown;
        }

        private void OnKeyDown(string key)
        {
            input = key;
        }

        public string Input => input;

        public override bool keepWaiting => string.IsNullOrEmpty(input);
    }
}