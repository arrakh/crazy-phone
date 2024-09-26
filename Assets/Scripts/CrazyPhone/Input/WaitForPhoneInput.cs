using UnityEngine;

namespace CrazyPhone.Input
{
    public class WaitForPhoneInput : CustomYieldInstruction
    {
        private PhoneInput phoneInput;
        private string key;
        
        public WaitForPhoneInput(PhoneInput phoneInput, string key)
        {
            this.phoneInput = phoneInput;
            this.key = key;
        }

        public override bool keepWaiting => !phoneInput.GetKeyDown(key);
    }
}