using System.Collections;
using CrazyPhone.Input;
using UnityEngine;

namespace CrazyPhone
{
    public class FirstSceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;
        
        [Header("Number")]
        [SerializeField] private string phoneNumber = "915";

        [Header("First Sequence")]
        [SerializeField] private AudioClip introClip;
        [SerializeField] private string firstNumber;

        [Header("Second Sequence")]
        [SerializeField] private AudioClip secondClip;

        private IEnumerator Start()
        {
            yield return new WaitForPhoneNumber(input, phoneNumber);
            
            //"You have reached Óbidos Hospital. If you're calling to schedule an appointment, please press .1"
            audioSource.PlayOneShot(introClip);
            yield return new WaitForSeconds(introClip.length);

            yield return new WaitForPhoneInput(input, firstNumber);
            
            //"To help us direct your call, please type your email address, using the letters on your telephone."
            audioSource.PlayOneShot(secondClip);
        }
    }
}