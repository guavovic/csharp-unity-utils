using System;
using UnityEngine;

namespace GV.Extensions
{
    /// <summary>
    /// PlayerPrefs para bool, enum e objetos serializáveis (como JSON).
    /// </summary>
    public static class TypedPlayerPrefs
    {
        public static void SetBool(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);

        public static bool GetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) != 0 : defaultValue;
        }

        public static void SetEnum<T>(string key, T value) where T : struct, Enum
        {
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        }

        public static T GetEnum<T>(string key, T defaultValue = default) where T : struct, Enum
        {
            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            object stored = Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key));
            return Enum.IsDefined(typeof(T), stored) ? (T)stored : defaultValue;
        }

        public static void SetObject<T>(string key, T value)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
        }

        public static T GetObject<T>(string key, T defaultValue = default)
        {
            string json = PlayerPrefs.GetString(key, "");

            if (string.IsNullOrEmpty(json))
                return defaultValue;

            try
            {
                return JsonUtility.FromJson<T>(json);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }
    }
}
