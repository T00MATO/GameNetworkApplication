using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class ServerSetting : SettingObject<ServerSetting>
{
    [Header("Server End Point")]

    [SerializeField]
    private string _remoteIp;
    [SerializeField]
    private ushort _remotePort;

    public static string REMOTE_IP { get => Instance._remoteIp; }
    public static ushort REMOTE_PORT { get => Instance._remotePort; }

#if UNITY_EDITOR
    [MenuItem("Settings/Edit/Server Setting")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ServerSetting))]
public class ServerSettingEditor : SettingEditor<ServerSetting>
{
}
#endif
