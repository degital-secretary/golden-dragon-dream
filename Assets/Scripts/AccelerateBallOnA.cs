using UnityEngine;

public class AccelerateBallOnA : MonoBehaviour
{
    public float acceleration = 10f;   // 加速度
    public float maxSpeed = 20f;       // 最高速度
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // 前方に加速（transform.forward方向）
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

            // 最高速度制限
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }
}