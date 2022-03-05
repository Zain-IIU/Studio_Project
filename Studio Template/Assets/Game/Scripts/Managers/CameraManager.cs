using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera camMain;
    [SerializeField] CinemachineVirtualCamera camFinish;
    [SerializeField] CinemachineVirtualCamera camFinishOpenHospital;
    [SerializeField] Transform finishTarget;


    Vector3 finishTargetDefaultPosition;    
    public override void Init()
    {
        finishTargetDefaultPosition = finishTarget.localPosition;
      
    }

    public void FollowMainCam(Transform target)
    {
        camMain.m_Follow = target;
        camMain.m_Priority = 10;
        camFinish.m_Priority = 9;
        camFinishOpenHospital.m_Priority = 8;
    }


    public void SwitchToFinishCam(float distanceCover,int totalPatients)
    {
        camMain.m_Priority = 9;
        camFinish.m_Priority = 10;
        finishTarget.localPosition = finishTargetDefaultPosition;
        finishTarget.DOMoveZ(distanceCover, (8f / 32) * totalPatients).SetDelay(.5f).SetEase(Ease.Linear);
      
    }


    public void SwitchToFinishCamOpenHospital()
    {
        camMain.m_Priority = 9;
        camFinishOpenHospital.m_Priority = 10;     

    }
}
