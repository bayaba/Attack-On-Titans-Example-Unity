using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Dummy : MonoBehaviour
{
    float timer, JumpPower;
    bool isRoate = false;

    public GameObject Sparks;

    Transform LeftHand, RightHand;
    
    void Start()
    {
        LeftHand = transform.FindChild("Body/LeftHand");
        RightHand = transform.FindChild("Body/RightHand");
    }

	void Update ()
    {
        if (Time.time - timer >= 2.0f && Random.Range(0, 100) == 0 && !isRoate)
        {
            timer = Time.time;
            audio.Play();
            JumpPower = 0.5f + Random.Range(0f, 1.5f);
            LeftHand.SendMessage("Shoot"); // shoot the left rope
            RightHand.SendMessage("Shoot"); // shoot the righr rope

            CancelInvoke("DummyJump");
            Invoke("DummyJump", 0.3f);
        }
    }

    void DummyJump()
    {
        bool take = false;

        RaycastHit hit;
        if (Physics.Raycast(LeftHand.position, LeftHand.forward, out hit, 20f)) take = true;
        if (Physics.Raycast(RightHand.position, RightHand.forward, out hit, 20f)) take = true;

        if (take)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.AddForce(Vector3.up * Mathf.Min(1200f, JumpPower * 1200f));

            if (JumpPower >= 0.8f)
            {
                LeftHand.GetComponentInChildren<ParticleSystem>().Play();
                RightHand.GetComponentInChildren<ParticleSystem>().Play();

                transform.DOMoveX(Random.Range(-13f, 13f), 6f).SetSpeedBased();

                GameObject temp = Instantiate(Sparks) as GameObject;
                temp.transform.parent = transform;
                temp.transform.localPosition = new Vector3(0, 1.5f, -0.5f);
            }
        }
        JumpPower = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            LeanTween.cancel(gameObject);
        }
    }
}
