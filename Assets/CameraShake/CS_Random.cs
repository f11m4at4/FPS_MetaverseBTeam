using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Random : CameraShakeBase
{
    public override void Play(Transform transform, CameraShakeInfo info)
    {
        transform.position = originPos + Random.insideUnitSphere * info.amplitude * Time.deltaTime;
    }

    public override void Stop(Transform transform)
    {
        transform.position = originPos;
    }
}