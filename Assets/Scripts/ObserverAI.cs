using UnityEngine;

public class MeochonFloatObserver : MonoBehaviour
{
    [Header("ふわふわパラメータ")]
    public float floatSpeed = 2f;    // 揺れるスピード
    public float floatHeight = 0.5f; // 揺れる高さ
    
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // サイン波を使って一定のリズムで上下にふわふわ浮遊させる
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 管理者らしく、少しずつカメラの方向（あるいは一定の方向）を向く
        transform.Rotate(Vector3.up * Time.deltaTime * 10f);
    }
}
