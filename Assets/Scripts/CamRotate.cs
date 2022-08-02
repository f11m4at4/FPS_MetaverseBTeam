using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에따라 물체를 상하좌우로 회전시키고 싶다.
// 필요속성 : 회전속도
public class CamRotate : MonoBehaviour
{
    // 필요속성 : 회전속도
    public float rotSpeed = 205;

    float mx;
    float my;

    void Start()
    {
        // 시작할 때 eulerangles 의 값을 mx, my 에 할당
        mx = transform.eulerAngles.y;
        my = -transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 입력에따라 물체를 상하좌우로 회전시키고 싶다.
        // 1. 사용자의 입력에따라
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        // 2. 방향이 필요
        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;
        // -60 ~ 60 각도 제한을 주자
        //my = Mathf.Clamp(my, -60, 60);
        // 3. 회전시키고 싶다.
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
