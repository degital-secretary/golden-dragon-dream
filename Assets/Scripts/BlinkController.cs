using System.Collections;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("まぶたパーツの SkinnedMeshRenderer")]
    public SkinnedMeshRenderer eyelidRenderer;

    [Tooltip("BlendShapeのインデックス番号 (通常は 0)")]
    public int blendShapeIndex = 0;

    [Header("まばたきの間隔（秒）")]
    public float minBlinkInterval = 2.0f; // 最短インターバル
    public float maxBlinkInterval = 5.0f; // 最長インターバル

    [Header("まばたきの速度（秒）")]
    public float blinkSpeed = 0.08f;      // 閉じる・開くスピード

    private void Start()
    {
        if (eyelidRenderer == null)
        {
            eyelidRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        // 自動まばたきのループを開始
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // 次のまばたきまでのランダムな待ち時間
            float waitTime = Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(waitTime);

            // 目を閉じる (0 -> 100)
            float timer = 0f;
            while (timer < blinkSpeed)
            {
                timer += Time.deltaTime;
                float weight = Mathf.Lerp(0f, 100f, timer / blinkSpeed);
                eyelidRenderer.SetBlendShapeWeight(blendShapeIndex, weight);
                yield return null;
            }
            eyelidRenderer.SetBlendShapeWeight(blendShapeIndex, 100f);

            // 目を開ける (100 -> 0)
            timer = 0f;
            while (timer < blinkSpeed)
            {
                timer += Time.deltaTime;
                float weight = Mathf.Lerp(100f, 0f, timer / blinkSpeed);
                eyelidRenderer.SetBlendShapeWeight(blendShapeIndex, weight);
                yield return null;
            }
            eyelidRenderer.SetBlendShapeWeight(blendShapeIndex, 0f);
        }
    }
}
