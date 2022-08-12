using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿����� �յ��¿� �̵��ϰ� �ʹ�.
// �ʿ�Ӽ� : �̵��ӵ�
// �߷��� �����Ű�� �ʹ�.
// �ʿ�Ӽ� : �߷�, �����ӵ�
// ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
// �ʿ�Ӽ� : �����Ŀ�
public class PlayerMove : MonoBehaviour
{
    // �ʿ�Ӽ� : �̵��ӵ�
    public float speed = 5;

    CharacterController cc;
    // �ʿ�Ӽ� : �߷�, �����ӵ�
    public float gravity = -20;
    float yVelocity = 0;
    // �ʿ�Ӽ� : �����Ŀ�
    public float jumpPower = 5;

    // ���������� ���� ���
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
        // ������� �Է¿����� �յ��¿� �̵��ϰ� �ʹ�.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        anim.SetFloat("Direction", h);
        anim.SetFloat("Speed", v);

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        // v = v0 + at
        yVelocity += gravity * Time.deltaTime;

        // �ٴڿ� 10 cm ������ �ִٸ� ������ �Ǵ�����
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        int layer = 1 << gameObject.layer;
        if(Physics.Raycast(ray, out hitInfo, 0.1f, ~layer)==false)
        {
            // ���߿� �ִ�.
            anim.SetBool("IsInAir", true);
        }
        // ���� �ٴڿ� ��� �ִٸ�
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // -> �����ӵ��� 0 ���� ����
            yVelocity = 0;
            jumpCount = 0;
            //isJumping = false;
            anim.SetBool("IsInAir", false);
        }

        // ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
        if (jumpCount < jumpCountMax && Input.GetButtonDown("Jump"))
        {
            // -> �����ӵ��� ��ȭ�� �ְ�ʹ�.
            yVelocity = jumpPower;
            jumpCount++;
            //isJumping = true;
            anim.SetBool("IsInAir", true);
        }

        dir.y = yVelocity;
        // P = P0 + vt
        // �̵��Ѵ�.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
