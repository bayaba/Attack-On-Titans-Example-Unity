using UnityEngine;
using System.Collections;

public class GroundScroll : MonoBehaviour
{
    float offset = 0f;

	void Update ()
    {
        offset += Time.deltaTime * (Global.ScrollSpeed / 5f);
        renderer.material.mainTextureOffset = new Vector3(0, offset, 0);
	}
}
