using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라셰이크를 수행할 클래스
public class CameraShake : MonoBehaviour
{
    // 카메라 트랜스폼
    public Transform targetCamera;
    // 재생 시간
    public float playTime = 0.1f;

    public enum CameraShakeType
    {
        Random,
        Sine,
        Animation
    }

    public CameraShakeType cameraShakeType = CameraShakeType.Random;

    CameraShakeBase cameraShake;

    // 사용자가 수정할 카메라셰이크를 위한 설정정보
    [SerializeField]
    CameraShakeInfo info;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = CreateCameraShake(cameraShakeType);
        
    }

    public static CameraShakeBase CreateCameraShake(CameraShakeType type)
    {
        switch(type)
        {
            case CameraShakeType.Random:
                return new CS_Random();
            case CameraShakeType.Sine:
                return new CS_Sine();
            case CameraShakeType.Animation:
                break;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayCameraShake();
        }
    }

    void PlayCameraShake()
    {
        // 카메라셰이크 종류에 따라 실행 방법이 다르다.
        if(cameraShakeType != CameraShakeType.Animation)
        {
            StopAllCoroutines();
            StartCoroutine(Play());
        }
    }

    // 재생 시간동안 카메라셰이크 실행
    IEnumerator Play()
    {
        // 카메라셰이크 초기화
        cameraShake.Init(targetCamera.position);

        float currentTime = 0;
        // 재생 동안 카메라셰이크 클래스의 Play()함수 호출
        while(currentTime < playTime)
        {
            currentTime += Time.deltaTime;
            //카메라셰이크 클래스의 Play()함수 호출
            cameraShake.Play(targetCamera, info);
            yield return null;
        }
        // 재생이 끝나면 Stop() 호출
        cameraShake.Stop(targetCamera);
    }
}
