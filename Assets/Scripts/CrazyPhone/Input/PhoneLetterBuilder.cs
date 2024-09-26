using System;
using UnityEngine;

namespace CrazyPhone.Input
{
    public class PhoneLetterBuilder : IDisposable
    {
        private PhoneInput phoneInput;
        private string currentText;
        private char currentLetter;
        private string lastKey;
        private int currentCount;
        private float cooldown, currentCooldown;
        private bool shouldRecordOnUpdate = false;
        private bool disposed = false;
        private bool enabled = true;

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
            if (!enabled) return;

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
            if (currentLetter.Equals(' ')) return;

            //if (!isSameKey && !isFirstTime) return;
            shouldRecordOnUpdate = true;
        }

        public void Clear() => currentText = String.Empty;

        public void SetEnable(bool enable) => enabled = enable;

        public void Delete()
        {
            if (!enabled) return;

            if (string.IsNullOrEmpty(currentText)) return;
            currentText = currentText.Remove(currentText.Length - 1, 1);
        }

        public void Update(float deltaTime)
        {
            if (!enabled) return;
            if (disposed) return;
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

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
        }
    }
}