using System;
using UnityEngine;

namespace CrazyPhone.Input
{
    public class PhoneLetterBuilder
    {
        private PhoneInput phoneInput;
        private string currentText;
        private char currentLetter;
        private string lastKey;
        private int currentCount;
        private float cooldown, currentCooldown;
        private bool shouldRecordOnUpdate = false;

        public string CurrentText => currentText;
        public char CurrentLetter => currentLetter;
        
        public PhoneLetterBuilder(PhoneInput phoneInput, float cooldown = 0.5f)
        {
            currentText = lastKey = String.Empty;
            this.phoneInput = phoneInput;
            this.cooldown = cooldown;

            phoneInput.onKeyDown += OnKeyDown;
        }

        private void OnKeyDown(string key)
        {
            if (key.Equals("*"))
            {
                Delete();
                return;
            }
            
            currentCooldown = cooldown;

            bool isSameKey = key.Equals(lastKey);
            Debug.Log($"KEY IS {key}, LAST IS {lastKey}, IS SAME {isSameKey}, COOLDOWN IS {currentCooldown}");

            if (isSameKey) currentCount++;
            else
            {
                lastKey = key;
                currentCount = 0;
            }

            currentLetter = PhoneMappings.Get(lastKey, currentCount);

            //if (!isSameKey && !isFirstTime) return;
            shouldRecordOnUpdate = true;
        }

        public void Clear() => currentText = String.Empty;

        public void Delete()
        {
            if (string.IsNullOrEmpty(currentText)) return;
            currentText = currentText.Remove(currentText.Length - 1, 1);
        }

        public void Update(float deltaTime)
        {
            currentCooldown -= deltaTime;
            if (currentCooldown > 0f) return;

            currentCooldown = cooldown;
            if (!shouldRecordOnUpdate) return;
            shouldRecordOnUpdate = false;
            RecordKey();
        }

        private void RecordKey()
        {
            Debug.Log("RECORDING");
            currentCooldown = cooldown;
            currentCount = -1;
            currentText += currentLetter;
            currentLetter = ' ';
        }
    }
}