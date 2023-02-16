using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Enemy))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        Enemy enemy = (Enemy)target;
        Handles.color = Color.white;

        //Vector3 viewAngleA = enemy.

        //Handles.DrawLine(enemy.transform.position, enemy.transform.position + Vector3.Angle(enemy.transform.forward, vectorToPlayer) * enemy.VisionRange);
    }
}
