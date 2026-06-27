using UnityEngine;

public class CircularAudioVisualizer : MonoBehaviour
{
    public int sampleSize = 64;  // Number of bars
    public float radius = 1f;    
    public float heightMultiplier = 10f; // Height scaling
    public GameObject pixelBarPrefab;  // Prefab for pixelated bars

    private AudioSource audioSource;
    //private float[] audioSamples;
    private GameObject[] visualElements;

    public int spectrumSize = 512; // Much higher than sampleSize
    private float[] audioSamples;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        audioSamples = new float[sampleSize];
        visualElements = new GameObject[sampleSize];

        // Instantiate pixel objects around the circle - much easier ways to do this + only works for centre of screen
        for (int i = 0; i < sampleSize; i++){
            float angle = 360f / sampleSize * i;
            Vector3 position = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                0
            );

            visualElements[i] = Instantiate(pixelBarPrefab, position, Quaternion.identity, this.transform);
            visualElements[i].transform.LookAt(Vector3.forward, visualElements[i].transform.position - transform.position);
        }
    }

    void Update(){
        audioSource.GetSpectrumData(audioSamples, 0, FFTWindow.Blackman);

        int samplesPerBar = spectrumSize / sampleSize;

        for (int i = 0; i < sampleSize; i++) {
            float sum = 0f;
            int start = i * samplesPerBar;
            int end = Mathf.Min(start + samplesPerBar, spectrumSize);

            for (int j = start; j < end; j++) {
                sum += audioSamples[j];
            }

            float average = sum / (end - start);
            float scaleY = Mathf.Clamp(average * heightMultiplier, 0.1f, 50f);

            // Optional smoothing
            Vector3 currentScale = visualElements[i].transform.localScale;
            float smoothedY = Mathf.Lerp(currentScale.y, scaleY, Time.deltaTime * 30f);

            visualElements[i].transform.localScale = new Vector3(1, smoothedY, 1);
        }
    }
}

