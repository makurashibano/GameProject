using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum EnemyAiState
    {
        WAIT,           //s“®‚ğˆê’U’â~
        MOVE,           //ˆÚ“®
        ATTACK,     //’â~‚µ‚ÄUŒ‚
        Fall,
    }

    public EnemyAiState aiState;
}
