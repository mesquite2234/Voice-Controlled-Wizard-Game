using System.Collections;
using UnityEngine;

public class MicLoudnessFilter : MonoBehaviour
{
    public string selectedDevice;
    [Range(0, 1f)]
    public float loudness; // Smoothed loudness above background
    public int clipLength = 1;

    [SerializeField]
    private AudioClip clip;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
            selectedDevice = Microphone.devices[0];
        else
            Debug.LogError("No microphone detected!");
    }

    void Update()
    {
        StartCoroutine(PlayBack());
    }

    IEnumerator PlayBack()
    {
        try
        {
            Microphone.End(Microphone.devices[0]);
        }
        catch
        {
            Debug.Log("First Cycle");
        }
        source.PlayOneShot(clip);
        clip = Microphone.Start(selectedDevice, false, clipLength, AudioSettings.outputSampleRate);
        yield return new WaitForSeconds(clipLength);
        StartCoroutine(PlayBack());
    }
}
