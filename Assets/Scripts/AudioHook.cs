/* 
 * Gab De Jesus, neonpotion
 * https://twitter.com/gabottles
 * 06/16/2019
 */

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHook : MonoBehaviour {

	[SerializeField] AudioChannel _channel = AudioChannel.BGM;

	VolumeControl _control;
	AudioSource _audioSource;
	bool _initialized = false;
	float _sourceMaxVolume;

	void Awake() {
		_audioSource = GetComponent<AudioSource>();
		_sourceMaxVolume = _audioSource.volume;
	}

	void OnDestroy() {
		if (_initialized) {
			_control.EventVolumeChange -= SetVolume;
		}
	}

	/// <summary>
	/// Assigns the volume controller for this hook.
	/// </summary>
	/// <param name="c"></param>
	public void AssignVolumeControl(VolumeControl c) {
		_initialized = true;
		_control = c;
		_control.EventVolumeChange += SetVolume;
	}

	/// <summary>
	/// Changes the Audio Source volume using the channel and master volume.
	/// </summary>
	/// <param name="data"></param>
	void SetVolume(VolumeData data) {

		float newChannelVolume = (_channel == AudioChannel.BGM) ? data.bgmVolume : data.sfxVolume;

		_audioSource.volume = Mathf.Lerp(0, _sourceMaxVolume, newChannelVolume);
		_audioSource.volume *= data.masterVolume;

	}
}

/// <summary>
/// interface that the volume controller should implement.
/// </summary>
public interface VolumeControl {
	event System.Action<VolumeData> EventVolumeChange;

	float master {
		get; set;
	}

	float sfxChannel {
		get; set;
	}

	float bgmChannel {
		get; set;
	}
}

// struct so we can pass around volume data without allocating memory
public struct VolumeData {
	public float sfxVolume;
	public float bgmVolume;
	public float masterVolume;

	public VolumeData(float master, float sfx, float bgm) {
		sfxVolume = sfx;
		bgmVolume = bgm;
		masterVolume = master;
	}
}

public enum AudioChannel {
	SFX, BGM
}