using UnityEngine;

namespace Utils.Drawers.Key
{
    public class KeyValueFormatAttribute : PropertyAttribute
    {
        public readonly string Format;
        public readonly string[] Args;

        public KeyValueFormatAttribute(string format, params string[] args)
        {
            Format = format;
            Args = args;
        }
    }
}