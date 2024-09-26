using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyPhone.Input
{
    public class PhoneTone : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip toneClip;

        private Dictionary<string, float> pitchMap = new Dictionary<string, float>()
        {
            {"1", 1.0f},   // C
            {"2", 1.122f}, // D
            {"3", 1.26f},  // E
            {"4", 1.335f}, // F
            {"5", 1.498f}, // G
            {"6", 1.682f}, // A
            {"7", 1.888f}, // B
            {"8", 2.0f},   // C (octave)
            {"9", 2.244f}, // D (octave)
            {"0", 2.52f},  // E (octave)
            {"*", 2.52f},  // E (octave)
            {"#", 2.52f},  // E (octave)
        };


        private void OnEnable()
        {
            input.onKeyDown += OnKeyDown;
        }

        private void OnKeyDown(string key)
        {
            if (!pitchMap.TryGetValue(key, out float pitch)) return;

            audioSource.pitch = pitch;
            audioSource.PlayOneShot(toneClip);
        }
    }
}