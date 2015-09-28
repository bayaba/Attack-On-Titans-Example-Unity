using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    float timer, JumpPower;
    bool click = false, isRoate = false;

    public GameObject Sparks, Effects;

    Transform LeftHand, RightHand;
    
    public TextMesh DistanceText;
    float Distance = 0f;

    Camera MainCamera;

    void Start()
    {
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

        LeftHand = transform.FindChild("LeftHand");
        RightHand = transform.FindChild("RightHand");
    }

	void Update ()
    {
        Distance += Global.ScrollSpeed * Time.deltaTime;
        DistanceText.text = "DISTANCE  " + (int)Distance + "M";

        if (Input.GetMouseButton(0))
        {
            if (!click && JumpPower == 0 && !isRoate)
            {
                timer = Time.time;
                click = true;
            }
        }
        else
        {
            if (click && !isRoate)
            {
                JumpPower = Time.time - timer;
                LeftHand.SendMessage("Shoot");
                RightHand.SendMessage("Shoot");
                Invoke("SetJump", 0.3f);
            }
            click = false;
        }
        MainCamera.fieldOfView = 80f + Mathf.Min(40f, Global.ScrollSpeed * 2f);
    }

    void ZoomIn()
    {
        iTween.Stop("ValueTo");
        iTween.ValueTo(gameObject, iTween.Hash("from", Global.ScrollSpeed, "to", 10f, "time", 0.5f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "ValueToSpeed"));
    }

    void ValueToSpeed(float value)
    {
        Global.ScrollSpeed = value;
    }

    void SetJump()
    {
        bool take = false;

        RaycastHit hit;
        if (Physics.Raycast(LeftHand.position, LeftHand.forward, out hit, 20f)) take = true;
        if (Physics.Raycast(RightHand.position, RightHand.forward, out hit, 20f)) take = true;

        if (take)
        {
            if (Global.ScrollSpeed < 20f)
            {
                float power = Global.ScrollSpeed + (Time.time - timer) * 10f;
                if (power > 20f) power = 20f;

                iTween.Stop("ValueTo");
                iTween.ValueTo(gameObject, iTween.Hash("from", Global.ScrollSpeed, "to", power, "time", 0.2f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "ValueToSpeed", "oncomplete", "ZoomIn"));
            }
            rigidbody.AddForce(Vector3.up * Mathf.Min(1200f, JumpPower * 1200f));

            if (JumpPower >= 0.8f)
            {
                GameObject temp = Instantiate(Sparks) as GameObject;
                temp.transform.parent = transform;
                temp.transform.localPosition = new Vector3(0, 1.5f, -0.5f);

                temp = Instantiate(Effects) as GameObject;
                temp.transform.parent = transform;
                temp.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }
        JumpPower = 0;
    }

    void RotatePlayer()
    {
        isRoate = true;
        iTween.RotateAdd(gameObject, iTween.Hash("x", 360f, "time", 0.5f, "easetype", iTween.EaseType.easeInCirc, "oncomplete", "RotateComplete"));
    }

    void RotateComplete()
    {
        iTween.Stop("RotateAdd");
        transform.rotation = Quaternion.identity;
        isRoate = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            RotateComplete();
        }
    }
}
