using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Config))]
public class ConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(Config.Instance);

        DrawDefaultInspector();
    }
}
#endif

[Serializable]
public class Config : ScriptableObject
{
    private static readonly string CONFIG_SETTING_NAME = "ConfigSetting";
    private static readonly string CONFIG_SETTING_PATH = $"Assets/Resources/{CONFIG_SETTING_NAME}.asset";

    private static Config _instance;
    public static Config Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<Config>(CONFIG_SETTING_NAME);

                if (_instance == null)
                {
                    _instance = CreateInstance<Config>();
#if UNITY_EDITOR
                    AssetDatabase.CreateAsset(_instance, CONFIG_SETTING_PATH);
#endif
                }
            }
            return _instance;
        }
    }
    
    [Header("Server End Point")]

    [SerializeField]
    private string _serverIp;
    [SerializeField]
    private ushort _serverPort;
    
    public static string ServerIp { get => Instance._serverIp; }
    public static ushort ServerPort { get => Instance._serverPort; }

#if UNITY_EDITOR
    [MenuItem("Config/Edit")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }
#endif
}