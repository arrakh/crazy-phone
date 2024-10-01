using System;
using System.Collections;
using System.IO.Ports;
using System.Linq;
using CrazyPhone.Input;
using CrazyPhone.Yields;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CrazyPhone
{
    public class DetectController : MonoBehaviour
    {
        [SerializeField] private PhoneInput phoneInput;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI status;
        
        IEnumerator Start()
        {
            var ports = SerialPort.GetPortNames();
            var progress = ports.Length * 2;

            var currentProgress = 0;
            bool connected = false;
            
            foreach (var port in ports)
            {
                phoneInput.gameObject.SetActive(false);
                PhoneInput.PORT = port;
                yield return new WaitForEndOfFrame();
                phoneInput.gameObject.SetActive(true);
                
                UpdateStatus($"connecting to {PhoneInput.PORT}");

                var waitUntil = new WaitUntil(() => phoneInput.IsConnected).TimeoutParallel(2f);
                yield return waitUntil;
                UpdateProgress(currentProgress++, progress);

                if (waitUntil.HasTimedOut)
                {
                    UpdateProgress(currentProgress++, progress);
                    continue;
                }

                try
                {
                    phoneInput.SendSerialMessage("ping");
                    UpdateStatus($"pinging {PhoneInput.PORT}");
                }
                catch (Exception e)
                {
                    UpdateStatus(e.Message);
                    UpdateProgress(currentProgress++, progress);
                    continue;
                }

                var pong = new WaitForPhoneSerializedMessage(phoneInput, "pong").TimeoutParallel(2f);
                yield return pong;

                if (pong.HasTimedOut)
                {
                    UpdateProgress(currentProgress++, progress);
                    continue;
                }
                
                UpdateProgress(progress, progress);
                UpdateStatus($"connected to {PhoneInput.PORT}");
                connected = true;
                break;
            }
            
            if (!connected) UpdateStatus("no device connected!");
            UpdateProgress(progress, progress);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("IntroSequence");
        }

        void UpdateProgress(int current, int max)
        {
            slider.value = (float) current / max;
        }

        void UpdateStatus(string text)
        {
            status.text = text;
        }
    }
}