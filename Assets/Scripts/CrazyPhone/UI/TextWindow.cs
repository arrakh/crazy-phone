using CrazyPhone.Input;
using TMPro;
using UnityEngine;

namespace CrazyPhone.UI
{
    public class TextWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private PhoneInput input;

        private PhoneLetterBuilder builder;
        private string target;

        public bool IsCompleted => builder.CurrentText.Equals(target);

        private bool initialized = false;

        public void Initialize(string targetText, float cooldown = 0.5f, bool isWarped = false)
        {
            target = targetText;
            builder = new PhoneLetterBuilder(input, cooldown, isWarped);
            initialized = true;
        }

        private void Update()
        {
            if (!initialized) return;
            
            builder.Update(Time.deltaTime);
            string letter = (builder.CurrentLetter == ' ' ? "" : builder.CurrentLetter.ToString());
            inputText.text = $"{builder.CurrentText}<alpha=#AA>{letter}";
        }
    }
}