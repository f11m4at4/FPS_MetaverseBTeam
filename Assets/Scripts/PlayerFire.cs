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

    Animator anim;
    void Start()
    {
        bulletPS = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� �߻��ư�� ������ �Ѿ��� �߻��ϰ� �ʹ�.
        // 1. ����ڰ� �߻��ư�� �������ϱ�
        if (Input.GetButtonDown("Fire1"))
        {
            //anim.SetTrigger("Attack");
            //anim.Play("Attack");
            anim.CrossFade("Attack", 1, 1, 0.1f);
            bulletAudio.Stop();
            bulletAudio.Play();
            ShootRay();
        }
    }

    void ShootRay()
    {
        // Ray �� �̿��ؼ� �Ѿ� �߻�
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ư�� ��ü�� �浹���� �ʵ��� �ϰ� �ʹ�.
        int layer = 1 << gameObject.layer;

        RaycastHit hitInfo;
        // Ray �� �߻��ؼ� ���� �ε����ٸ�
        if (Physics.Raycast(ray, out hitInfo, 1000, ~layer))
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

            // �ε��� �༮�� Enemy ��� �ǰ� �̺�Ʈ ȣ������
            Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
            if(enemy)
            {
                enemy.OnDamageProcess(ray.direction);
            }
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
