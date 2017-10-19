//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Plays the specified sound.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
		Custom,
	}

	public AudioClip audioClip;
	public Trigger trigger = Trigger.OnClick;

	bool mIsOver = false;

#if UNITY_3_5
	public float volume = 1f;
	public float pitch = 1f;
#else
	[Range(0f, 1f)] public float volume = 1f;
	[Range(0f, 2f)] public float pitch = 1f;
#endif

	bool canPlay
	{
		get
		{
			if (!enabled) return false;
			UIButton btn = GetComponent<UIButton>();
			return (btn == null || btn.isEnabled);
		}
	}

	void OnHover (bool isOver)
	{
		if (trigger == Trigger.OnMouseOver)
		{
			if (mIsOver == isOver) return;
			mIsOver = isOver;
		}

        if (canPlay && ((isOver && trigger == Trigger.OnMouseOver) || (!isOver && trigger == Trigger.OnMouseOut)))
        {
            // mylee modify Option volume
          //  volume = CFunc.GetOption().gameOption.EffectSound;
            NGUITools.PlaySound(audioClip, volume, pitch);
        }
	}

	void OnPress (bool isPressed)
	{
		if (trigger == Trigger.OnPress)
		{
			if (mIsOver == isPressed) return;
			mIsOver = isPressed;
		}

        if (canPlay && ((isPressed && trigger == Trigger.OnPress) || (!isPressed && trigger == Trigger.OnRelease)))
        {
            // mylee modify Option volume
           // volume = CFunc.GetOption().gameOption.EffectSound;
            NGUITools.PlaySound(audioClip, volume, pitch);
        }
	}

	void OnClick ()
	{
        if (canPlay && trigger == Trigger.OnClick)
        {
            // mylee modify Option volume
          //  volume = CFunc.GetOption().gameOption.EffectSound;
            NGUITools.PlaySound(audioClip, volume, pitch);
        }
	}

	void OnSelect (bool isSelected)
	{
        if (canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
        {
            // mylee modify Option volume
           // volume = CFunc.GetOption().gameOption.EffectSound;
            OnHover(isSelected);
        }
	}

	public void Play ()
	{
        // mylee modify Option volume
     //   volume = CFunc.GetOption().gameOption.EffectSound;
		NGUITools.PlaySound(audioClip, volume, pitch);
	}
}
