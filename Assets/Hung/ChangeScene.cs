#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor {

    [MenuItem("Scene/Hung Scene")]
    public static void OpenLoading()
    {
        OpenScene("HungScene");
    }

    [MenuItem("Scene/Game #2")]
    public static void OpenGame()
    {
        OpenScene("Scene");
    }
    private static void OpenScene (string sceneName) {
		if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ()) {
			EditorSceneManager.OpenScene ("Assets/Game/Scenes/" + sceneName + ".unity");
		}
	}
}
#endif