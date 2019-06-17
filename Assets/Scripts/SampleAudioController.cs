/* 
 * Gab De Jesus, neonpotion
 * https://twitter.com/gabottles
 * 06/16/2019
 */

using System;
using UnityEngine;

/// <summary>
/// A sample class for playing audio with simple audio settings
/// </summary>
public class SampleAudioController : MonoBehaviour, VolumeControl {

	float _masterVolume = 1f;
	float _sfxVolume = 1f;
	float _bgmVolume = 1f;

	/// <summary>
	/// The master volume is a multiplier that affects both the bgm and the sfx
	/// </summary>
	public float master {
		get { return _masterVolume; }
		set {
			_masterVolume = value;
			EventVolumeChange(new VolumeData(_masterVolume, _sfxVolume, _bgmVolume));
		}
	}

	/// <summary>
	/// The sfx channel volume is a multiplier for audiosources that are tagged as SFX
	/// </summary>
	public float sfxChannel {
		get { return _sfxVolume; }
		set {
			_sfxVolume = value;
			EventVolumeChange(new VolumeData(_masterVolume, _sfxVolume, _bgmVolume));
		}
	}

	/// <summary>
	/// The bgm channel volume is a multiplier for audiosources that are tagged as BGM
	/// </summary>
	public float bgmChannel {
		get { return _bgmVolume; }
		set {
			_bgmVolume = value;
			EventVolumeChange(new VolumeData(_masterVolume, _sfxVolume, _bgmVolume));
		}
	}

	/// <summary>
	/// This event fires when a volume channel changes it's value
	/// </summary>
	public event Action<VolumeData> EventVolumeChange = (VolumeData dummy) => { };

	void Start() {
		FindAndAssignAudioHookInstances();
	}

	/// <summary>
	/// Finds all active audio hooks in the scene and assigns the volume control object.
	/// </summary>
	public void FindAndAssignAudioHookInstances() {
		AudioHook[] hooks = FindObjectsOfType<AudioHook>();
		for (int i = 0; i < hooks.Length; i++) {
			hooks[i].AssignVolumeControl(this);
		}
	}

	/// <summary>
	/// Sets the master volume to the value given.
	/// </summary>
	/// <param name="volume"></param>
	public void SetMasterVolume(float volume) {
		master = Mathf.Clamp01(volume);
	}

	/// <summary>
	/// Sets the given channel to the given volume
	/// </summary>
	/// <param name="channel">The channel to change</param>
	/// <param name="volume">the new volume</param>
	public void SetChannelVolume(AudioChannel channel, float volume) {
		if (channel == AudioChannel.BGM) {
			bgmChannel = Mathf.Clamp01(volume);
		} else {
			sfxChannel = Mathf.Clamp01(volume);
		}
	}
}
