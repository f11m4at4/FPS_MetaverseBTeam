using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CamPos �� ���� �̵��ϰ� �ʹ�.
// �ʿ�Ӽ� : CamPos, �̵��ӵ�
public class CamFollow : MonoBehaviour
{
    // �ʿ�Ӽ� : CamPos, �̵��ӵ�
    public Transform campos;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // CamPos �� ���� �̵��ϰ� �ʹ�.
        transform.position = Vector3.Lerp(transform.position, campos.position, speed * Time.deltaTime);
        // ȸ�� ����
        //transform.forward = Vector3.Lerp(transform.forward, campos.forward, speed * Time.deltaTime);
    }
}
