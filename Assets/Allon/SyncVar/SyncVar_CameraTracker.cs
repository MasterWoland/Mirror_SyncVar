﻿using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SyncVar_CameraTracker : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdateCameraPosition))]
    public Vector3 _position;

    [SyncVar(hook = nameof(UpdateCameraRotation))]
    public Quaternion _rotation;

    public bool IsMainCameraOnServer = false;
    public Camera _mainCamera;

    public override void OnStartServer()
    {
        base.OnStartServer();

        FindMainCamera();
        StartCoroutine(TrackMainCamera());
    }

    private void FindMainCamera()
    {
        _mainCamera = Camera.main;
        if (_mainCamera)
        {
            IsMainCameraOnServer = true;
        }
    }

    private void UpdateCameraPosition(Vector3 pos)
    {
        Debug.Log("[CLIENT] pos = " + pos);
    }

    private void UpdateCameraRotation(Quaternion rot)
    {
        Debug.Log("[CLIENT] rotation = " + rot);
    }

    private IEnumerator TrackMainCamera()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (IsMainCameraOnServer)
        {
            _position = Camera.main.transform.position;
            _rotation = Camera.main.transform.rotation;

            Debug.Log("___server coroutine___");
            yield return wait;
        }
    }
}