using System;
using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.UI;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

namespace CrazyPhone
{
    public class CrazySceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;

        [Header("Call Asylum")]
        [SerializeField] private string phoneNumber = "915";
        
        [Header("Hold tone")]
        [SerializeField] private AudioClip holdTone;
        [SerializeField] private float holdDuration = 9f;
        
        [Header("Intro")]
        [SerializeField] private AudioClip asylumIntro;

        [Header("Name Warped")] 
        [SerializeField] private AudioClip namePromptClip;
        [SerializeField] private TextWindow textWindow;
        [SerializeField] private ScaleAnimation textScaleOut;
        
        [Header("Smash Fly")]
        [SerializeField] private AudioSource flyLoopSource;
        [SerializeField] private GameObject squashFlyObject;
        [SerializeField] private AudioClip squashFlyClip;

        [Header("Help Me")] 
        [SerializeField] private AudioClip beNiceClip;
        [SerializeField] private AudioClip helpMeClip;
        [SerializeField] private AudioClip wrongSfx;
        [SerializeField] private AudioClip correctSfx;
        [SerializeField] private AudioClip thankYouForFollowingClip;

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

        [SerializeField] private AudioClip wrongClip;

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
            yield return new WaitForSeconds(asylumIntro.length + 0.3f);

            //For authentication purposes, please type your name
            audioSource.PlayOneShot(namePromptClip);
            yield return new WaitForSeconds(namePromptClip.length + 0.4f);
            
            textWindow.Initialize(PlayerInfo.name, 0.5f, true);
            textWindow.gameObject.SetActive(true);

            yield return new WaitUntil(() => textWindow.IsCompleted);
            
            textScaleOut.StartAnimation();
            yield return new WaitForSeconds(textScaleOut.Duration);
            textWindow.gameObject.SetActive(false);
            
            // Smash the fly!
            flyLoopSource.Play();
            squashFlyObject.SetActive(true);
            audioSource.PlayOneShot(squashFlyClip);
            yield return new WaitForSeconds(squashFlyClip.length);
            yield return new WaitForPhoneHangUp(input);
            squashFlyObject.SetActive(false);
            flyLoopSource.Stop();
            yield return new WaitForPhonePickUp(input);
            yield return new WaitForSeconds(2f);
            
            //In order to get help, please say: Help me, be nice
            audioSource.PlayOneShot(beNiceClip);
            yield return new WaitForSeconds(beNiceClip.length + 0.5f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(wrongSfx);
            yield return new WaitForSeconds(wrongSfx.length + 0.2f);
            
            //In order to get help, please say: Help me while pressing 5. PART 1
            audioSource.PlayOneShot(helpMeClip);
            yield return new WaitForSeconds(helpMeClip.length + 0.5f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(wrongSfx);
            yield return new WaitForSeconds(wrongSfx.length + 0.2f);
            
            //In order to get help, please say: Help me while pressing 5. PART 2
            audioSource.PlayOneShot(helpMeClip);
            yield return new WaitForSeconds(helpMeClip.length + 0.5f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(correctSfx);
            yield return new WaitForSeconds(correctSfx.length + 0.2f);
            
            //Thank you for following the instructions carefully.
            audioSource.PlayOneShot(thankYouForFollowingClip);
            yield return new WaitForSeconds(thankYouForFollowingClip.length + 0.5f);

            SceneManager.LoadScene("FormSequence 2");

            yield break;

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