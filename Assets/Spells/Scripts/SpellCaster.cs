using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class SpellCaster : MonoBehaviour
{
    private Transform playerCam;
    private Transform orientation;
    private Rigidbody rb;

    public string[] spellNames;

    [Space]
    [Header("Fire Ball")]
    public GameObject fireBall;
    public float fbDamage = 10;
    public float fbSizeMultiplier = 10;
    public float fbStartSpeed = 10;
    public float fbDuration = 5f;

    [Space]
    [Header("Misty Step")]
    public GameObject mistParticles;
    public float msDistance = 30f;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        orientation = GameObject.FindGameObjectWithTag("Orientation").transform;
        playerCam = Camera.main.transform.parent;
    }

    private void Update()
    {

    }

    public void Cast(string spellName, ConfidenceLevel m_confidence)
    {
        int confidence = ConfidenceToInt(m_confidence);
        if(!spellNames.Contains(spellName))
        {
            Debug.LogWarning(spellName + " is not a valid spell");
            return;
        }

        else if (spellName == "fireball")
        {
            FireBallSpell(confidence);
        }

        else if (spellName == "misty step")
        {
            MistyStepSpell(confidence);
        }
    }

    void FireBallSpell(int confidence)
    {
        Debug.Log("Fireball cast: " + confidence);
        GameObject node = Instantiate(fireBall, playerCam.position + (playerCam.forward * 1.5f), Quaternion.identity);
        node.GetComponent<Fireball>().startSize = fbSizeMultiplier * confidence;
        node.GetComponent<Fireball>().startSpeed = fbStartSpeed;
        node.GetComponent<Fireball>().duration = fbDuration;
        node.GetComponent<Fireball>().damage = fbDamage / 3 * confidence;
        node.GetComponent<Fireball>().playerFwd = playerCam.forward;
    }

    void MistyStepSpell(int confidence)
    {
        float distance = msDistance / 3 * confidence;
        Vector3 desiredPos = transform.position + (orientation.transform.forward * distance);

        if (rb.linearVelocity != Vector3.zero)
        {
            desiredPos = transform.position + new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.y) * distance;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, desiredPos - transform.position, out hit, msDistance))
        {
            desiredPos = hit.point;
        }
        Debug.Log("OLD:" + transform.position.ToString());
        Debug.Log("NEW:" + desiredPos.ToString());
        rb.transform.position = desiredPos;
    }

    private int ConfidenceToInt(ConfidenceLevel confidence)
    {
        int output = 0;

        if (confidence == ConfidenceLevel.Low)
        {
            output = 1;
        }
        else if (confidence == ConfidenceLevel.Medium)
        {
            output = 2;
        }
        else if (confidence == ConfidenceLevel.High)
        {
            output = 3;
        }

        return output;
    }
}
