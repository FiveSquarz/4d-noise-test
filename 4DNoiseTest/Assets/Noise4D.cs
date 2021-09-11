using System.Collections.Generic;
using UnityEngine;
using SquarzUtilities.Maths;

public sealed class Noise4D : MonoBehaviour {

    readonly float start = -9.8151962319f;

    float lastSize;

    public float
        minSize = 0f,
        size = 0f,

        speed = 0f,
        threshold = 0f,
        frequency = 0f,
        updateDelay = 0f;

    [SerializeField] GameObject cube = null;

    Dictionary<Vector3, MeshRenderer> grid = new Dictionary<Vector3, MeshRenderer>();
    int amount = 0;

    void Start() {
        lastSize = size;
        Set();
    }

    void Set() {
        size = Mathf.Max(minSize, size);
        cube.transform.localScale = new Vector3(size, size, size);

        float last = 0f;
        for (float x = start; x < 10f; x += size)
            last = x;

        foreach (MeshRenderer meshRenderer in grid.Values) {
            Destroy(meshRenderer.gameObject);
        }
        grid.Clear();

        amount = 0;

        for (float x = start; x <= 10f; x += size) {
            for (float y = start; y <= 10f; y += size) {
                for (float z = start; z <= 10f; z += size) {
                    GameObject clone = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                    clone.GetComponent<MeshRenderer>().material.color = (x == start || y == start || z == start || x == last || y == last || z == last) ? new Color(0.5f, 1f, 1f) : new Color(1f, 1f, 1f);
                    grid.Add(clone.transform.position, clone.GetComponent<MeshRenderer>());
                    amount += 1;
                }
            }
        }
        Debug.Log(amount);
    }

    float lastUpdate = Mathf.NegativeInfinity;
    void Update() {
        threshold = Mathf.Clamp(threshold, -1f, 1f);
        updateDelay = Mathf.Max(updateDelay, 0f);

        if (size != lastSize) Set();
        lastSize = size;

        if (Time.time - lastUpdate >= updateDelay) {
            lastUpdate = Time.time;

            foreach (KeyValuePair<Vector3, MeshRenderer> item in grid) {
                item.Value.enabled = PerlinNoise.Noise4D(item.Key.x * frequency, item.Key.y * frequency, item.Key.z * frequency, Time.time * speed) >= threshold;
            }
        }
    }
}