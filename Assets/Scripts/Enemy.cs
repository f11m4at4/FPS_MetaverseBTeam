using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���� ������ ���¸� ������ �ϰ� �ʹ�.
// �ʿ�Ӽ� : ��������
// �ִϸ��̼��� ���¸� Move �� ��ȯ�ϰ� �ʹ�.
// �ʿ�Ӽ� : Animator
//�״� �ִϸ��̼��� ������ ���� �ٴ����� ��������� �ϰ�ʹ�.
public class Enemy : MonoBehaviour
{
    #region Enemy �Ӽ�����
    // �ʿ�Ӽ� : ��������
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    };
    EnemyState m_State = EnemyState.Idle;
    #endregion

    #region Idle �Ӽ�
    // �ʿ�Ӽ� : ���ð�, ����ð�
    public float idleDelayTime = 2;
    float currentTime = 0;
    #endregion

    #region Move �Ӽ�
    // �ʿ�Ӽ� : �̵��ӵ�, Ÿ��, CharacterController
    public float speed = 5;
    public Transform target;
    CharacterController cc;
    #endregion

    // �ʿ�Ӽ� : Animator
    Animator anim;

    // Path Finding �� ���� �׺���̼�
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        print("state : " + m_State);
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damage:
                //Damage();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    // �����ð��� �帣�� ���¸� �̵����� �ٲٰ� �ʹ�.
    
    private void Idle()
    {
        // �����ð��� �帣�� ���¸� �̵����� �ٲٰ� �ʹ�.
        // 1. �ð��� �귶���ϱ�
        currentTime += Time.deltaTime;
        // 2. �ð��� �����ϱ�
        if (currentTime > idleDelayTime)
        {
            // 3. ���¸� �̵����� ��ȯ
            m_State = EnemyState.Move;
            currentTime = 0;
            // �ִϸ��̼��� ���¸� Move �� ��ȯ�ϰ� �ʹ�.
            anim.SetTrigger("Move");
            agent.enabled = true;
        }
    }

    // Ÿ�������� �̵��ϰ� �ʹ�.
    // Ÿ�������� ȸ���ϰ� �ʹ�.
    // ���ݹ����� Ÿ���� ������ ���¸� �������� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : ���ݹ���
    public float attackRange = 2;

    // Ÿ���� �̵������� ���� �ۿ� ������ ��Ʈ�� �ϰ� �ʹ�.
    // �ʿ�Ӽ� : �̵������� ����
    public float moveToTargetRange = 5;
    Vector3 randPos;
    bool canMove = false;
    private void Move()
    {
        // Ÿ�������� �̵��ϰ� �ʹ�.
        // 1. ������ �ʿ�
        // -> target - me
        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        dir.y = 0;

        // ���� ���ϴ� ����
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5, Color.red);
        // Ÿ�������� ���ϴ� ����
        Debug.DrawLine(transform.position, transform.position + dir * 5, Color.yellow);

        float dot = Vector3.Dot(transform.forward, dir);
        
        // �̵����� �ȿ� �ִٸ� �׸��� �þ߹��� �ȿ� ���� ��
        if (true)//distance < moveToTargetRange && dot > 0)
        {
            // -> Ÿ�������� �̵�
            agent.destination = target.position;
        }
        // �׷��� ������
        // Ÿ���� �̵������� ���� �ۿ� ������ ��Ʈ�� �ϰ� �ʹ�.
        else
        {
            // ��Ʈ�� �ϰ� �ʹ�.

            // ���� ���� �̵��� ���� ��ã�Ҵٸ�
            if (canMove == false)
            {
                // -> �̵��� ���� ã��.
                canMove = GetRandomPosition(transform.position, out randPos, 10);
            }
            // ���� ã�Ҵٸ�
            // -> �׷��� �ʴٸ�
            else
            {
                // �̵�
                agent.destination = randPos;
                // ���� �������� ���� �����ߴٸ�
                if (Vector3.Distance(randPos, transform.position) < 0.1f)
                {
                    // �ٽ� ã�ƾ� �Ѵٰ� �˷��ֱ�
                    canMove = false;
                }
            }
        }
        // Ÿ�������� ȸ���ϰ� �ʹ�.
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        // 2. �̵��ϰ� �ʹ�.
        //cc.SimpleMove(dir * speed);
        // ���ݹ����� Ÿ���� ������ ���¸� �������� ��ȯ�ϰ� �ʹ�.
        if(distance < attackRange)
        {
            m_State = EnemyState.Attack;
            currentTime = attackDelayTime;
            agent.enabled = false;
        }
    }

    private bool GetRandomPosition(Vector3 position, out Vector3 randPos, float range = 3)
    {
        // ������ ��ġã��
        Vector3 center = Random.insideUnitSphere * range;
        center.y = 0;
        center += position;
        NavMeshHit hitInfo;
        // ���� ��ġ�κ��� ������ �� �ִ��� ���� ����
        bool result = NavMesh.SamplePosition(center, out hitInfo, range, 1);
        // randPos �� ��ġ �Ҵ�
        randPos = hitInfo.position;
        // ���� ���� ��ȯ
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // �����ð��� �ѹ��� �����ϰ� �ʹ�.
    // �ʿ�Ӽ� : ���ݴ��ð�
    // Ÿ���� ���ݹ����� ����� ���¸� �̵����� ��ȯ�ϰ� �ʹ�.
    public float attackDelayTime = 2;
    private void Attack()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        currentTime += Time.deltaTime;
        if(currentTime > attackDelayTime)
        {
            currentTime = 0;
            print("attack!!!");
            anim.SetTrigger("Attack");
        }
        // Ÿ���� ���ݹ����� ����� ���¸� �̵����� ��ȯ�ϰ� �ʹ�.
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > attackRange)
        {
            m_State = EnemyState.Move;
            anim.SetTrigger("Move");
            agent.enabled = true;
        }
    }

    // �ǰݴ����� �� ȣ��Ǵ� �̺�Ʈ �Լ�
    // 3 �� ������ �׵��� ó������
    public int hp = 3;
    public float damageSpeed = 0.5f;
    Vector3 knockBackPos;
    public void OnDamageProcess(Vector3 shootDirection)
    {
        agent.enabled = false;

        StopAllCoroutines();
        // 3 �� ������
        hp--;
        if (hp <= 0)
        {
            // �׵��� ó������
            m_State = EnemyState.Die;
            // �浹ü ��������
            cc.enabled = false;
            anim.SetTrigger("Die");

        }
        else
        {
            m_State = EnemyState.Damage;
            // �ִϸ��̼� ���� ��ȯ
            anim.SetTrigger("Damage");
            //transform.position += shootDirection * damageSpeed;
            //cc.Move(shootDirection * damageSpeed);
            shootDirection.y = 0;
            knockBackPos = transform.position + shootDirection * damageSpeed;
            StartCoroutine(Damage());
        }
        currentTime = 0;
    }

    // �����ð� ��ٷȴٰ� ���¸� Idle �� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : �ǰݴ��ð�
    public float damageDelayTime = 2;
    private IEnumerator Damage()
    {
        float damageEndCheckTime = 0;
        // ���ϴ� ��ġ�� ��� �̵��ϰ� �ʹ�.
        // �ð��� 2�ʰ� ���� �ȵƴٸ�
        while (damageEndCheckTime < 2)
        {
            // �ð��� �귯�� �Ѵ�.
            damageEndCheckTime += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, knockBackPos, 15 * Time.deltaTime);

            // �ڷ�ƾ �����Ű��
            //yield break;
            // delta Time ��ŭ ��ٸ���
            yield return null;
        }
        transform.position = knockBackPos;

        m_State = EnemyState.Idle;
        //currentTime += Time.deltaTime;
        //if (currentTime > damageDelayTime)
        //{
        //    currentTime = 0;
        //    m_State = EnemyState.Idle;
        //}
    }

    public float dieSpeed = 0.5f;
    private void Die()
    {
        //�״� �ִϸ��̼��� ������ ���� �ٴ����� ��������� �ϰ�ʹ�.
        currentTime += Time.deltaTime;
        if(currentTime > 2)
        {
            // �Ʒ��� ��������� ����.
            transform.position += Vector3.down * dieSpeed * Time.deltaTime;
            // ������ ������� ���� -2
            if (transform.position.y < -3)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
