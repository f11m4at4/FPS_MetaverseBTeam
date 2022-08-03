using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� �߻��ư�� ������ �Ѿ��� �߻��ϰ� �ʹ�.
// �ʿ�Ӽ� : �Ѿ˰���, �ѱ�
public class PlayerFire : MonoBehaviour
{
    // �ʿ�Ӽ� : �Ѿ˰���, �ѱ�
    public GameObject bulletFactory;
    public Transform firePosition;
    // ����ȿ��
    public Transform bulletImpact;
    ParticleSystem bulletPS;
    AudioSource bulletAudio;
    void Start()
    {
        bulletPS = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� �߻��ư�� ������ �Ѿ��� �߻��ϰ� �ʹ�.
        // 1. ����ڰ� �߻��ư�� �������ϱ�
        if (Input.GetButtonDown("Fire1"))
        {
            bulletAudio.Stop();
            bulletAudio.Play();
            ShootRay();
        }
    }

    void ShootRay()
    {
        // Ray �� �̿��ؼ� �Ѿ� �߻�
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        // Ray �� �߻��ؼ� ���� �ε����ٸ�
        if (Physics.Raycast(ray, out hitInfo))
        {
            // �ε��� ������ ���� Ƣ�� �ϰ�ʹ�.
            // 1. �ε��� �������� �̵���Ű��
            bulletImpact.position = hitInfo.point;
            // ������ Ƣ�� ������ �ε��� ������ ���ϴ� �����
            // ��ġ��������.
            bulletImpact.forward = hitInfo.normal;
            // 2. ����ȿ�� ����ϱ�
            bulletPS.Stop();
            bulletPS.Play();
        }
    }

    private void ShootBullet()
    {
        // 2. �Ѿ��� �־�� �Ѵ�.
        GameObject bullet = Instantiate(bulletFactory);
        // 3. �Ѿ��� �߻��ϰ� �ʹ�.
        bullet.transform.position = firePosition.position;
        bullet.transform.forward = firePosition.forward;
    }
}
