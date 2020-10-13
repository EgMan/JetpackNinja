using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public float spinSpeed = 2f;
    public string nextLevel = "";
    private void FixedUpdate()
    {
            Vector3 euler = transform.rotation.eulerAngles;
            euler.y += spinSpeed;
			transform.rotation = Quaternion.Euler(euler);
    }
    private void OnTriggerEnter(Collider other) {
        print("EG");
        if (other.CompareTag("Player"))
        {
            if (nextLevel.Length == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
