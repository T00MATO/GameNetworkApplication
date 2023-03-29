using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class GameSetting : SettingObject<GameSetting>
{
    [SerializeField]
    private int _maxUsers;

    public static int MAX_USERS { get => Instance._maxUsers; }

#if UNITY_EDITOR
    [MenuItem("Settings/Edit/Game Setting")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : SettingEditor<GameSetting>
{
}
#endif
