using System;
using Cinemachine;
using UnityEngine;

public class CameraHelper : QuantumCallbacks
{
    [SerializeField] private CinemachineVirtualCamera camera;
    private void Awake()
    {
        PlayerEntityView.OnPlayerInstantiated += PlayerInstantiated;
    }

    private void OnDestroy()
    {
        PlayerEntityView.OnPlayerInstantiated -= PlayerInstantiated;
    }

    private void PlayerInstantiated(bool isLocal, CameraController cameraController)
    {
        if(!isLocal) return;
        camera.Follow = cameraController.CinemachineCameraTarget.transform;
    }
}
