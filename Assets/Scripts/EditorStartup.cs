#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EditorStartup
{
    public static readonly string START_SCENE_PATH = "Assets/Scenes/LoginScene.unity";

    static EditorStartup()
    {
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(START_SCENE_PATH);
        EditorSceneManager.playModeStartScene = sceneAsset;
    }
}
#endif
