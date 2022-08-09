using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventClass : MonoBehaviour
{
    void OnAttack()
    {
        // Player 의 hp 를 줄여주자
        PlayerHealth.Instance.HP--;
    }
}
