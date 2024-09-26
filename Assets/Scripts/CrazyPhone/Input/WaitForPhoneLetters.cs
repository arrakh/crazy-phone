using UnityEngine;

namespace CrazyPhone.Input
{
    public class WaitForPhoneLetters : CustomYieldInstruction
    {
        public override bool keepWaiting { get; }
    }
}