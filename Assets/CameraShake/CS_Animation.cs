using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Animation : CameraShakeBase
{
    public override void Play(Transform transform, CameraShakeInfo info)
    {
        transform.GetComponent<Animation>().Stop();
        // animation 컴포넌트 재생
        transform.GetComponent<Animation>().Play(PlayMode.StopAll);
    }

    public override void Stop(Transform transform)
    {
        // animation 컴포넌트 정지
        transform.GetComponent<Animation>().Stop();
    }
}