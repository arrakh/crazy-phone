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

        [Header("First Sequence")]
        [SerializeField] private AudioClip introClip;
        [SerializeField] private string firstNumber;

        [Header("Second Sequence")]
        [SerializeField] private AudioClip secondClip;
        private PhoneLetterBuilder emailBuilder;
        [SerializeField] private AudioClip afterEmailClip;

        [SerializeField] private AudioClip hangUpClip;

        private IEnumerator Start()
        {
            emailBuilder = new PhoneLetterBuilder(input, 0.2f);
            emailBuilder.SetEnable(false);
            
            yield return new WaitForPhoneNumber(input, phoneNumber, OnUpdateState);

            audioSource.clip = holdTone;
            audioSource.Play();
            yield return new WaitForSeconds(holdDuration);
            audioSource.Stop();
            
            //"You have reached Óbidos Hospital. If you're calling to schedule an appointment, please press .1"
            audioSource.PlayOneShot(introClip);
            yield return new WaitForSeconds(introClip.length);

            yield return new WaitForPhoneInput(input, firstNumber);
            
            //"To help us direct your call, please type your email address, using the letters on your telephone."
            audioSource.PlayOneShot(secondClip);
            yield return new WaitForSeconds(secondClip.length);

            emailBuilder.SetEnable(true);

            yield return new WaitForPhoneInput(input, "*");
            
            audioSource.PlayOneShot(afterEmailClip);
            yield return new WaitForSeconds(afterEmailClip.length + 1f);

            var email = $"{emailBuilder.CurrentText}@email.com";
            TextToSpeech.Start(email);
            
            yield return new WaitForSeconds(2f + email.Length * 0.2f);
            
            audioSource.PlayOneShot(hangUpClip);
            yield return new WaitForSeconds(hangUpClip.length);
            yield return new WaitForPhoneHangUp(input);

            SceneManager.LoadScene("CrazySequence");
        }

        private void Update()
        {
            emailBuilder.Update(Time.deltaTime);
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