using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * �L�����̍��W��ύX����Controller
 */
public class Player : MonoBehaviour
{
    [SerializeField]
    float SPEED = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    Animator animator;
    [SerializeField] Transform footprintGenerator;
    [SerializeField] ParticleSystem footprint_L;
    [SerializeField] ParticleSystem footprint_R;

    [SerializeField] Collider2D interactingCollider;
    Collider2D[] results = new Collider2D[8];
    Interaction result;

    bool IsInteract()
    {
        // collider2d�ƏՓ˂��Ă���collider�̐����Ԃ��Ă���
        int hitCount = interactingCollider.OverlapCollider(new ContactFilter2D(), results);

        if (hitCount > 0)
        {
            Debug.Log("����");
            for (int i = 0; i < hitCount; i++)
            {
                if (results[i].tag == "Interactable")
                {
                    Debug.Log("�C���^���N�g");
                    result = results[i].gameObject.GetComponent<Interaction>();
                    return true;
                }
            }
        }
        return false;
    }

    void Start()
    {
        
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        // x,���̓��͒l�𓾂�
        // ���ꂼ��+��-�̒l�Ɠ��͂̊֘A�t����Input Manager�Őݒ肳��Ă���
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        if (Input.anyKeyDown)
        {
            Vector2? action = this.actionKeyDown();
            if (action.HasValue)
            {
                // �L�[���͂������Animator��state���Z�b�g����
                setStateToAnimator(vector: action.Value);
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Debug.Log("�L�[����");
            if (IsInteract())
            {
                result.Interact();
            }
        }
        
        Vector2 vector = new Vector2(
           (int)Input.GetAxis("Horizontal"),
           (int)Input.GetAxis("Vertical"));
        setStateToAnimator(vector: vector.magnitude >= 0.1 ? vector : null);
    }

    private void FixedUpdate()
    {
        // ���x��������
        rigidBody.MovePosition(rigidBody.position + inputAxis.normalized * SPEED  / 25f);
    }

    public void Footprint_L()
    {
        footprint_L.Emit(1);
    }

    public void Footprint_R()
    {
        footprint_R.Emit(1);
    }

    private void setStateToAnimator(Vector2? vector)
    {
        if (!vector.HasValue)
        {
            this.animator.speed = 0.0f;
            return;
        }
        this.animator.SetFloat("x", vector.Value.x);
        this.animator.SetFloat("y", vector.Value.y);
        this.animator.speed = 1.0f;

        var angle = Mathf.Atan2(-1f * vector.Value.y, vector.Value.x);
        footprint_L.startRotation = angle + 0.5f * Mathf.PI;
        footprint_R.startRotation = angle + 0.5f * Mathf.PI;
        footprintGenerator.eulerAngles = new(0, 0, -1 * (angle * Mathf.Rad2Deg + 90)); //�Ȃ�����œ����̂��킩��Ȃ�

    }

    private Vector2? actionKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }
}