#if UNITY_EDITOR
using AudioManagement.Tool;
using UnityEditor;
using UnityEngine;
using Utils;

[CustomEditor(typeof(AudioElementScriptableHelper))]
public class CreateAudioElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AudioElementScriptableHelper helper = (AudioElementScriptableHelper)target;
        if (GUILayout.Button("Create all audio element(s)"))
        {
            helper.CreateAll();
        }

        if (GUILayout.Button("Delete all audio element(s)"))
        {
            helper.DeleteAll();
        }

        if (GUILayout.Button("Check all audio clip is exit"))
        {
            TaskUtils.OnSameThread(helper.CheckAudioClipIsExit);
        }

        if (GUILayout.Button("Check audio clip in audio elements"))
        {
            helper.CheckAudioClipRefInAudioElement();
        }
    }
}
#endif