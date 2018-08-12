using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityCheck : MonoBehaviour {

    static public bool visible = true;
    public MeshRenderer myMeshRenderer;

	// Switches the plane meshes from visible to not visible
	void Update () {
        if (visible){
            myMeshRenderer.enabled = true;
        }
        else {
            myMeshRenderer.enabled = false;
        }
	}

    public void SwitchVisibility(){
        visible = !visible;
    }
}
