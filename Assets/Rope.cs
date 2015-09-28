using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{
    LineRenderer rope;

    static float Length = 30f;

    void Start()
    {
        rope = GetComponent<LineRenderer>();
    }

	void Shoot()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", Length, "time", 0.3f, "onupdate", "Stretch", "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "StretchComplete"));
        iTween.ValueTo(gameObject, iTween.Hash("from", Length, "to", 0f, "time", 0.1f, "onupdate", "Stretch", "easetype", iTween.EaseType.easeInCubic, "oncomplete", "StretchEnd", "delay", 0.5f));
	}

    void Stretch(float value)
    {
        rope.SetPosition(0, new Vector3(0, 0, 0));
        rope.SetPosition(1, new Vector3(Random.Range(-3f, 3f), 0, value * 0.1f));
        rope.SetPosition(2, new Vector3(Random.Range(-3f, 3f), 0, value * 0.5f));
        rope.SetPosition(3, new Vector3(Random.Range(-3f, 3f), 0, value * 0.8f));
        rope.SetPosition(4, new Vector3(Random.Range(-3f, 3f), 0, value * 0.9f));
        rope.SetPosition(5, new Vector3(0, 0, value));
    }

    void StretchComplete()
    {
        rope.SetPosition(0, new Vector3(0, 0, 0));
        rope.SetPosition(1, new Vector3(0, 0, 0));
        rope.SetPosition(2, new Vector3(0, 0, 0));
        rope.SetPosition(3, new Vector3(0, 0, 0));
        rope.SetPosition(4, new Vector3(0, 0, 0));
        rope.SetPosition(5, new Vector3(0, 0, Length));
    }

    void StretchEnd()
    {
        rope.SetPosition(0, new Vector3(0, 0, 0));
        rope.SetPosition(1, new Vector3(0, 0, 0));
        rope.SetPosition(2, new Vector3(0, 0, 0));
        rope.SetPosition(3, new Vector3(0, 0, 0));
        rope.SetPosition(4, new Vector3(0, 0, 0));
        rope.SetPosition(5, new Vector3(0, 0, 0));
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * Length, Color.red);
	}
}
