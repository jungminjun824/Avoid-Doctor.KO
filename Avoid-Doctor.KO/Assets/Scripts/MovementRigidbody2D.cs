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
    [SerializeField] private float lowGravity = 2; // 점프키를 오래 누르고 있을 때 적용되는 낮은 중력
    [SerializeField] private float hightGravity = 3; // 일반적으로 적용되는 높은 중력 계수
    [SerializeField] private int maxJumpCount = 2;
    private int currentJumpCount;

    [Header("Collision")]
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private Vector2 footPosition; // 발 위치
    private Vector2 footArea; // 발 범위

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
        // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보
        Bounds bounds = collider2D.bounds;
        //플레이어의 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // 플에이어의 발 인식 범위 설정
        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);
        isGrounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);

        if (isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }


        // 낮은 점프, 높은 점프 구현을 위한 중력 계수(gravityScale) 조절(Jump Up일 때만 적용)
        // 중력 계수가 낮은 if문은 높은 점프가 되고, 중력 계수가 높은 else문은 낮은 점프가 된다.
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
