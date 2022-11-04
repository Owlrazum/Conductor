using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointType
{
    TurnToLeft,
    TurnToRight,
    Final
}

public class PathPoint : MonoBehaviour
{
    [SerializeField, Tooltip("Should be only TurnToLeft or TurnToRight. Final is the bus position of stopping platform")]
    private PointType turnType;

    public PointType GetTurnType()
    {
        return turnType;
    }
}
