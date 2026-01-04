using UnityEngine;
using UnityEngine.SceneManagement;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class SceneSwitcher : MonoBehaviour
{
    [Tooltip("切り替えたいシーン名を指定。空欄なら Build Settings の次のシーンへ移動")]
    public string targetSceneName = "";

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame)
        {
            SwitchScene();
        }
#else
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchScene();
        }
#endif
    }

    private void SwitchScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
            return;
        }

        int current = SceneManager.GetActiveScene().buildIndex;
        int count = SceneManager.sceneCountInBuildSettings;
        int next = (current + 1) % Mathf.Max(1, count);
        SceneManager.LoadScene(next);
    }
}