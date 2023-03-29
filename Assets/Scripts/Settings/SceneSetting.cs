using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Flags]
public enum SceneKey : byte
{
    LOGIN,
    LOBBY,
    ROOM,
}

[Serializable]
public class SceneSetting : SettingObject<SceneSetting>
{
    [SerializeField]
    private List<SceneKey> _sceneKeyOrders;

    private Dictionary<SceneKey, int> _sceneKeyOrdersCache = new();

    private void OnEnable()
    {
        foreach (SceneKey index in Enum.GetValues(typeof(SceneKey)))
        {
            if (!_sceneKeyOrders.Contains(index))
            {
                _sceneKeyOrders.Add(index);
            }

            var order = _sceneKeyOrders.IndexOf(index);
            _sceneKeyOrdersCache.Add(index, order);
        }
    }

    public static int GetIndex(SceneKey sceneIndex)
    {
        return Instance._sceneKeyOrdersCache[sceneIndex];
    }

#if UNITY_EDITOR
    [MenuItem("Settings/Edit/Scene Setting")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneSetting))]
public class SceneSettingEditor : SettingEditor<SceneSetting>
{
}
#endif
