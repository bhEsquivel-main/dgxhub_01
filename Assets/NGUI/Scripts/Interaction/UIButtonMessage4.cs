//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sends a message to the remote object when something happens.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Message4")]
public class UIButtonMessage4 : UIButtonMessage
{
    public GameObject target1;
    public string functionName1;
    public bool isPress;

    private float _Curtime = 0.0f;
    private float _Starttime = 1.0f;        // 1.0 초 뒤에 발동하기 위한 변수
    private float _UpdataCurtime = 0.0f;
    private float _Updatatime = 0.1f;        // 

    //-------------------------------------------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------------------------------------------------------//

    void OnPress(bool isPressed)
    {
        if (enabled)
        {
            if (isPressed && trigger == Trigger.OnPress)
            {
                isPress = true;
            }
            else if (!isPressed)
            {
                isPress = false;
                _Curtime = 0.0f;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------//

    void Update()
    {
        if (isPress)
        {
            if (_Curtime >= _Starttime)
            {
                if (_UpdataCurtime >= _Updatatime)
                {
                    if (string.IsNullOrEmpty(functionName1))
                        return;

                    if (target1 == null)
                        return;

                    target1.SendMessage(functionName1, gameObject, SendMessageOptions.DontRequireReceiver);

                    _UpdataCurtime = 0.0f;
                }
                else
                {
                    _UpdataCurtime += Time.deltaTime;
                }
            }
            else
            {
                _Curtime += Time.deltaTime;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------------------------------------------------------------------------------------------------------------//
}