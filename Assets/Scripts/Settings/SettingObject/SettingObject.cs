using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public abstract class SettingObject<T> : ScriptableObject where T : SettingObject<T>
{
    private static string SETTING_NAME { get => typeof(T).Name; }
#if UNITY_EDITOR
    private static string SETTING_PATH { get => $"Assets/Resources/{SETTING_NAME}.asset"; }
#endif

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<T>(SETTING_NAME);
                if (_instance == null)
                {
                    _instance = CreateInstance<T>();
#if UNITY_EDITOR
                    AssetDatabase.CreateAsset(_instance, SETTING_PATH);
#endif
                }
            }
            return _instance;
        }
    }
}

#if UNITY_EDITOR
public class SettingEditor<T> : Editor where T : SettingObject<T>
{
    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(SettingObject<T>.Instance);

        DrawDefaultInspector();
    }
}
#endif
