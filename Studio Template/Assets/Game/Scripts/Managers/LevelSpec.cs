using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpec : MonoBehaviour
{
    [SerializeField] Transform road;
    [SerializeField] Vector3 roadStartFrom;
    [SerializeField] Transform finishSection;
    [SerializeField] Vector3 finishSectionPosition;


    private void Awake()
    {
        road.localPosition = roadStartFrom;
        finishSection.position = finishSectionPosition;
    }
}
