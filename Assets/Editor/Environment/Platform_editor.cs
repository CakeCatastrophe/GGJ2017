using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Platform))]
public class Platform_editor : Editor
{

    void OnSceneGUI()
    {
        
        Platform myPlatform = (Platform)target;
        if (myPlatform != null)
        {
            Vector3 startPos = myPlatform.StartPos;
            Vector3 NewStartPos = Handles.PositionHandle(startPos, Quaternion.identity);

            Vector3 endPos = myPlatform.EndPos;
            Vector3 NewEndPos = Handles.PositionHandle(endPos, Quaternion.identity);

            Vector3 anchorPoint = myPlatform.AnchorPos;
            if (startPos != NewStartPos)
            {
                Vector3 min = NewStartPos-anchorPoint;
                myPlatform.m_minOffset = min;
                Vector3 max = -myPlatform.m_minOffset.normalized * myPlatform.m_maxOffset.magnitude;
                myPlatform.m_maxOffset = max;
                EditorUtility.SetDirty(myPlatform);
            }
            else if (endPos != NewEndPos)
            {
                Vector3 max =  NewEndPos-anchorPoint;

                   
                myPlatform.m_maxOffset = max;
                Vector3 min = -myPlatform.m_maxOffset.normalized * myPlatform.m_minOffset.magnitude;
                myPlatform.m_minOffset = min;
                
                EditorUtility.SetDirty(myPlatform);
            }


        }
    }
}
