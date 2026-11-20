using UnityEngine;

public class CameraMotionController : MonoBehaviour
{
    [Header("追従ターゲット（メオチョンなど）")]
    public Transform target;

    [Header("基準となる位置オフセット")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float followSmoothness = 5f;

    [Header("ふわふわ浮遊・揺れの設定")]
    public float floatAmplitudeY = 0.25f; // 上下の揺れ幅
    public float floatAmplitudeX = 0.2f;  // 左右の揺れ幅
    public float floatSpeed = 1.5f;       // 揺れる速さ

    [Header("ターゲットを見つめるか")]
    public bool lookAtTarget = true;
    public Vector3 lookOffset = new Vector3(0f, 0.5f, 0f);

    private void LateUpdate()
    {
        if (target == null) return;

        // ふわふわした浮遊オフセットを計算
        float waveY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitudeY;
        float waveX = Mathf.Cos(Time.time * floatSpeed * 0.7f) * floatAmplitudeX;
        Vector3 floatOffset = new Vector3(waveX, waveY, 0f);

        // 目標位置へ滑らかに移動
        Vector3 desiredPosition = target.position + offset + floatOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmoothness * Time.deltaTime);

        // ターゲットの方向を向く
        if (lookAtTarget)
        {
            transform.LookAt(target.position + lookOffset);
        }
    }
}
