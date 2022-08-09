using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 사용자의 체력을 관리한다.
// 필요속성: 체력, UI
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    // 필요속성: 체력, UI
    int hp = 3;
    public Image damageUI;
    // 사용자의 체력을 관리한다.
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            // 스톱와치 켜는 동작
            damageUI.enabled = true;
        }
    }

    public float blinkTime = 0.205f;
    float currentTime;

    public static PlayerHealth Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 스톱와치가 시작됐다면
        if (damageUI.enabled)
        {
            // -> UI 를 일정시간 활성화 시켜줬다 끄도록하고싶다.
            currentTime += Time.deltaTime;
            if(currentTime > blinkTime)
            {
                currentTime = 0;
                damageUI.enabled = false;
            }
        }
    }

}
