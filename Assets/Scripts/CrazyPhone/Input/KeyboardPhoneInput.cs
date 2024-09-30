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
                if (UnityEngine.Input.GetKeyUp(number)) ProcessUpKey(numString);
            }
        }

        private void DetectSpacebar()
        {
            isClosed = UnityEngine.Input.GetKey(KeyCode.Space);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) ProcessDownKey(CLOSE);
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space)) ProcessUpKey(OPEN);
        }

        private void ProcessDownKey(string numString)
        {
            currentlyPressed.Add(numString);
            thisFrame.Add(numString);
            onKeyDown?.Invoke(numString);
        }

        private void ProcessUpKey(string numString)
        {
            if (!currentlyPressed.Contains(numString)) return;
            currentlyPressed.Remove(numString);
            releasedThisFrame.Add(numString);
        }
    }
}