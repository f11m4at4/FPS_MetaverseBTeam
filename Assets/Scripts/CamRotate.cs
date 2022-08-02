using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿����� ��ü�� �����¿�� ȸ����Ű�� �ʹ�.
// �ʿ�Ӽ� : ȸ���ӵ�
public class CamRotate : MonoBehaviour
{
    // �ʿ�Ӽ� : ȸ���ӵ�
    public float rotSpeed = 205;

    float mx;
    float my;

    void Start()
    {
        // ������ �� eulerangles �� ���� mx, my �� �Ҵ�
        mx = transform.eulerAngles.y;
        my = -transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ������� �Է¿����� ��ü�� �����¿�� ȸ����Ű�� �ʹ�.
        // 1. ������� �Է¿�����
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        // 2. ������ �ʿ�
        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;
        // -60 ~ 60 ���� ������ ����
        //my = Mathf.Clamp(my, -60, 60);
        // 3. ȸ����Ű�� �ʹ�.
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
