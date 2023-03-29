using UnityEngine.SceneManagement;

public static class ExtendedManager
{
    public static void LoadScene(SceneKey sceneKey)
    {
        var sceneIndex = SceneSetting.GetIndex(sceneKey);
        SceneManager.LoadScene(sceneIndex);
    }
}
