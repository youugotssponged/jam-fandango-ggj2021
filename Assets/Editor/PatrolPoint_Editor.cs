using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatrolPoint))]
public class PatrolPoint_Editor : Editor
{
    private void OnSceneGUI()
    {
        PatrolPoint pp = (PatrolPoint)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(pp.transform.position, Vector3.up, Vector3.forward, 360, pp.patrolRadius);
    }
}
