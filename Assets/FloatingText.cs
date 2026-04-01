using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float amplitude = 0.1f;   // how high it moves
    public float frequency = 1f;     // how fast it moves
    public bool shouldFloat = true;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (shouldFloat)
        {
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = startPos + new Vector3(0f, offset, 0f);
        }
    }
}