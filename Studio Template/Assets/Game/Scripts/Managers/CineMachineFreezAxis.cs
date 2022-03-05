using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CineMachineFreezAxis : CinemachineExtension
{
    public float X_Position = 10;
    public float Y_Position = 10;
    public float Z_Position = 10;

    public bool isXAxis = true;
    public bool isYAxis = true;
    public bool isZAxis = false;
    public bool isYOnlyUp = false;

    Vector3 camPos = new Vector3(0, -20, 0);
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (enabled && stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            if (isXAxis) pos.x = X_Position;
            if (isYAxis) pos.y = Y_Position;
            if (isZAxis) pos.z = Z_Position;


            if (isYOnlyUp)
            {
                if (pos.y < camPos.y)
                {
                    pos.y = camPos.y;
                }

                camPos = pos;
            }
            state.RawPosition = pos;
        }
    }
}