using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float m_rotationSpeed = 10.0f;
    public float m_visionRange = 10.6f;
    [Range(0, 360)]
    public float m_viewConeAngle = 80.0f;
    public float m_pursuingMaxTime = 5.0f;
    public float m_shootingMaxTime = 2.0f;
    public Color VIEWCONE_COLOR_PATROL = Color.green;
    public Color VIEWCONE_COLOR_WARNING = Color.yellow;
    public Color VIEWCONE_COLOR_DANGER = Color.red;

}
