using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class CameraTest : MonoBehaviour
{
/*
    public bool cameraDebug = false;
    public bool playerForward = false;
//_____________________________________________________
    float sphereRadius;
    LayerMask layerWhiteList;
    PlayerCameraSc camSc;
//_____________________________________________________
    void Awake()
    {
        if(cameraDebug)
        {
            UpdateVars();
        }
    }
    void UpdateInfo()
    {
        try
        {
            camSc = Camera.main.GetComponent<PlayerCameraSc>();
            sphereRadius = camSc.sphereRadius;
            layerWhiteList = camSc.groundLayersWL;
        }
        catch
        {
            Debug.Log("<color=red>PlayerCameraSC not found</color>", this);
        }
    }
    public void UpdateVars()
    {
        UpdateInfo();
        if(!camSc.GetCamera())
        {
            camSc.SetCamera(Camera.main);
        }
        if(!camSc.GetPlayerObj())
        {
            camSc.SetPlayerObj(GameObject.FindWithTag("Player"));
        }
        var playerSc = camSc.GetPlayerObj().GetComponent<PlayerControllerSc>();
        if(!playerSc.GetCameraSc())
        {
            playerSc.SetCameraSc(camSc);
        }
    }
    public void UpdateCam()
    {
        var _cam = camSc.gameObject.GetComponent<Camera>();
        var _player = camSc.GetPlayerObj();
        Physics.Raycast(_cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0)), out var sphereCenter, Mathf.Infinity, layerWhiteList);
        Vector3 target = transform.position + (_player.transform.position - sphereCenter.point);
        target.y = transform.position.y;
        if(!_cam.orthographic && sphereCenter.distance != 0 && sphereCenter.distance != camSc.cameraHeight)
        {
            target.y = camSc.cameraHeight/sphereCenter.distance * target.y;
        }
        if(_cam.orthographic && sphereCenter.distance != 0 && sphereCenter.distance != camSc.cameraHeight)
        {
            _cam.orthographicSize = camSc.cameraHeight/sphereCenter.distance * target.y;
        }
        transform.position = target;
    }
    void OnDrawGizmos()
    {
        if(cameraDebug)
        {
            UpdateInfo();
            RaycastHit hitInfo;
            Physics.Raycast(camSc.GetCamera().ViewportPointToRay(new Vector3(0.5f,0.5f,0)), out hitInfo, Mathf.Infinity, layerWhiteList);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(hitInfo.point, sphereRadius);
            Debug.DrawLine(camSc.GetCamera().ViewportPointToRay(new Vector3(0.5f,0.5f,0)).origin, hitInfo.point, Color.green);
        }
        if(playerForward)
        {
            if(!cameraDebug)UpdateInfo();
            Vector3 startLinePosition = camSc.GetPlayerObj().transform.position;
            startLinePosition.y +=0.7f;
            Debug.DrawLine(startLinePosition, startLinePosition+camSc.GetPlayerObj().transform.forward*5, Color.yellow);
        }
    }
    */
}

//_______________________________________________________________________________________
/*
[CustomEditor(typeof(CameraTest))]
class CameraTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraTest _camTest = (CameraTest)target;
        if(_camTest.cameraDebug)
        {
            if (GUILayout.Button("Update variables"))
            {
                try
                {
                    _camTest.UpdateVars();
                    Debug.Log("<color=green>Updtating variables succes</color>", this);
                }
                catch
                {
                    Debug.Log("<color=red>Updtating variables failed</color>", this);
                }
            }
            if (GUILayout.Button("Update camera position"))
            {
                try
                {
                    _camTest.UpdateCam();
                    Debug.Log("<color=green>Camera moved</color>", this);
                }
                catch (System.Exception e)
                {
                    Debug.Log("<color=red>Error. Camera not moved</color>", this);
                    throw e;
                }
            }
        }
    }
}
*/
//_______________________________________________________________________________________
public class ReadOnlyAttribute : PropertyAttribute{}
 
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

