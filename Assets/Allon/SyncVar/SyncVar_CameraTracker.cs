using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SyncVar_CameraTracker : NetworkBehaviour
{
    public bool DoMoveCameraOnClient = false;
    
    [SyncVar(hook = nameof(UpdateCameraPosition))]
    public Vector3 _position;

    [SyncVar(hook = nameof(UpdateCameraRotation))]
    public Quaternion _rotation;

    public bool IsMainCameraOnServer = false;
    public Camera _mainCamera;
    public Camera _clientCamera;
    
    public override void OnStartServer()
    {
        base.OnStartServer();

        FindMainCamera();
        StartCoroutine(TrackMainCamera());
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        _clientCamera = Camera.main;
    }

    private void FindMainCamera()
    {
        _mainCamera = Camera.main;
        if (_mainCamera) IsMainCameraOnServer = true;
    }

    private void UpdateCameraPosition(Vector3 pos)
    {
        if (DoMoveCameraOnClient)
        {
            _clientCamera.transform.position = pos;
        }
    }

    private void UpdateCameraRotation(Quaternion rot)
    {
        if (DoMoveCameraOnClient)
        {
            _clientCamera.transform.rotation = rot;
        }
    }

    private IEnumerator TrackMainCamera()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        var _mainCamTF = _mainCamera.transform;
        
        while (IsMainCameraOnServer)
        {
            _position = _mainCamTF.position;
            _rotation = _mainCamTF.rotation;
            
            yield return wait;
        }
    }

    // -------- EVENTS ----------
    private void OnEnable()
    {
        CanvasManager.OnToggleMoveCameraOnClient += ToggleDoMoveCameraOnClient;
    }

    private void OnDisable()
    {
        CanvasManager.OnToggleMoveCameraOnClient += ToggleDoMoveCameraOnClient;
    }

    public void ToggleDoMoveCameraOnClient()
    {
        DoMoveCameraOnClient = !DoMoveCameraOnClient;
    }

    // --------------------------
}