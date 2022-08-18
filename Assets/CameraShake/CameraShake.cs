using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶����ũ�� ������ Ŭ����
public class CameraShake : MonoBehaviour
{
    // ī�޶� Ʈ������
    public Transform targetCamera;
    // ��� �ð�
    public float playTime = 0.1f;

    public enum CameraShakeType
    {
        Random,
        Sine,
        Animation
    }

    public CameraShakeType cameraShakeType = CameraShakeType.Random;

    CameraShakeBase cameraShake;

    // ����ڰ� ������ ī�޶����ũ�� ���� ��������
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
        // ī�޶����ũ ������ ���� ���� ����� �ٸ���.
        if(cameraShakeType != CameraShakeType.Animation)
        {
            StopAllCoroutines();
            StartCoroutine(Play());
        }
    }

    // ��� �ð����� ī�޶����ũ ����
    IEnumerator Play()
    {
        // ī�޶����ũ �ʱ�ȭ
        cameraShake.Init(targetCamera.position);

        float currentTime = 0;
        // ��� ���� ī�޶����ũ Ŭ������ Play()�Լ� ȣ��
        while(currentTime < playTime)
        {
            currentTime += Time.deltaTime;
            //ī�޶����ũ Ŭ������ Play()�Լ� ȣ��
            cameraShake.Play(targetCamera, info);
            yield return null;
        }
        // ����� ������ Stop() ȣ��
        cameraShake.Stop(targetCamera);
    }
}
