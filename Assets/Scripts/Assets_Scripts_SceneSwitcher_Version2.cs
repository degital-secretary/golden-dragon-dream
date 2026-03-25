using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("Bキーを押した時に遷移するシーン名")]
    [SerializeField] private string sceneToLoad;

    private bool _isTransitioning = false; // 二重遷移防止用

    void Update()
    {
        // Bキーが押されたかチェック
        if (Input.GetKeyDown(KeyCode.B) && !_isTransitioning)
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("遷移先のシーン名が設定されていません！インスペクターから入力してください。");
            return;
        }

        _isTransitioning = true;
        Debug.Log($"シーン {sceneToLoad} へ遷移します...");
        SceneManager.LoadScene(sceneToLoad);
    }
}