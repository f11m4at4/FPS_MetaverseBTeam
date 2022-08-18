using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶����ũ�� ���� ���� ������ ����� ����ü
[System.Serializable]
public struct CameraShakeInfo
{
    // ����
    public float amplitude;
    // ������ ���� ������
    public float sinSpeed;
}

// ��� ī�޶����ũ Ŭ������ �θ�Ŭ����
public class CameraShakeBase
{
    // ī�޶� �ʱ� ��ġ ��ﺯ��
    protected Vector3 originPos;

    // ī�޶����ũ �ʱ�ȭ �Լ�
    public virtual void Init(Vector3 originPos)
    {
        this.originPos = originPos;
    }

    // ī�޶����ũ ��� �Լ�
    // transform : ī�޶��� Ʈ������
    // info : ����ڰ� ������ ī�޶����ũ ��������
    public virtual void Play(Transform transform, CameraShakeInfo info) { }

    // ī�޶����ũ ���� �Լ�
    // transform : ī�޶��� Ʈ������
    public virtual void Stop(Transform transform) { }
}
