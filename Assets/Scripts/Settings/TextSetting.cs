using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Flags]
public enum TextKey : byte
{
    MATCH_START,
    MATCH_WAITING,
    MATCH_SUCCEED,
}

[Serializable]
public class TextSetting : SettingObject<TextSetting>
{
    [Serializable]
    private struct TextKeyValuePair
    {
        public TextKey TextKey;
        public string Text;
    }

    [SerializeField]
    private List<TextKeyValuePair> _texts;

    private Dictionary<TextKey, string> _textsCahce = new();

    private void OnEnable()
    {
        foreach (var text in _texts)
        {
            _textsCahce[text.TextKey] = text.Text;
        }
    }

    public static string GetText(TextKey textKey)
    {
        return Instance._textsCahce[textKey];
    }

#if UNITY_EDITOR
    [MenuItem("Settings/Edit/Text Setting")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TextSetting))]
public class TextSettingEditor : SettingEditor<TextSetting>
{
}
#endif
