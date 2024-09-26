using UnityEngine;

namespace CrazyPhone.Input
{
    public partial class PhoneInput
    {
        private void Update()
        {
            if (isConnected) return;
            foreach (var (number, numString) in PhoneMappings.KeycodeMappings)
            {
                if (UnityEngine.Input.GetKeyDown(number))
                {
                    currentlyPressed.Add(numString);
                    thisFrame.Add(numString);
                    onKeyDown?.Invoke(numString);
                }

                if (!UnityEngine.Input.GetKeyUp(number)) continue;
                if (!currentlyPressed.Contains(numString)) continue;
                currentlyPressed.Remove(numString);
                releasedThisFrame.Add(numString);
            }
        }
    }
}