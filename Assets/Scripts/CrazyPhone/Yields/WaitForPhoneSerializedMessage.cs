using CrazyPhone.Input;
using UnityEngine;

namespace CrazyPhone.Yields
{
    public class WaitForPhoneSerializedMessage : CustomYieldInstruction
    {
        private readonly string target;
        private string lastMessage;
        private bool success;

        public WaitForPhoneSerializedMessage(PhoneInput input, string target)
        {
            input.onSerializedMessage += OnMessage;
            this.target = target;
        }

        private void OnMessage(string msg)
        {
            lastMessage = msg;
            success = lastMessage.Equals(target);
        }

        public override bool keepWaiting => !success;
    }
}