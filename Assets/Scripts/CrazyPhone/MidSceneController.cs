using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.Yields;
using TenSecondsReplay.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyPhone
{
    public class MidSceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;

        [Header("Intro")] 
        [SerializeField] private AudioClip thankYouClip;

        [Header("Flies")] 
        [SerializeField] private GameObject flies;
        [SerializeField] private AudioClip likeFliesClip;

        [Header("Chose an appointment month")] 
        [SerializeField] private AudioClip appoMonthClip;

        [Header("Snail")] 
        [SerializeField] private GameObject snailObject;
        [SerializeField] private AudioClip snailClip;
        [SerializeField] private ShakePositionAnimation snailShake;

        [Header("Chose an appointment day")] 
        [SerializeField] private AudioClip appoDayClip;
        
        [Header("Squash the fly")] 
        [SerializeField] private AudioClip squashFlyClip;
        [SerializeField] private AudioClip goodWorkClip;

        [Header("Please help me")] 
        [SerializeField] private AudioClip pleaseHelpClip;
        [SerializeField] private AudioClip wrongSfx;
        [SerializeField] private AudioClip correctSfx;
        [SerializeField] private AudioClip thankYouForFollowingClip;

        [Header("Please Hold")] 
        [SerializeField] private AudioClip pleaseHoldClip;
        [SerializeField] private float holdMusicDuration;
        [SerializeField] private AudioClip toContinueClip;

        private IEnumerator Start()
        {
            //Thank you for completing the authentication procedure. You may now make your appointment. In order to proceed, we'd like to ask you a few questions.
            audioSource.PlayOneShot(thankYouClip);
            yield return new WaitForSeconds(thankYouClip.length + 1f);

            string answer;

            do
            {
                //Do you like flies?
                audioSource.PlayOneShot(likeFliesClip);
                flies.SetActive(true);
                yield return new WaitForSeconds(likeFliesClip.length);

                var flyAnswer = new WaitForAnyPhoneInput(input);
                yield return flyAnswer;
                answer = flyAnswer.Input;
            } 
            while (answer != "1");
            flies.SetActive(false);

            //To start the application process, please provide a month for your chosen appointment, using the the letters on your keypad.
            audioSource.PlayOneShot(appoMonthClip);
            yield return new WaitForSeconds(appoMonthClip.length + 1f);
            
            //It appears a snail is blocking you from progressing further in the application process. Please remove the snail.
            snailObject.SetActive(true);
            audioSource.PlayOneShot(snailClip);
            yield return new WaitForSeconds(snailClip.length + 1f);
            yield return new WaitForPhoneSpamInput(input, 10, OnSnailSpam, "6");
            snailObject.SetActive(false);
            yield return new WaitForSeconds(2f);

            //To start the application process, please provide a day for your chosen appointment, using the the letters on your keypad, followed by the star symbol.
            audioSource.PlayOneShot(appoDayClip);
            yield return new WaitForSeconds(appoDayClip.length + 1f);
            
            //Sounds like a fly has been trapped in your phone, please put the phone down to smash it. Don't forget to pick up again.
            audioSource.PlayOneShot(squashFlyClip);
            yield return new WaitForSeconds(squashFlyClip.length);
            yield return new WaitForPhoneHangUp(input);
            yield return new WaitForPhonePickUp(input);
            
            //Good work! You are almost there, now please listen carefully.
            audioSource.PlayOneShot(goodWorkClip);
            yield return new WaitForSeconds(goodWorkClip.length + 1f);
            
            //In order to get help, please say: Help me while pressing 5
            for (int i = 0; i < 2; i++)
            {
                audioSource.PlayOneShot(pleaseHelpClip);
                yield return new WaitForSeconds(pleaseHelpClip.length + 0.5f);
                yield return new WaitForPhoneInput(input, "5");
                audioSource.PlayOneShot(wrongSfx);
                yield return new WaitForSeconds(wrongSfx.length + 0.2f);
            }
            
            audioSource.PlayOneShot(pleaseHelpClip);
            yield return new WaitForSeconds(pleaseHelpClip.length + 0.5f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(correctSfx);
            yield return new WaitForSeconds(correctSfx.length + 0.2f);
            
            //Thank you for following the instructions carefully.
            audioSource.PlayOneShot(thankYouForFollowingClip);
            yield return new WaitForSeconds(thankYouForFollowingClip.length + 1f);
            
            //Hold music
            audioSource.clip = pleaseHoldClip;
            audioSource.Play();
            yield return new WaitForSeconds(holdMusicDuration);
            audioSource.Stop();
            
            //Now in order to continue, please hang up and call the number provided on the screen.
            audioSource.PlayOneShot(toContinueClip);
            yield return new WaitForSeconds(pleaseHelpClip.length + 0.5f);

            yield return new WaitForPhoneHangUp(input);
            SceneManager.LoadScene("CrazySequence");
        }

        private void OnSnailSpam(WaitForPhoneSpamInput spamInput, bool success)
        {
            if (success) snailShake.StartAnimation();
        }
    }
}