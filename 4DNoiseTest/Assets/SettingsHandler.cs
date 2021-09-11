using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsHandler : MonoBehaviour {

    [SerializeField] Noise4D noise = null;

    void Start() {
        transform.Find("Size").GetChild(0).GetComponent<InputField>().text = noise.size.ToString();
        transform.Find("Speed").GetChild(0).GetComponent<InputField>().text = noise.speed.ToString();
        transform.Find("Threshold").GetChild(0).GetComponent<InputField>().text = noise.threshold.ToString();
        transform.Find("Frequency").GetChild(0).GetComponent<InputField>().text = noise.frequency.ToString();
        transform.Find("UpdateDelay").GetChild(0).GetComponent<InputField>().text = noise.updateDelay.ToString();
    }

    public void ChangeSize(string value) {
        noise.size = Mathf.Max(float.Parse(value), noise.minSize);
        transform.Find("Size").GetChild(0).GetComponent<InputField>().text = noise.size.ToString();
    }

    public void ChangeSpeed(string value) {
        noise.speed = float.Parse(value);
    }

    public void ChangeThreshold(string value) {
        noise.threshold = float.Parse(value);
    }

    public void ChangeFrequency(string value) {
        noise.frequency = float.Parse(value);
    }

    public void ChangeUpdateDelay(string value) {
        noise.updateDelay = float.Parse(value);
    }
}
