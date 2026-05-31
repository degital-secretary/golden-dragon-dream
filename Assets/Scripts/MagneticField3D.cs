using UnityEngine;

public class MagneticField3D : MonoBehaviour
{
    [Header("磁力設定")]
    [Tooltip("引き寄せる力（大きくすると高速で吸い寄せます）")]
    [SerializeField] private float magnetForce = 25f;
    
    [Tooltip("磁力が届く半径")]
    [SerializeField] private float radius = 5f;

    [Header("対象設定")]
    [Tooltip("磁力の影響を受けるオブジェクトのレイヤーを指定してください")]
    [SerializeField] private LayerMask targetLayer;

    // 3Dの物理演算は FixedUpdate で行います
    void FixedUpdate()
    {
        // 自分の位置を中心に、球体（3D空間）の範囲内にあるコライダーを全取得
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);

        foreach (Collider col in colliders)
        {
            // 3D用の Rigidbody を取得
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // 自分（磁石）に向かう3Dの方向ベクトルを計算
                Vector3 direction = transform.position - rb.position;
                
                // 3Dの力を加える（正規化ベクトルに力を乗算）
                rb.AddForce(direction.normalized * magnetForce);
            }
        }
    }

    // Unityのエディタ画面（Scene画面）上で、磁力の球体範囲を水色の線で表示します
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        // 3D用のワイヤーフレーム球体を描画
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
