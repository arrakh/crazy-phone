using System.Runtime.InteropServices;

namespace CrazyPhone.Utilities
{
    sealed class TextToSpeech
    {
        public static void Start(string text)
            => ttsrust_say(text);

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_WEBGL)
    const string _dll = "__Internal";
#else
        const string _dll = "ttsrust";
#endif

        [DllImport(_dll)] static extern void ttsrust_say(string text);
    }
}
