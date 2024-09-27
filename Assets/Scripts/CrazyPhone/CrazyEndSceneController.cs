using System;
using System.Collections;
using System.Collections.Generic;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrazyPhone
{
    public class CrazyEndSceneController : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;

        [Header("Smash Fly")]
        [SerializeField] private AudioSource flyLoopSource;
        [SerializeField] private GameObject squashFirstFlyObject;
        [SerializeField] private GameObject[] flySwarm;
        private bool isKillingSwarm = false;
        private int flyLeft;
        private List<GameObject> activeFlies = new();

        [Header("Smash Buttons")] 
        [SerializeField] private AudioSource screamSource;
        [SerializeField] private AnimationCurve screamProgressCurve;

        private IEnumerator Start()
        {
            //First Fly
            flyLoopSource.Play();
            squashFirstFlyObject.SetActive(true);
            yield return new WaitForPhoneHangUp(input);
            squashFirstFlyObject.SetActive(false);
            flyLoopSource.Stop();
            yield return new WaitForPhonePickUp(input);
            yield return new WaitForSeconds(1f);

            //Fly Swarm
            flyLoopSource.Play();
            isKillingSwarm = true;
            flyLeft = flySwarm.Length;
            foreach (var fly in flySwarm)
            {
                fly.gameObject.SetActive(true);
                activeFlies.Add(fly);
                var delay = Random.Range(0.2f, 0.4f);
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => flyLeft <= 0);
            flyLoopSource.Stop();
            
            //Smash buttons to stop screaming 
            screamSource.Play();
            yield return new WaitForPhoneSpamInput(input, 20, OnUpdateScreamSpam, PhoneMappings.KeypadStrings);
        }

        private void OnUpdateScreamSpam(WaitForPhoneSpamInput spamInput, bool success)
        {
            screamSource.volume = screamProgressCurve.Evaluate(1f - spamInput.NormalizedAlpha);
        }

        private void Update()
        {
            KillSwarmUpdate();
        }   

        private void KillSwarmUpdate()
        {
            if (!isKillingSwarm) return;
            if (activeFlies.Count <= 0) return;
            if (!input.GetKeyDown("down")) return;
            var fly = activeFlies.GetRandom();
            fly.gameObject.SetActive(false);
            flyLeft--;
            activeFlies.Remove(fly);
        }
    }
}