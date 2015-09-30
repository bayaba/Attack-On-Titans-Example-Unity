using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour
{
    void Awake()
    {
        float scale = Random.Range(2f, 4f);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Global.ScrollSpeed * Time.deltaTime);

        if (transform.position.z < -1f)
        {
            float scale = Random.Range(2f, 4f);
            transform.localScale = new Vector3(scale, scale, scale);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 60f);
        }
    }
}
