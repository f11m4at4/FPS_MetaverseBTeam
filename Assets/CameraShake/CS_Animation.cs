using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Animation : CameraShakeBase
{
    public override void Play(Transform transform, CameraShakeInfo info)
    {
        transform.GetComponent<Animation>().Stop();
        // animation ������Ʈ ���
        transform.GetComponent<Animation>().Play(PlayMode.StopAll);
    }

    public override void Stop(Transform transform)
    {
        // animation ������Ʈ ����
        transform.GetComponent<Animation>().Stop();
    }
}