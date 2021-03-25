#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

public class ChangeScene : Editor
{

    [MenuItem("Scene/Hung Scene")]
    public static void OpenLoading()
    {
        OpenScene("HungScene");
    }

    [MenuItem("Scene/Game #2")]
    public static void OpenPlayerScene()
    {
        OpenScene("PlayScene");
    }

    [MenuItem("Scene/First Scene")]
    public static void OpenFirstScene()
    {
        OpenScene("FirstScene");
    }

    [MenuItem("Scene/Init Scene")]
    public static void OpenInitScene()
    {
        OpenScene("InitScene");
    }

    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Game/Scenes/" + sceneName + ".unity");
        }
    }
}
#endif