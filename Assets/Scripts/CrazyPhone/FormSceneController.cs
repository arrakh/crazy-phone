using System;
using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using UnityEngine;

namespace CrazyPhone
{
    public class FormSceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;
        
        [Header("Audio Clips")]
        [SerializeField] private AudioClip pageSwitch;
        [SerializeField] private AudioClip wrongClip;
        [SerializeField] private AudioClip success;
        [SerializeField] private ScaleAnimation popInAnimation, popOutAnimation;

        private string formConfirmationNumber = "215920";

        private IEnumerator Start()
        {            
            popInAnimation.StartAnimation();

            yield return new WaitForPhoneNumber(input, formConfirmationNumber, OnUpdateState);
            yield return new WaitForSeconds(1f);

            audioSource.PlayOneShot(success);
            yield return new WaitForSeconds(success.length);

            popOutAnimation.StartAnimation();
        }

        private void OnUpdateState(WaitForPhoneNumber ph, bool wasCorrect)
        {
            if (!wasCorrect)
            {
                ph.Clear();
            }
        }
    }
}