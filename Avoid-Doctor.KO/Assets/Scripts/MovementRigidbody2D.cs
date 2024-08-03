using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementRigidbody2D : MonoBehaviour
{
    [Header("Move Horizontal")]
    [SerializeField] private float moveSpeed = 8;

    [Header("Move Vertical (Jump)")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float lowGravity = 2; // ����Ű�� ���� ������ ���� �� ����Ǵ� ���� �߷�
    [SerializeField] private float hightGravity = 3; // �Ϲ������� ����Ǵ� ���� �߷� ���
    [SerializeField] private int maxJumpCount = 2;
    private int currentJumpCount;

    [Header("Collision")]
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private Vector2 footPosition; // �� ��ġ
    private Vector2 footArea; // �� ����

    private Rigidbody2D rigid2D;
    private new Collider2D collider2D;

    public bool IsLongJump { set; get; } = false;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        // �÷��̾� ������Ʈ�� Collider2D min, center, max ��ġ ����
        Bounds bounds = collider2D.bounds;
        //�÷��̾��� �� ��ġ ����
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // �ÿ��̾��� �� �ν� ���� ����
        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);
        isGrounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);

        if (isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }


        // ���� ����, ���� ���� ������ ���� �߷� ���(gravityScale) ����(Jump Up�� ���� ����)
        // �߷� ����� ���� if���� ���� ������ �ǰ�, �߷� ����� ���� else���� ���� ������ �ȴ�.
        if (IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravity;
        }
        else
        {
            rigid2D.gravityScale = hightGravity;
        }
    }
    private void LateUpdate()
    {
        float x = Mathf.Clamp(transform.position.x, Constants.min.x, Constants.max.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void MoveTo(float x)
    {
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }
    public bool JumpTo()
    {
        if (currentJumpCount > 0)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
            currentJumpCount--;

            return true;

        }
        return false;
    }
}
