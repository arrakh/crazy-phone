using System;
using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using UnityEngine;
using UnityEngine.Windows;

namespace CrazyPhone
{
    public class CrazySceneController : MonoBehaviour
    {

        [SerializeField] private PhoneInput input;
        [SerializeField] private string phoneNumber = "915";
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip wrongClip;
        [SerializeField] private AudioClip holdTone;
        [SerializeField] private float holdDuration = 9f;
        [SerializeField] private AudioClip asylumIntro;
        [SerializeField] private AudioClip helpMe;
        [SerializeField] private AudioClip press5;
        [SerializeField] private string Five;
        [SerializeField] private AudioClip sayPlease;
        [SerializeField] private string Seven;
        [SerializeField] private AudioClip sayHelpMePlease;
        [SerializeField] private string Nine;
        [SerializeField] private AudioClip louder;
        [SerializeField] private AudioClip iSaidLouder;
        [SerializeField] private AudioClip screamAsHardAsYouCan;
        [SerializeField] private AudioClip appointmentConfirmed;

        private IEnumerator Start()
        {
            //Wait for new phone number input to call insane asylum
            yield return new WaitForPhoneNumber(input, phoneNumber, OnUpdateState);

            audioSource.clip = holdTone;
            audioSource.Play();
            yield return new WaitForSeconds(holdDuration);
            audioSource.Stop();

            //"You've reached City of Óbidos National Sanatorium. In order to get help, please say:"
            audioSource.PlayOneShot(asylumIntro);
            yield return new WaitForSeconds(asylumIntro.length);

            //"Help me"
            audioSource.PlayOneShot(helpMe);
            yield return new WaitForSeconds(helpMe.length);

            //"and press 5"
            audioSource.PlayOneShot(press5);
            yield return new WaitForSeconds(press5.length);

            //Wait for input: 5
            yield return new WaitForPhoneInput(input, Five);

            //"You must say please and press 7"
            audioSource.PlayOneShot(sayPlease);
            yield return new WaitForSeconds(sayPlease.length);

            //Wait for input: 7
            yield return new WaitForPhoneInput(input, Seven);

            //"You must say Help me please and press 9"
            audioSource.PlayOneShot(sayHelpMePlease);
            yield return new WaitForSeconds(sayHelpMePlease.length);

            //Wait for input: 9
            yield return new WaitForPhoneInput(input, Nine);

            //"Louder and press 9."
            audioSource.PlayOneShot(louder);
            yield return new WaitForSeconds(louder.length);

            //Wait for input: 9
            yield return new WaitForPhoneInput(input, Nine);

            //"I said louder!
            audioSource.PlayOneShot(iSaidLouder);
            yield return new WaitForSeconds(iSaidLouder.length);

            //Wait for input: 9
            yield return new WaitForPhoneInput(input, Nine);

            //"Good, now scream as hard as you can"
            audioSource.PlayOneShot(screamAsHardAsYouCan);
            yield return new WaitForSeconds(screamAsHardAsYouCan.length);

            //Wait for input: 9
            yield return new WaitForPhoneInput(input, Nine);

            //"Appointment Confirmed
            audioSource.PlayOneShot(appointmentConfirmed);
            yield return new WaitForSeconds(appointmentConfirmed.length);


        }

        private void Update()
        {
            
        }

        private void OnUpdateState(WaitForPhoneNumber phoneNumber, bool wasCorrect)
        {
            if (!wasCorrect)
            {
                audioSource.PlayOneShot(wrongClip);
                phoneNumber.Clear();
            }
        }


    }
}