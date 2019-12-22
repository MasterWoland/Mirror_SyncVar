using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Tracker : NetworkBehaviour
{
    public Transform _trackableTransform;
    public Camera _mainCamera;
    public Vector3 _position;
//    public Quaternion _rotation;
    public Vector3 _clientPosition;

//    private void Awake()
//    {
//        Debug.Log("Awaking: "+this.name);
//        DontDestroyOnLoad(this);
//    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("SERVER");
        CmdFindTrackable();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log("We are the client");
    }

    private void Start()
    {
        StartCoroutine(TrackServerCamera());
    }

    private IEnumerator TrackServerCamera()
    {
        while (true)
        {
            // appears not to find it at times
            if(_trackableTransform == null) CmdFindTrackable();
            
            CmdObtainServerTransform();

            // setting client position to the position found on the server
            RpcSetClientTransform(_position);


            yield return new WaitForEndOfFrame();
        }
    }

    [Command]
    private void CmdObtainServerTransform()
    {
        _position = _trackableTransform.position;
    }

    [ClientRpc]
    private void RpcSetClientTransform(Vector3 pos)
    {
        _clientPosition = pos;
    }

//    private void OnEnable()
//    {
//        SceneLoaded.SceneIsLoaded += OnSceneLoaded;
//    }
//
//    private void OnSceneLoaded()
//    {
//        SceneLoaded.SceneIsLoaded -= OnSceneLoaded;
//
////        _mainCamera = Camera.main;
//        CmdFindTrackable();
//    }

    [Command]
    private void CmdFindTrackable()
    {
        _trackableTransform = GameObject.FindWithTag("Trackable").transform;

        Debug.Log("___ finding trackable: " + _trackableTransform.name);
    }
}