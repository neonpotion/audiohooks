/* 
 * Gab De Jesus, neonpotion
 * https://twitter.com/gabottles
 * 06/16/2019
 */

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ChannelVolumeSlider : MonoBehaviour {

	[SerializeField] SampleAudioController _sampleAudioController;
	[SerializeField] AudioChannel _channel;
	Slider _slider;

	void Start() {
		_slider = GetComponent<Slider>();
		_slider.onValueChanged.AddListener(UpdateVolume);
	}

	void OnDestroy() {
		_slider.onValueChanged.RemoveListener(UpdateVolume);
	}

	/// <summary>
	/// Updates the volume
	/// </summary>
	/// <param name="val"></param>
	void UpdateVolume(float val) {
		if (_channel == AudioChannel.SFX) {
			_sampleAudioController.sfxChannel = val;
		} else {

			_sampleAudioController.bgmChannel = val;
		}
	}
}
