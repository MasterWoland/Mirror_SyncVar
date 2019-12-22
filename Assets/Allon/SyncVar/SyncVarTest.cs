using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SyncVarTest : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdateSyncVar))]
    private Color32 _color = Color.red;

    public override void OnStartServer()
    {
        base.OnStartServer();

        StartCoroutine(ChangeSyncVar());
    }

    private void UpdateSyncVar(Color32 color)
    {
        Renderer r = GetComponent<Renderer>();
        r.material.color = color;
        Debug.Log("Setting color on CLIENT");
    }

    private IEnumerator ChangeSyncVar()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
            _color = Random.ColorHSV();
            Debug.Log("[COLOR] "+_color);

            yield return wait;
        }
    }
}
