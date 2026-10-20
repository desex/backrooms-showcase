using System;
using DG.Tweening;
using UnityEngine;

namespace Ecs.Utils.ObstaclePass
{
    [Serializable]
    public partial struct PassObstacleMovePoint
    {
        public float moveTime;      //TODO: REFACTOR THIS when will be work on some changes with Pass Obstacle
        public Transform movePoint; //TODO: REFACTOR THIS PlasticVisualAnimation_IsAnimationCorrect
        public EActionType ActionType;
        public Ease MovementEase;
    }
}