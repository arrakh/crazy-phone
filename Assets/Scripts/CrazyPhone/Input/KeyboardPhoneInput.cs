using UnityEngine;

namespace CrazyPhone.Input
{
    public partial class PhoneInput
    {
        private void Update()
        {
            if (isConnected) return;

            DetectSpacebar();

            foreach (var (number, numString) in PhoneMappings.KeycodeMappings)
            {
                if (UnityEngine.Input.GetKeyDown(number)) ProcessDownKey(numString);

                if (!UnityEngine.Input.GetKeyUp(number)) continue;
                if (!currentlyPressed.Contains(numString)) continue;
                currentlyPressed.Remove(numString);
                releasedThisFrame.Add(numString);
            }
        }

        private void DetectSpacebar()
        {
            isClosed = UnityEngine.Input.GetKey(KeyCode.Space);
        }

        private void ProcessDownKey(string numString)
        {
            currentlyPressed.Add(numString);
            thisFrame.Add(numString);
            onKeyDown?.Invoke(numString);
        }
    }
}