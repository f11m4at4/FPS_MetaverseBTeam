using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라셰이크를 위한 설정 정보를 기억할 구조체
[System.Serializable]
public struct CameraShakeInfo
{
    // 진폭
    public float amplitude;
    // 사인파 진동 빠르기
    public float sinSpeed;
}

// 모든 카메라셰이크 클래스의 부모클래스
public class CameraShakeBase
{
    // 카메라 초기 위치 기억변수
    protected Vector3 originPos;

    // 카메라셰이크 초기화 함수
    public virtual void Init(Vector3 originPos)
    {
        this.originPos = originPos;
    }

    // 카메라셰이크 재생 함수
    // transform : 카메라의 트랜스폼
    // info : 사용자가 정의한 카메라셰이크 설정정보
    public virtual void Play(Transform transform, CameraShakeInfo info) { }

    // 카메라셰이크 정지 함수
    // transform : 카메라의 트랜스폼
    public virtual void Stop(Transform transform) { }
}
