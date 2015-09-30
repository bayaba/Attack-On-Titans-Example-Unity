using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{
    LineRenderer rope;

    static float Length = 15f;

    void Start()
    {
        rope = GetComponent<LineRenderer>();
    }

	void Shoot()
    {
        LeanTween.value(gameObject, 0f, Length, 0.3f).setOnUpdate(Stretch).setEase(LeanTweenType.easeOutCubic).setOnComplete(StretchComplete);
        LeanTween.value(gameObject, Length, 0f, 0.1f).setOnUpdate(Stretch).setEase(LeanTweenType.easeInCubic).setOnComplete(StretchEnd).setDelay(0.5f);
	}

    void Stretch(float value)
    {
        float len = Length / 10f;

        rope.SetPosition(0, new Vector3(0, 0, 0));
        rope.SetPosition(1, new Vector3(Random.Range(-len, len), Random.Range(-len, len), value * 0.1f));
        rope.SetPosition(2, new Vector3(Random.Range(-len, len), Random.Range(-len, len), value * 0.5f));
        rope.SetPosition(3, new Vector3(Random.Range(-len, len), Random.Range(-len, len), value * 0.8f));
        rope.SetPosition(4, new Vector3(Random.Range(-len, len), Random.Range(-len, len), value * 0.9f));
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
