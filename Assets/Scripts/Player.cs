using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * キャラの座標を変更するController
 */
public class Player : MonoBehaviour
{
    [SerializeField]
    float SPEED = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    Animator animator;

    void Start()
    {
        
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        // x,ｙの入力値を得る
        // それぞれ+や-の値と入力の関連付けはInput Managerで設定されている
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        if (Input.anyKeyDown)
        {
            Vector2? action = this.actionKeyDown();
            if (action.HasValue)
            {
                // キー入力があればAnimatorにstateをセットする
                setStateToAnimator(vector: action.Value);
                return;
            }
        }
        // 入力からVector2インスタンスを作成
        Vector2 vector = new Vector2(
            (int)Input.GetAxis("Horizontal"),
            (int)Input.GetAxis("Vertical"));

        // キー入力が続いている場合は、入力から作成したVector2を渡す
        // キー入力がなければ null
        setStateToAnimator(vector: vector != Vector2.zero ? vector : (Vector2?)null);
    }

    private void FixedUpdate()
    {
        // 速度を代入する
        rigidBody.MovePosition(rigidBody.position + inputAxis.normalized * SPEED  / 25f);
    }

    private void setStateToAnimator(Vector2? vector)
    {
        if (!vector.HasValue)
        {
            this.animator.speed = 0.0f;
            return;
        }

        Debug.Log(vector.Value);
        this.animator.speed = 1.0f;
        this.animator.SetFloat("x", vector.Value.x);
        this.animator.SetFloat("y", vector.Value.y);

    }

    private Vector2? actionKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }
}