using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum EnemyAiState
    {
        WAIT,           //�s������U��~
        MOVE,           //�ړ�
        ATTACK,     //��~���čU��
        Fall,
    }

    public EnemyAiState aiState;
}
