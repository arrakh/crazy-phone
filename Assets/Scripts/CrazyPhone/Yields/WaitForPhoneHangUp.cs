using CrazyPhone.Input;
using UnityEngine;

namespace CrazyPhone.Yields
{
    public class WaitForPhoneHangUp : CustomYieldInstruction
    {
        private PhoneInput input;

        public WaitForPhoneHangUp(PhoneInput input)
        {
            this.input = input;
        }

        public override bool keepWaiting => !input.IsClosed;
    }
}