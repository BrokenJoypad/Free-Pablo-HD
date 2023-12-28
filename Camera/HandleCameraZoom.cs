using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class HandleCameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineCamera;
    private int CameraZoom = 0;
    private float CurrentFov;
    [SerializeField] private float DefaultFOV = 60;
    [SerializeField] private float ZoomedInFOVTarget = 30;


    void Start(){

        CinemachineCamera = GetComponentInParent<CinemachineVirtualCamera>();
        CinemachineCamera.m_Lens.FieldOfView = DefaultFOV;
    }

    void Update(){

        CurrentFov = CinemachineCamera.m_Lens.FieldOfView;

        if (Input.mouseScrollDelta.y == 1){
            CameraZoom = 1;
            if(CameraZoom == 1){
                StartCoroutine(IncrementTowardsTargetFOV());
            }
        } else if(Input.mouseScrollDelta.y == -1){
            CameraZoom = -1;
            if (CameraZoom == -1){
                StartCoroutine(DecrementTowardsDefaultFOV());
            }
        }
    }

    IEnumerator IncrementTowardsTargetFOV()
    {
        while (CurrentFov > ZoomedInFOVTarget)
            {
                CinemachineCamera.m_Lens.FieldOfView -= 100f * Time.deltaTime;
                yield return null;
            
            }

    }

    IEnumerator DecrementTowardsDefaultFOV()
    {
        while (CurrentFov < DefaultFOV)
        {
            CinemachineCamera.m_Lens.FieldOfView += 100f * Time.deltaTime;
            yield return null;

        }

    }

}