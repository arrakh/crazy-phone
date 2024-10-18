using System;
using System.Collections;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyPhone
{
    public class FormSceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private string formConfirmationNumber;
        
        [Header("Audio Clips")]
        [SerializeField] private AudioClip pageSwitch;
        [SerializeField] private AudioClip wrongClip;
        [SerializeField] private AudioClip success;
        [SerializeField] private ScaleAnimation popInAnimation, popOutAnimation;
        [SerializeField] private AudioClip introClip;

        private IEnumerator Start()
        {            
            popInAnimation.StartAnimation();
            audioSource.PlayOneShot(introClip);

            yield return new WaitForPhoneNumber(input, formConfirmationNumber, OnUpdateState);
            yield return new WaitForSeconds(1f);

            audioSource.PlayOneShot(success);
            yield return new WaitForSeconds(success.length);

            popOutAnimation.StartAnimation();
            
            yield return new WaitForSeconds(popOutAnimation.Duration);

            SceneManager.LoadScene("MidSequence");
        }

        private void OnUpdateState(WaitForPhoneNumber ph, bool wasCorrect)
        {
            if (!wasCorrect)
            {
                ph.Clear();
                
                // TODO: This does not work
                // if (ph.CurrentProgress[^1].Equals("0")) audioSource.PlayOneShot(wrongClip);
            }
        }
    }
}