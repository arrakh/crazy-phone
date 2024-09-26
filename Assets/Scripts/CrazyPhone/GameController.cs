using System.Collections;
using CrazyPhone.Input;
using UnityEngine;

namespace CrazyPhone
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;

        [Header("First Sequence")]
        [SerializeField] private AudioClip introClip;
        [SerializeField] private string firstNumber;

        private IEnumerator Start()
        {
            yield return null;
        }
    }
}