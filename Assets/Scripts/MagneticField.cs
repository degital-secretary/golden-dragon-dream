using UnityEngine;

public class MagneticField : MonoBehaviour
{
    [Header("磁力設定")]
    [Tooltip("引き寄せる力（大きくすると高速で吸い寄せます）")]
    [SerializeField] private float magnetForce = 25f;
    
    [Tooltip("磁力が届く半径")]
    [SerializeField] private float radius = 5f;

    [Header("対象設定")]
    [Tooltip("磁力の影響を受けるオブジェクトのレイヤーを指定してください")]
    [SerializeField] private LayerMask targetLayer;

    // 物理演算（力を加える処理）は FixedUpdate で行います
    void FixedUpdate()
    {
        // 自分の位置を中心に、半径 radius 内にある targetLayer のコライダーを全取得
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // 自分（磁石）に向かう方向ベクトルを計算
                Vector2 direction = (Vector2)transform.position - rb.position;
                
                // 距離が近いほど強く引き寄せる（正規化ベクトルに力を乗算）
                rb.AddForce(direction.normalized * magnetForce);
            }
        }
    }

    // Unityのエディタ画面上で、磁力の範囲を水色の線で表示します
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
