# **Audio hooks**

This is a simple component for hooking audio sources with a global audio volume controller.



## **Instructions**

1. Implement the VolumeControl Interface so we know where the audio hook would listen too
2. Attach the audiohook component to an audio source
3. Specify the channel of the audiohook
4. You're Done! Triggering the volume change event in the VolumeControl interface should update the audio souce.



I've included a sample audio controller and a sample scene.