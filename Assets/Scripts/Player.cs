using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * �L�����̍��W��ύX����Controller
 */
public class Player : MonoBehaviour
{
    public const int MAX_HP = 200;
    public float SPEED = 1f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    Animator animator;
    [SerializeField] Transform footprintGenerator;
    [SerializeField] ParticleSystem footprint_L;
    [SerializeField] ParticleSystem footprint_R;
    [SerializeField] Transform pivot;

    [SerializeField] Collider2D interactingCollider;
    Collider2D[] results = new Collider2D[8];
    Interaction result;

    [SerializeField] Inventory inventory;

    public static bool isInteracting = false;
    public static bool isInventoryOpening = false;
    public static bool canMove = true;
    public static Transform playerTransform;
    public static Player instance;

    [SerializeField] Slider HPBar;
    [SerializeField] TextMeshProUGUI HPText;
    private double hp;
    public double HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                Debug.Log("GameOver");
            }
            if (hp >= MAX_HP)
            {
                hp = MAX_HP;
            }
            HPBar.value = (int)Math.Ceiling(hp);
            HPText.text = $"HP {(int)Math.Ceiling(hp)} / {MAX_HP}";
            
        }
    }

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
        playerTransform = transform;
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        instance = this;
        HP = MAX_HP;
    }

    void Update()
    {
        
        if (canMove)
        {
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
                if (!isInteracting && !isInventoryOpening)
                {
                    if (IsInteract())
                    {
                        result.Interact();
                        isInteracting = true;
                        canMove = false;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isInteracting && !isInventoryOpening)
                {
                    OpenInventory();
                }
            }

            Vector2 vector = new Vector2(
               (int)Input.GetAxis("Horizontal"),
               (int)Input.GetAxis("Vertical"));
            setStateToAnimator(vector: vector.magnitude >= 0.1 ? vector : null);
        }
        else
        {
            inputAxis.x = 0f;
            inputAxis.y = 0f;
            setStateToAnimator(null);
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (isInventoryOpening)
                {
                    inventory.Close();
                }
            }

        }
    }

    private void FixedUpdate()
    {
        // ���x��������
        rigidBody.MovePosition(rigidBody.position + inputAxis.normalized * SPEED  / 25f);
    }

    public void OpenInventory()
    {
        isInventoryOpening = true;
        canMove = false;
        inventory.Open();
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
        this.animator.speed = (SPEED + 1.0f) / 2f;

        var angle = Mathf.Atan2(-1f * vector.Value.y, vector.Value.x);
        footprint_L.startRotation = angle + 0.5f * Mathf.PI;
        footprint_R.startRotation = angle + 0.5f * Mathf.PI;
        footprintGenerator.eulerAngles = new(0, 0, -1 * (angle * Mathf.Rad2Deg + 90)); //�Ȃ�����œ����̂��킩��Ȃ�
        pivot.eulerAngles = new(0, 0, -1 * (angle * Mathf.Rad2Deg + 90));
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