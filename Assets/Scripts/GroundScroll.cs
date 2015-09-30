using UnityEngine;
using System.Collections;

public class GroundScroll : MonoBehaviour
{
    float offset = 0f;
    public float Scroll = 0f;

	void Update ()
    {
        offset += Time.deltaTime * (Global.ScrollSpeed / Scroll);
        renderer.material.mainTextureOffset = new Vector3(0, offset, 0);
	}
}
