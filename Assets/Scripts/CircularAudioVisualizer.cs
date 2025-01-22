using UnityEngine;

public class CircularAudioVisualizer : MonoBehaviour
{
    public int sampleSize = 64;  // Number of bars
    public float radius = 1f;    // Radius of the circle
    public float heightMultiplier = 10f; // Height scaling
    public GameObject pixelBarPrefab;  // Prefab for pixelated bars

    private AudioSource audioSource;
    private float[] audioSamples;
    private GameObject[] visualElements;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        audioSamples = new float[sampleSize];
        visualElements = new GameObject[sampleSize];

        // Instantiate pixel objects around the circle
        for (int i = 0; i < sampleSize; i++){
            float angle = (360f / sampleSize) * i;
            Vector3 position = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                0
            );

            visualElements[i] = Instantiate(pixelBarPrefab, position, Quaternion.identity, this.transform);
            visualElements[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, visualElements[i].transform.position - transform.position);
        }
    }

    void Update(){
        audioSource.GetSpectrumData(audioSamples, 0, FFTWindow.Blackman);

        for (int i = 0; i < sampleSize; i++){
            float scaleY = Mathf.Clamp(audioSamples[i] * heightMultiplier, 0.1f, 5f);
            //int index = Mathf.FloorToInt(Mathf.Pow(i, 2f) / (sampleSize * sampleSize) * audioSamples.Length); // attempting to redistribute

            visualElements[i].transform.localScale = new Vector3(1, scaleY, 1);
        }
    }
}

