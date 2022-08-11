using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CamPos 를 따라서 이동하고 싶다.
// 필요속성 : CamPos, 이동속도
public class CamFollow : MonoBehaviour
{
    // 필요속성 : CamPos, 이동속도
    public Transform campos;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // CamPos 를 따라서 이동하고 싶다.
        transform.position = Vector3.Lerp(transform.position, campos.position, speed * Time.deltaTime);
        // 회전 적용
        //transform.forward = Vector3.Lerp(transform.forward, campos.forward, speed * Time.deltaTime);
    }
}
