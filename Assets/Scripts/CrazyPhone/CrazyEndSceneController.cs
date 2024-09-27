using System;
using System.Collections;
using System.Collections.Generic;
using CrazyPhone.Input;
using CrazyPhone.Utilities;
using CrazyPhone.Yields;
using TenSecondsReplay.Utilities;
using UnityEngine;
using UnityEngine.UI;
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

        [Header("Help Me")] 
        [SerializeField] private AudioClip wrongSfx;
        [SerializeField] private AudioClip correctSfx;
        [SerializeField] private AudioClip helpMeClip, helpMePleaseClip, louderClip;
        
        [Header("Scream for help")]
        [SerializeField] private AudioClip screamClip;
        [SerializeField] private AudioClip crackSfx;
        [SerializeField] private Image screenCrackImage;
        [SerializeField] private Sprite secondCrack, thirdCrack;
        [SerializeField] private ShakePositionAnimation screamShake;

        [SerializeField] private AudioClip finalWinClip;

        private IEnumerator Start()
        {
            input.onKeyDown += OnKeyDown;
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
            
            //[Distorted voice] In order to get help, please say: Help me.
            audioSource.PlayOneShot(helpMeClip);
            yield return new WaitForSeconds(helpMeClip.length + 0.5f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(wrongSfx);
            yield return new WaitForSeconds(wrongSfx.length + 0.2f);
            
            //Louder. In order to get help, please say: Help me, please!
            audioSource.PlayOneShot(helpMePleaseClip);
            yield return new WaitForSeconds(helpMePleaseClip.length + 0.5f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(wrongSfx);
            yield return new WaitForSeconds(wrongSfx.length + 0.2f);
            
            //I said louder!
            audioSource.PlayOneShot(louderClip);
            yield return new WaitForSeconds(louderClip.length + 0.2f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(wrongSfx);
            yield return new WaitForSeconds(wrongSfx.length + 0.2f);
            
            //I said louder!
            audioSource.PlayOneShot(louderClip);
            yield return new WaitForSeconds(louderClip.length + 0.2f);
            yield return new WaitForPhoneInput(input, "5");
            audioSource.PlayOneShot(correctSfx);
            yield return new WaitForSeconds(correctSfx.length + 0.2f);
            
            //Scream for help
            audioSource.PlayOneShot(screamClip);
            yield return new WaitForSeconds(screamClip.length);
            yield return new WaitForPhoneSpamInput(input, 50, OnUpdateFinalScreamSpam, PhoneMappings.KeypadStrings);
            
            //Congratulations.... You appointment for City of Óbidos National Sanatorium is confirmed. Thank you for calling. Good bye.
            yield return new WaitForSeconds(3f);
            
            audioSource.PlayOneShot(finalWinClip);
            yield return new WaitForSeconds(finalWinClip.length + 0.2f);
            
            Application.Quit();
        }

        private void OnUpdateFinalScreamSpam(WaitForPhoneSpamInput spamInput, bool success)
        {
            if (!success) return;
            
            switch (spamInput.NormalizedAlpha)
            {
                case > 0.1f and <= 0.25f: screenCrackImage.gameObject.SetActive(true); audioSource.PlayOneShot(crackSfx); break;
                case > 0.25f and <= 0.6f: screenCrackImage.sprite = secondCrack; audioSource.PlayOneShot(crackSfx); break;
                case >= 1f: screenCrackImage.sprite = thirdCrack; audioSource.PlayOneShot(crackSfx); break;
            }
            
            screamShake.StartAnimation();
        }

        private void OnUpdateScreamSpam(WaitForPhoneSpamInput spamInput, bool success)
        {
            screamSource.volume = screamProgressCurve.Evaluate(spamInput.NormalizedAlpha);
        }

        private void OnKeyDown(string msg)
        {
            KillSwarmUpdate(msg);
        }

        private void KillSwarmUpdate(string msg)
        {
            if (!isKillingSwarm) return;
            if (activeFlies.Count <= 0) return;
            if (msg != "down") return;
            var fly = activeFlies.GetRandom();
            fly.gameObject.SetActive(false);
            flyLeft--;
            activeFlies.Remove(fly);
        }
    }
}