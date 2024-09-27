using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrazyPhone.Input
{
    public static class PhoneMappings
    {
        private static Dictionary<string, char[]> _mapping = new(StringComparer.OrdinalIgnoreCase)
        {
            {"2", new []{'a', 'b', 'c'}},
            {"3", new []{'d', 'e', 'f'}},
            {"4", new []{'g', 'h', 'i'}},
            {"5", new []{'j', 'k', 'l'}},
            {"6", new []{'m', 'n', 'o'}},
            {"7", new []{'p', 'r', 's'}},
            {"8", new []{'t', 'u', 'v'}},
            {"9", new []{'w', 'x', 'y'}},
        };
        
        private static Dictionary<string, char[]> _mappingWarped = new(StringComparer.OrdinalIgnoreCase)
        {
            {"2", new []{'a', 'b', 'c'}},
            {"3", new []{'d', 'e', 'f'}},
            {"4", new []{'g', 'h', 'i'}},
            {"5", new []{'j', 'k', 'l'}},
            {"6", new []{'m', 'n', 'o'}},
            {"7", new []{'p', 'r', 's'}},
            {"8", new []{'t', 'u', 'v'}},
            {"9", new []{'w', 'x', 'y'}},
        };

        public static string[] KeypadStrings = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", };
        
        public static Dictionary<KeyCode, string> KeycodeMappings = new()
        {
            {KeyCode.Alpha0, "0"},
            {KeyCode.Alpha1, "1"},
            {KeyCode.Alpha2, "2"},
            {KeyCode.Alpha3, "3"},
            {KeyCode.Alpha4, "4"},
            {KeyCode.Alpha5, "5"},
            {KeyCode.Alpha6, "6"},
            {KeyCode.Alpha7, "7"},
            {KeyCode.Alpha8, "8"},
            {KeyCode.Alpha9, "9"},
            
            {KeyCode.Keypad0, "0"},
            {KeyCode.Keypad1, "1"},
            {KeyCode.Keypad2, "2"},
            {KeyCode.Keypad3, "3"},
            {KeyCode.Keypad4, "4"},
            {KeyCode.Keypad5, "5"},
            {KeyCode.Keypad6, "6"},
            {KeyCode.Keypad7, "7"},
            {KeyCode.Keypad8, "8"},
            {KeyCode.Keypad9, "9"},

            {KeyCode.A, "*"},
            {KeyCode.S, "#"},

            {KeyCode.UpArrow, "up"},
            {KeyCode.DownArrow, "down"},
        };

        public static char Get(string key, int index)
        {
            if (!_mapping.ContainsKey(key)) return ' ';
            
            var letters = _mapping[key];

            int wrappedIndex = index % letters.Length;

            return letters[wrappedIndex];
        }

        public static char GetWarped(string key, int index)
        {
            if (!_mappingWarped.ContainsKey(key)) return ' ';
            
            var letters = _mappingWarped[key];

            int wrappedIndex = index % letters.Length;

            return letters[wrappedIndex];
        }
    }
}