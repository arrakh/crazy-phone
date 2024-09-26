using UnityEngine;

namespace CrazyPhone.Input
{
    public class WaitForPhoneNumber : CustomYieldInstruction
    {
        private PhoneInput phoneInput;
        private string target;
        private int numberIndex = 0;

        public WaitForPhoneNumber(PhoneInput phoneInput, string target)
        {
            this.phoneInput = phoneInput;
            this.target = target;
            phoneInput.onKeyDown += OnKeyDown;
        }

        private void OnKeyDown(string key)
        {
            string targetKey = target[NumberIndex].ToString();
            if (key.Equals(targetKey)) numberIndex = NumberIndex + 1;
            else numberIndex = 0;
        }

        public override bool keepWaiting => CheckForWaiting();

        public int NumberIndex => numberIndex;

        public string CurrentProgress => target.Substring(0, numberIndex);

        private bool CheckForWaiting()
        {
            return NumberIndex < target.Length;
        }
    }
}