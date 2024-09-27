using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyPhone.Input
{
    public partial class PhoneInput : MonoBehaviour
    {
        private const string PORT = "COM12";

        private const char DELIMITER = '~';
        private const string CLOSE = "down";
        private const string OPEN = "up";
        private const char DOWN = 'd';
        private const char UP = 'u';

        [SerializeField] private SerialController serialController;
        
        private HashSet<string> currentlyPressed = new ();
        private HashSet<string> thisFrame = new();
        private HashSet<string> releasedThisFrame = new();
        
        private bool isClosed, isConnected;

        public event Action<string> onKeyDown; 
        public bool IsClosed => isClosed;

        private void Awake()
        {
            serialController.portName = PORT;
        }

        private void LateUpdate()
        {
            thisFrame = new();
            releasedThisFrame = new(); // Reset the released keys each frame
        }

        public bool GetKey(string key) => currentlyPressed.Contains(key);
        public bool GetKeyDown(string key) => thisFrame.Contains(key);
        public bool GetKeyUp(string key) => releasedThisFrame.Contains(key);

        public void SendSerialMessage(string msg) => serialController.SendSerialMessage(msg);
        
        void OnMessageArrived(string msg)
        {
            //Debug.Log(msg);

            var split = msg.Split(DELIMITER);
            if (string.IsNullOrWhiteSpace(split[0])) return;

            var key = split[0];

            switch (key)
            {
                case CLOSE: isClosed = true; return;
                case OPEN: isClosed = false; return;
            }
            
            var state = split[1];
            
            bool isDown = state[0] == DOWN;
            if (!isDown && state[0] != UP) return;
            
            if (isDown) 
            {
                onKeyDown?.Invoke(key);
                currentlyPressed.Add(key);
                thisFrame.Add(key);
            }
            else 
            {
                if (currentlyPressed.Contains(key)) 
                {
                    currentlyPressed.Remove(key);
                    releasedThisFrame.Add(key);
                }
            }
        }

        void OnConnectionEvent(bool success)
        {
            isConnected = success;
            //Debug.Log($"Connection Event: {success}");
        }

        public void SetLight(bool on) => serialController.SendSerialMessage(on ? "lightOn" : "lightOff");

    }
}