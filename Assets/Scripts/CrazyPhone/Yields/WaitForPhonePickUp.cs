using CrazyPhone.Input;
using UnityEngine;

namespace CrazyPhone.Yields
{
    public class WaitForPhonePickUp : CustomYieldInstruction
    {
        private PhoneInput input;

        public WaitForPhonePickUp(PhoneInput input)
        {
            this.input = input;
        }

        public override bool keepWaiting => input.IsClosed;
    }
}