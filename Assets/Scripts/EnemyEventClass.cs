using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventClass : MonoBehaviour
{
    void OnAttack()
    {
        // Player �� hp �� �ٿ�����
        PlayerHealth.Instance.HP--;
    }
}
