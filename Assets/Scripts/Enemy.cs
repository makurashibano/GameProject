using System.Collections;
using System.Collections.Generic;
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

    bool isDmage = false;

    [SerializeField]
    float distancePoint = 3.0f;
    //ノックバックする力
    public float boundsPower = 10.0f;
    //ノックバックする位置
    Vector3 forceDir = new Vector3(0f, 0f, 0f);
    bool isAttack = false;
    bool isFall = false;

    NavMeshAgent navmesh;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isFall = false;
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
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
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
        if (isFall)
        {
            enemyState.aiState = EnemyState.EnemyAiState.Fall;
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
        Debug.Log(enemyState.aiState);
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
            isFall = true;
            navmesh.enabled = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Push")
        {
            Knockback(other);
            isDmage = true;
        }
    }
    //ノックバック関数
    void Knockback(Collision other)
    {
        // 衝突位置を取得する
        Vector3 hitPos = other.contacts[0].point;

        // 衝突位置から自機へ向かうベクトルを求める
        Vector3 boundVec = this.transform.position - hitPos;
        boundVec.y = 0f;
        // 逆方向にはねる
        forceDir = boundsPower * boundVec.normalized;
        rb.AddForce(forceDir, ForceMode.Impulse);

        isDmage = false;
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
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }
}
