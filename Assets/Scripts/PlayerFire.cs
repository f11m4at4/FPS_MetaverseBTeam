using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 발사버튼을 누르면 총알을 발사하고 싶다.
// 필요속성 : 총알공장, 총구
public class PlayerFire : MonoBehaviour
{
    // 필요속성 : 총알공장, 총구
    public GameObject bulletFactory;
    public Transform firePosition;
    // 파편효과
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
        // 사용자가 발사버튼을 누르면 총알을 발사하고 싶다.
        // 1. 사용자가 발사버튼을 눌렀으니까
        if (Input.GetButtonDown("Fire1"))
        {
            bulletAudio.Stop();
            bulletAudio.Play();
            ShootRay();
        }
    }

    void ShootRay()
    {
        // Ray 를 이용해서 총알 발사
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        // Ray 를 발사해서 만약 부딪혔다면
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 부딪힌 지점에 파편 튀게 하고싶다.
            // 1. 부딪힌 지점으로 이동시키기
            bulletImpact.position = hitInfo.point;
            // 파편이 튀는 방향을 부딪힌 지점이 향하는 방향과
            // 일치시켜주자.
            bulletImpact.forward = hitInfo.normal;
            // 2. 파편효과 재생하기
            bulletPS.Stop();
            bulletPS.Play();
        }
    }

    private void ShootBullet()
    {
        // 2. 총알이 있어야 한다.
        GameObject bullet = Instantiate(bulletFactory);
        // 3. 총알을 발사하고 싶다.
        bullet.transform.position = firePosition.position;
        bullet.transform.forward = firePosition.forward;
    }
}
