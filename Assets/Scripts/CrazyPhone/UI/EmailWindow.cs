using System;
using CrazyPhone.Input;
using TMPro;
using UnityEngine;

namespace CrazyPhone.UI
{
    public class EmailWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI emailText;
        [SerializeField] private PhoneInput input;

        private PhoneLetterBuilder builder;

        private void Awake()
        {
            builder = new PhoneLetterBuilder(input);
        }

        private void Update()
        {
            builder.Update(Time.deltaTime);
            string letter = (builder.CurrentLetter == ' ' ? "" : builder.CurrentLetter.ToString());
            emailText.text = $"{builder.CurrentText}<alpha=#AA>{letter}<alpha=#FF>@email.com";
        }
    }
}