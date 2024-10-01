using System;
using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyPhone
{
    public class IntroSceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;
        
        [Header("Number")]
        [SerializeField] private string phoneNumber = "915";
        [SerializeField] private AudioClip wrongClip;

        [SerializeField] private AudioClip holdTone;
        [SerializeField] private float holdDuration = 9f;

        [Header("Welcome")]
        [SerializeField] private AudioClip introClip;
        [SerializeField] private string firstNumber;
        
        [Header("Birth Date")]
        [SerializeField] private AudioClip birthDateClip;

        [Header("Name")]
        [SerializeField] private AudioClip nameClip;

        [Header("Email")]
        [SerializeField] private AudioClip emailClip;
        private PhoneLetterBuilder letterBuilder;
        [SerializeField] private AudioClip afterEmailClip;
        [SerializeField] private AudioClip pleaseHoldClip;
        [SerializeField] private AudioClip holdMusic;
        [SerializeField] private float holdMusicDuration;
        [SerializeField] private AudioClip thankForProvideClip;

        [Header("Press 5 After Pressing 7")] 
        [SerializeField] private AudioClip fiveAfterSevenClip;
        [SerializeField] private AudioClip noticeClip;
        [SerializeField] private AudioClip confirmationClip;

        private IEnumerator Start()
        {
            letterBuilder = new PhoneLetterBuilder(input, 0.4f);
            letterBuilder.SetEnable(false);
            
            yield return new WaitForPhoneNumber(input, phoneNumber, OnUpdateState);

            audioSource.clip = holdTone;
            audioSource.Play();
            yield return new WaitForSeconds(holdDuration);
            audioSource.Stop();
            
            //"You have reached Óbidos Hospital. If you're calling to schedule an appointment, please press .1"
            audioSource.PlayOneShot(introClip);
            yield return new WaitForSeconds(introClip.length + 1f);
            yield return new WaitForPhoneInput(input, firstNumber);

            //"For authentication purposes, please type in your birth year followed by the star symbol."
            audioSource.PlayOneShot(birthDateClip);
            yield return new WaitForSeconds(birthDateClip.length + 1f);
            yield return new WaitForPhoneInput(input, "*");

            //"For additional security purposes, please type your name using the letters on your telephone keypad, followed by the star symbol."
            audioSource.PlayOneShot(nameClip);
            letterBuilder.SetEnable(true);
            yield return new WaitForSeconds(nameClip.length + 1f);
            yield return new WaitForPhoneInput(input, "*");
            PlayerInfo.name = letterBuilder.CurrentText;
            yield return new WaitForSeconds(1f);
            var nameString = "Hello, " + PlayerInfo.name;
            TextToSpeech.Start(nameString);
            yield return new WaitForSeconds(2f + nameString.Length * 0.2f);
            
            letterBuilder.Clear();
            letterBuilder.SetEnable(false);

            //To assist in directing your call, please type your email adress using the letters on your keypad, followed by the star symbol.
            audioSource.PlayOneShot(emailClip);
            yield return new WaitForSeconds(emailClip.length + 1f);

            //Enter email
            letterBuilder.SetEnable(true);
            yield return new WaitForPhoneInput(input, "*");
            PlayerInfo.email = letterBuilder.CurrentText;
            
            audioSource.PlayOneShot(afterEmailClip);
            yield return new WaitForSeconds(afterEmailClip.length + 1f);
            var email = $"{letterBuilder.CurrentText}@ email dot com";
            TextToSpeech.Start(email);
            yield return new WaitForSeconds(2f + email.Length * 0.2f);
            letterBuilder.Clear();
            letterBuilder.SetEnable(false);

            //Please hold
            audioSource.PlayOneShot(pleaseHoldClip);
            yield return new WaitForSeconds(pleaseHoldClip.length);
            
            //Hold music
            audioSource.clip = holdMusic;
            audioSource.Play();
            yield return new WaitForSeconds(holdMusicDuration);
            audioSource.Stop();
            
            //Thank you for providing your personal information, we have located your position.
            audioSource.PlayOneShot(thankForProvideClip);
            yield return new WaitForSeconds(thankForProvideClip.length + 1f);
            
            //To confirm that our camera surveillance is working properly, please press 5... after pressing 7.
            audioSource.PlayOneShot(fiveAfterSevenClip);
            yield return new WaitForSeconds(fiveAfterSevenClip.length);
            yield return new WaitForPhoneNumber(input, "75", OnUpdateState);
            
            //We have noticed you are using a computer. 
            audioSource.PlayOneShot(noticeClip);
            yield return new WaitForSeconds(noticeClip.length + 1f);
            
            //Your confirmation number is required to continue.
            audioSource.PlayOneShot(confirmationClip);
            yield return new WaitForSeconds(confirmationClip.length + 1f);
            
            
            SceneManager.LoadScene("FormSequence 1");
        }

        private void Update()
        {
            letterBuilder.Update(Time.deltaTime);
        }

        private void OnUpdateState(WaitForPhoneNumber ph, bool wasCorrect)
        {
            if (!wasCorrect)
            {
                audioSource.PlayOneShot(wrongClip);
                ph.Clear();
            }
        }
    }
}