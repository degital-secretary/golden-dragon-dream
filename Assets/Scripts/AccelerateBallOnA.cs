using UnityEngine;

public class MeochonMovementWithBrake : MonoBehaviour
{
    [Header("移動パラメータ")]
    public float acceleration = 15f;   // 加速度
    public float maxSpeed = 10f;       // 最高速度
    public float brakePower = 20f;     // ブレーキの強さ（Rキー）
    
    [Header("サバイバル設定")]
    public bool isBatteryDead = false; // バッテリー（リモコン）切断フラグ

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // リモコン（Aキー）が死んでいたら動かない
        if (isBatteryDead) return;

        // 【Rキーが押されている場合：ブレーキ（急減速）】
        if (Input.GetKey(KeyCode.R))
        {
            // 進行方向とは逆に力を加えてスピードを急激に落とす
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * (brakePower * 0.2f));
        }
        // 【Aキーが押されている場合：加速】
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

            // 最高速度の制限
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        else
        {
            // どちらも押していないときは緩やかに減速
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * 3f);
        }
    }

    // 外部からバッテリー切れを引き起こす関数
    public void TriggerBatteryFailure()
    {
        isBatteryDead = true;
        rb.linearVelocity = Vector3.zero; // ぷつんと止まる
    }
}