using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿����� �յ��¿� �̵��ϰ� �ʹ�.
// �ʿ�Ӽ� : �̵��ӵ�
public class PlayerMove : MonoBehaviour
{
    // �ʿ�Ӽ� : �̵��ӵ�
    public float speed = 5;

    CharacterController cc;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������� �Է¿����� �յ��¿� �̵��ϰ� �ʹ�.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        // P = P0 + vt
        cc.Move(dir * speed * Time.deltaTime);
    }
}
