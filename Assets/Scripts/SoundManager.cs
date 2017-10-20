//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 20, 2017   //
//								 //
//-------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] aclips;
	private AudioSource aud;

	public void Initialize() {
		aud = GetComponent<AudioSource>();
	}

	public void PlayClip(EUtil.SoundFX sfx) {
		aud.PlayOneShot(aclips[(int)sfx], 1);
	} 


}
