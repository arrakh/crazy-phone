using System;
using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using TenSecondsReplay.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyPhone
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Slider slider;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private ShakePositionAnimation snailShake;

        private WaitForPhoneNumber phone;

        private WaitForPhoneSpamInput spam;
        private PhoneLetterBuilder builder;

        private IEnumerator Start()
        {
            input.onKeyDown += OnKeyDown;
            /*Debug.Log("START");
            input.SendSerialMessage("ringA");
            yield return new WaitForPhonePickUp(input);
            Debug.Log("PICKED UP, put it down...");
            yield return new WaitForPhoneHangUp(input);
            Debug.Log("NICEEEEE");*/
            
            spam = new WaitForPhoneSpamInput(input, 80, "1", "3", "9");
            yield return spam.PlayAudioParallel(audioSource, audioClip, 3f);
            Debug.Log("DONE!");
        }

        private void OnKeyDown(string msg)
        {
            if (msg.Equals("1")) snailShake.StartAnimation();
        }

        /*private void Start()
        {
            builder = new PhoneLetterBuilder(input);
            //TextToSpeech.Start("Hello arya, your email is arrakhpra@email.com");
        }*/

        private void Update()
        {
            slider.value = spam.NormalizedAlpha;
            //text.text = phone.CurrentProgress;
            //builder.Update(Time.deltaTime);
            //text.text = $"{builder.CurrentText}<alpha=#44>{builder.CurrentLetter.ToString()}";
        }
    }
}