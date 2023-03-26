#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EditorSetup
{
    static EditorSetup()
    {
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/LoginScene.unity");
        EditorSceneManager.playModeStartScene = sceneAsset;
    }
}

#endif
