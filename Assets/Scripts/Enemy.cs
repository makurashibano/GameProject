using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyState enemyState; 

    //playerの数を入れる配列
    GameObject[] targets;
    //playerの中で一番近いオブジェクト
    GameObject target;

    [SerializeField]
    float distancePoint = 3.0f;
    [SerializeField]
    private float gravitySpeed = 14f;
    //ノックバックする力
    public float boundsPower = 5.0f;
    Vector3 boundVec = new Vector3(0f,0f,0f);

    NavMeshAgent navmesh;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        navmesh = GetComponent<NavMeshAgent>();
        //シーン内にあるPlayerオブジェクトを配列に入れる
        targets = GameObject.FindGameObjectsWithTag("Player");
        
    }

    //一番近いオブジェクトを探す関数
    private void  FetchNearObjectWithTag(string tagName)
    {
        // 該当タグが1つしか無い場合はそれを返す
        targets = GameObject.FindGameObjectsWithTag(tagName);
        var minTargetDistance = float.MaxValue;
        for(int i = 0; i<targets.Length;i++)
        {
            //オブジェクトが自身の場合計測しない
            if (this.gameObject == targets[i]) continue;
            // 前回計測したオブジェクトよりも近くにあれば記録
            var targetDistance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (!(targetDistance < minTargetDistance)) continue;
            minTargetDistance = targetDistance;
            //一番近いオブジェクトを代入
            target = targets[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0, 0);
        //一番近いキャラクターを取得
        FetchNearObjectWithTag("Player");
        //ステートを取得して行動を決める
        switch (enemyState.aiState)
        {
            case EnemyState.EnemyAiState.WAIT:
                WaitStart();
                break;
            case EnemyState.EnemyAiState.MOVE:
                MoveStart();
                break;
            case EnemyState.EnemyAiState.ATTACK:
                AttackStart();
                break;
            case EnemyState.EnemyAiState.Fall:
                FallStart();
                break;
        }
        //攻撃する前に止まる
        if (enemyState.aiState ==EnemyState.EnemyAiState.ATTACK)
        {
            navmesh.speed = 0f;
        }
        else
        {
            navmesh.speed = 2f;
        }
        //一番近いキャラクターとの距離を取得
        float distance = (transform.position - target.transform.position).magnitude;
        //落下したらステートを落下にする
        if (enemyState.aiState == EnemyState.EnemyAiState.Fall)
        {           
            return;
        }
        //一定の距離近づいたらステートを攻撃にそれ以外を移動にする
        if(distance < distancePoint)
        {
            enemyState.aiState = EnemyState.EnemyAiState.ATTACK;
        }
        else
        {
            enemyState.aiState = EnemyState.EnemyAiState.MOVE;
        }

        
    }
    //移動関数
    void Move()
    {
        //playerをNavmeshを使って追いかける
        navmesh.destination = target.transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Finish")
        {
            enemyState.aiState = EnemyState.EnemyAiState.Fall;
            navmesh.enabled = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Push")
        {
            KnockbackDirection(other);
            StartCoroutine("Knock");
        }
    }

    IEnumerator Knock()
    {
        int i = 0;
        while(true)
        {
            Debug.Log(boundVec);
            i++;
            rb.AddForce(boundVec * boundsPower, ForceMode.Impulse);
            yield return null;
            if (i == 100)
            {
                yield break;
            }

        }
        

    }

    //ノックバックする方向を取得する関数
    void KnockbackDirection(Collision other)
    {
        rb.velocity = Vector3.zero;
        // 衝突位置を取得する
        Vector3 hitPos = other.contacts[0].point;

        // 衝突位置から自機へ向かうベクトルを求める
        boundVec = (this.transform.position - hitPos).normalized;
        boundVec.y = 0f;           
    }
    //立ち止まる関数
    void WaitStart()
    {

    }
    //動く関数
    void MoveStart()
    {
        //移動
        Move();
        
    }
    //攻撃関数
    void AttackStart()
    {
        
    }
    void FallStart()
    {        
        rb.velocity -= new Vector3(0, gravitySpeed, 0);
    }
}
