using System;
using System.Collections;
using CrazyPhone.Input;
using TMPro;
using UnityEngine;

namespace CrazyPhone
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private TextMeshProUGUI text;

        private WaitForPhoneNumber phone;
        private PhoneLetterBuilder builder;

        /*private IEnumerator Start()
        {
            phone = new (input, "2468");
            
            Debug.Log("START BY PRESSING 3");
            yield return new WaitForPhoneInput(input, "3");
z
            Debug.Log("PRESS 2468");
            yield return phone;
            
            Debug.Log("NICEEE");
        }*/

        private void Start()
        {
            builder = new PhoneLetterBuilder(input);
        }

        private void Update()
        {
            //text.text = phone.CurrentProgress;
            builder.Update(Time.deltaTime);
            text.text = $"{builder.CurrentText}<alpha=#44>{builder.CurrentLetter.ToString()}";
        }
    }
}