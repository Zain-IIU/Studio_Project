using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

[CustomEditor(typeof(DotweenAnimation)), CanEditMultipleObjects]

public class DotweenAnimationEditor : Editor
{

    bool targetPositionToggle = false;
    bool flagTogglePositionOn = false;
    Tool currentTool;
    public override void OnInspectorGUI()
    {
        //  return;

        DrawDefaultInspector();


        targetPositionToggle = GUILayout.Toggle(targetPositionToggle, "Set Target", "Button");
        if (!targetPositionToggle && !flagTogglePositionOn)
        {
            flagTogglePositionOn = true;

            Tools.current = currentTool;
        }else if (targetPositionToggle && flagTogglePositionOn)
        {
            flagTogglePositionOn = false;
            Tools.current = Tool.None;
        }

        if (GUILayout.Button("Reset Target Position"))
        {
            Undo.RecordObject(((DotweenAnimation)target), "Transform Position");
            ((DotweenAnimation)target).target = ((DotweenAnimation)target).transform.localPosition;
        }
     
    }


 

    protected virtual void OnSceneGUI()
    {
        if (targetPositionToggle)
        {
         if(Tools.current != Tool.None) currentTool = Tools.current;

            DotweenAnimation targetPosition = (DotweenAnimation)target;

            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(targetPosition.target, Quaternion.identity);
            Handles.DrawRectangle(1, targetPosition.target, Quaternion.identity, .15f);

            if (EditorGUI.EndChangeCheck())
            {

                Undo.RecordObject(targetPosition, "Change Look At Target Position");
                targetPosition.target = newTargetPosition;
              
            }
        }
    }


    private void OnEnable()
    {
        currentTool = Tools.current;
      
    }
    private void OnDisable()
    {
        Tools.current = currentTool;
    }
}
