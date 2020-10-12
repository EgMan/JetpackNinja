using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public List<Transform> points;
    public float timeIncrement = 0.01f;
    public float timeOffset = 0f;
    private float time;
    private static float [,] CATMULL_ROM_SPLINE = new float[4,4] {
        {-0.5f,  1.0f, -0.5f,  0.0f},
        {1.5f, -2.5f,  0.0f,  1.0f},
        {-1.5f,  2.0f,  0.5f,  0.0f},
        {0.5f, -0.5f, 0.0f, 0.0f}
    };

    // Start is called before the first frame update
    void Start()
    {
        time = timeOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count == 0)
        {
            return;
        }
        int idx = (int)Mathf.Floor(time);
        Vector3 v1 = points[idx % points.Count].position;
        Vector3 v2 = points[(idx + 1) % points.Count].position;
        Vector3 v3 = points[(idx + 2) % points.Count].position;
        Vector3 v4 = points[(idx + 3) % points.Count].position;

        float x = interpolateFloats(time, v1.x, v2.x, v3.x, v4.x);
        float y = interpolateFloats(time, v1.y, v2.y, v3.y, v4.y);
        float z = interpolateFloats(time, v1.z, v2.z, v3.z, v4.z);
        gameObject.transform.position = new Vector3(x, y, z);

        time += timeIncrement;
    }
    float interpolateFloats(float time, float f1, float f2, float f3, float f4)
	{
		time = time % 1f;
		float time_pow2 = Mathf.Pow(time, 2);
		float time_pow3 = Mathf.Pow(time, 3);

		return (time_pow3 * CATMULL_ROM_SPLINE[0, 0] + time_pow2 * CATMULL_ROM_SPLINE[0, 1] + time * CATMULL_ROM_SPLINE[0, 2] + CATMULL_ROM_SPLINE[0, 3]) * f1 +
			(time_pow3 * CATMULL_ROM_SPLINE[1, 0] + time_pow2 * CATMULL_ROM_SPLINE[1, 1] + time * CATMULL_ROM_SPLINE[1, 2] + CATMULL_ROM_SPLINE[1, 3]) * f2 +
			(time_pow3 * CATMULL_ROM_SPLINE[2, 0] + time_pow2 * CATMULL_ROM_SPLINE[2, 1] + time * CATMULL_ROM_SPLINE[2, 2] + CATMULL_ROM_SPLINE[2, 3]) * f3 +
			(time_pow3 * CATMULL_ROM_SPLINE[3, 0] + time_pow2 * CATMULL_ROM_SPLINE[3, 1] + time * CATMULL_ROM_SPLINE[3, 2] + CATMULL_ROM_SPLINE[3, 3]) * f4;
	}
}
