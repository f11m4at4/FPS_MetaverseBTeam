using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������� ü���� �����Ѵ�.
// �ʿ�Ӽ�: ü��, UI
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    // �ʿ�Ӽ�: ü��, UI
    int hp = 3;
    public Image damageUI;
    // ������� ü���� �����Ѵ�.
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            // �����ġ �Ѵ� ����
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
        // �����ġ�� ���۵ƴٸ�
        if (damageUI.enabled)
        {
            // -> UI �� �����ð� Ȱ��ȭ ������� �������ϰ�ʹ�.
            currentTime += Time.deltaTime;
            if(currentTime > blinkTime)
            {
                currentTime = 0;
                damageUI.enabled = false;
            }
        }
    }

}
