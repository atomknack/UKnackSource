#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UKnack.Attributes.KnackPickers;

public partial class ValidPicker 
{
    private void KeyDown(KeyDownEvent e)
    {
        if (e.keyCode == KeyCode.Escape)
        {
            ESCKeyWasPressed=true;
        }
    }

    private void CheckEscAndCallOnCancelIfNotAlreadyCalled(string by, bool alreadyDestroying = false)
    {
        //Debug.Log($"{by} - OnCancel state: was called {_onCancelWasCalled}; isNull {onCancel == null}; EscPressed {Keyboard.current.escapeKey.wasPressedThisFrame} {Event.current}");

        if (_onCancelWasCalled)
            return;

        if (ESCKeyWasPressed)
        {
            DoOnCancel();
            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            DoOnCancel();
            return;
        }

        if (Event.current == null)
            return;
        //Debug.Log($"{Event.current.type} {Event.current.keyCode}");

        if (Event.current.isKey && Event.current.keyCode == KeyCode.Escape)
        {
            DoOnCancel();
        }

        void DoOnCancel()
        {
            if (_onCancelWasCalled)
                return;
            //Debug.Log($"{by} - OnCancel should be invoked");
            onCancel?.Invoke();
            _onCancelWasCalled = true;
            if ( !alreadyDestroying ) 
                Close();
        }
    }

}
#endif

///////////////////////////////////////////////////////////

//private void OnProjectChange() => CheckEscAndCallOnCancelIfNotAlreadyCalled("OnProjectChange");
//private void OnHierarchyChange() => CheckEscAndCallOnCancelIfNotAlreadyCalled("OnHierarchyChange");
//private void Update() => CheckEscAndCallOnCancelIfNotAlreadyCalled("Update");
/*
//Key handling from
//https://forum.unity.com/threads/listen-for-key-up-down-in-editor-mode.978912/

[UnityEditor.Callbacks.DidReloadScripts]
private static void ScriptsHasBeenReloaded()
{
    SceneView.duringSceneGui += DuringSceneGui;
}

private static void DuringSceneGui(SceneView sceneView)
{
    Event e = Event.current;

    if (e.type == EventType.KeyUp)
    {
        //Do Something
    }

    if (e.type == EventType.KeyDown)
    {
        Debug.Log(e.keyCode);
        if (e.keyCode == KeyCode.Escape)
        {
            Debug.Log($"Got esc key");
            ESCKeyWasPressed = true;
        }
    }

    //Right mouse button
    if (e.type == EventType.MouseDown && Event.current.button == 0)
    {
        //Do Something
    }
}
*/