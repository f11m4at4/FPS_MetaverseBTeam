using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에따라 앞뒤좌우 이동하고 싶다.
// 필요속성 : 이동속도
// 중력을 적용시키고 싶다.
// 필요속성 : 중력, 수직속도
// 사용자가 점프버튼을 누르면 점프하고 싶다.
// 필요속성 : 점프파워
public class PlayerMove : MonoBehaviour
{
    // 필요속성 : 이동속도
    public float speed = 5;

    CharacterController cc;
    // 필요속성 : 중력, 수직속도
    public float gravity = -20;
    float yVelocity = 0;
    // 필요속성 : 점프파워
    public float jumpPower = 5;

    // 점프중인지 여부 기억
    bool isJumping = false;
    int jumpCount = 0;
    public int jumpCountMax = 2;

    Animator anim;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 입력에따라 앞뒤좌우 이동하고 싶다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        anim.SetFloat("Direction", h);
        anim.SetFloat("Speed", v);

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        // v = v0 + at
        yVelocity += gravity * Time.deltaTime;

        // 바닥에 10 cm 떨어져 있다면 점프로 판단하자
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        int layer = 1 << gameObject.layer;
        if(Physics.Raycast(ray, out hitInfo, 0.1f, ~layer)==false)
        {
            // 공중에 있다.
            anim.SetBool("IsInAir", true);
        }
        // 만약 바닥에 닿아 있다면
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // -> 수직속도를 0 으로 하자
            yVelocity = 0;
            jumpCount = 0;
            //isJumping = false;
            anim.SetBool("IsInAir", false);
        }

        // 사용자가 점프버튼을 누르면 점프하고 싶다.
        if (jumpCount < jumpCountMax && Input.GetButtonDown("Jump"))
        {
            // -> 수직속도에 변화를 주고싶다.
            yVelocity = jumpPower;
            jumpCount++;
            //isJumping = true;
            anim.SetBool("IsInAir", true);
        }

        dir.y = yVelocity;
        // P = P0 + vt
        // 이동한다.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
