using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Sine : CameraShakeBase
{
    float theta = 0;

    public override void Play(Transform transform, CameraShakeInfo info)
    {
        theta += info.sinSpeed * Time.deltaTime;
        // sine �Լ��� �̿��ؼ� ���Ʒ��� �̵���Ű�� �ʹ�.
        transform.position = originPos + Vector3.up * Mathf.Sin(theta) * info.amplitude;
    }

    public override void Stop(Transform transform)
    {
        transform.position = originPos;
        theta = 0;
    }
}