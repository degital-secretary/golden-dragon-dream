using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Animator animator;
    public string motionName = "Flap";

    void Start()
    {
        // 自分のオブジェクトについているAnimatorを取得する
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // // 「B」キーが押された瞬間を検知する、、、やっぱ「C」に、、、。
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Cキーが押されました！"); // コンソールにログを出す
            // // アニメーションを最初から再生する
            // ※ "YourAnimationName" の部分を、実際のFBX内のアニメーション名（例: "Armature|Action" や "Flap" など）に変更してください
            animator.Play(motionName, 0, 0f);
        }
    }
}

