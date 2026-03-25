using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Header("--- 遷移設定 ---")]
    [Tooltip("Bキーを押した時に移動したいシーンの名前を入力してください")]
    [SerializeField] private string targetSceneName = "TitleScene";

    [Header("--- デバッグ設定 ---")]
    [SerializeField] private bool showDebugLog = true;

    private bool _isProcessing = false;

    void Update()
    {
        // 「B」キーが押された瞬間、かつ遷移中でない場合のみ実行
        if (Input.GetKeyDown(KeyCode.B) && !_isProcessing)
        {
            ExecuteSceneTransition();
        }
    }

    private void ExecuteSceneTransition()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("<color=red>【警告】</color> 遷移先のシーン名が空です！Inspectorで名前を入れてください。");
            return;
        }

        _isProcessing = true; // 連打防止

        if (showDebugLog)
        {
            Debug.Log($"<color=cyan>【Bキー作戦発動】</color> {targetSceneName} へ移動を開始します...");
        }

        // シーンをロード
        SceneManager.LoadScene(targetSceneName);
    }
}
