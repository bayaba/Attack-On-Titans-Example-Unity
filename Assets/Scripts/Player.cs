using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    float timer, JumpPower;
    float JumpedPower;
    bool click = false, isRotate = false, isFirst = false;

    public GameObject Sparks;

    Transform Body, CameraPoint, LeftHand, RightHand;
    
    public Text DistanceText;
    float Distance = 0f;

    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));

        Body = transform.FindChild("Body");
        CameraPoint = transform.FindChild("CameraPoint");
        LeftHand = transform.FindChild("Body/LeftHand");
        RightHand = transform.FindChild("Body/RightHand");

        LeanTween.moveLocal(CameraPoint.gameObject, new Vector3(0f, 0f, 0f), 2.5f).setEase(LeanTweenType.easeOutQuad).setDelay(1.0f);
        LeanTween.rotateLocal(CameraPoint.gameObject, new Vector3(0f, 0f, 0f), 2.5f).setEase(LeanTweenType.easeOutQuad).setDelay(1.0f);
    }

	void Update()
    {
        Distance += Global.ScrollSpeed * Time.deltaTime;
        DistanceText.text = "DISTANCE  " + (int)Distance + "M";

        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel("Title");

        if (Input.GetMouseButton(0))
        {
            if (!click && JumpPower == 0)
            {
                timer = Time.time;
                click = true;
            }
        }
        else
        {
            if (click)
            {
                audio.Play();
                JumpPower = JumpedPower = Time.time - timer;
                LeftHand.SendMessage("Shoot");
                RightHand.SendMessage("Shoot");
                Invoke("DoJumpPlayer", 0.3f);
            }
            click = false;
        }

        if (Input.GetMouseButton(1))
        {
            if (rigidbody.velocity.y != 0)
            {
                rigidbody.AddRelativeForce(Vector3.up * 10f);
            }
        }
    }

    void ZoomIn()
    {
        LeanTween.value(gameObject, Global.ScrollSpeed, 10f, JumpedPower * 1.0f).setOnUpdate(
            (float value)=>{
                Global.ScrollSpeed = value;
            }
        ).setEase(LeanTweenType.easeOutCubic);
    }

    void DoJumpPlayer()
    {
        bool take = false;

        RaycastHit hit;
        if (Physics.Raycast(LeftHand.position, LeftHand.forward, out hit, 20f)) take = true;
        if (Physics.Raycast(RightHand.position, RightHand.forward, out hit, 20f)) take = true;

        if (take && !isRotate)
        {
            if (Global.ScrollSpeed < 20f)
            {
                float power = Global.ScrollSpeed + (Time.time - timer) * 10f;
                if (power > 20f) power = 20f; // limit power

                if (JumpPower >= 0.5f)
                {
                    LeanTween.value(gameObject, Global.ScrollSpeed, power, 0.5f).setOnComplete(ZoomIn).setOnUpdate(
                        (float value) =>
                        {
                            Global.ScrollSpeed = value;
                        }
                    ).setEase(LeanTweenType.easeOutCubic);
                }
            }

            if (JumpPower >= 0.5f)
            {
                LeftHand.GetComponentInChildren<ParticleSystem>().Play();
                RightHand.GetComponentInChildren<ParticleSystem>().Play();
            }
            if (rigidbody.velocity.y < 0) rigidbody.velocity = new Vector3(0, rigidbody.velocity.y * 0.3f, 0);
            rigidbody.AddRelativeForce(Vector3.up * Mathf.Min(1000f, JumpPower * 1000f));

            if (JumpPower >= 0.8f)
            {
                GameObject temp = Instantiate(Sparks) as GameObject;
                temp.transform.parent = transform;
                temp.transform.localPosition = new Vector3(0, 1.5f, -0.5f);
            }
        }
        JumpPower = 0;
    }

    void RotatePlayer()
    {
        if (!isRotate)
        {
            isRotate = true;

            LeftHand.GetComponentInChildren<ParticleSystem>().Stop();
            RightHand.GetComponentInChildren<ParticleSystem>().Stop();

            if (!isFirst)
            {
                Time.timeScale = 0.5f;

                LeanTween.moveLocal(CameraPoint.gameObject, new Vector3(0.9f, 0.9f, 0f), 0.5f).setEase(LeanTweenType.easeOutQuad);
                LeanTween.rotateLocal(CameraPoint.gameObject, new Vector3(0, 320f, 0), 0.5f).setEase(LeanTweenType.easeOutQuad);
            }
            LeanTween.rotateAround(Body.gameObject, Vector3.right, 360f, 1.0f).setEase(LeanTweenType.easeOutQuad).setOnComplete(RotateComplete);
        }
    }

    void RotateComplete()
    {
        if (isRotate)
        {
            Body.rotation = Quaternion.identity;

            if (!isFirst)
            {
                Time.timeScale = 1.0f;
                LeanTween.moveLocal(CameraPoint.gameObject, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutQuad);
                LeanTween.rotateLocal(CameraPoint.gameObject, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutQuad);
                isFirst = true;
            }
            isRotate = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            RotateComplete();
        }
    }
}
