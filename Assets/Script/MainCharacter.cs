using System.Collections;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Joystick joystick;
    public GameObject bullet;
    public Transform firingPoint;

    private Animator anim;
    private Transform tempFiringPoint;
    private AudioSource firiingAudio;

    void Awake()
    {
        anim = GetComponent<Animator>();
        firiingAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (moveVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }

    public void Firing()
    {
        if (anim.GetBool("Run"))
        {
            // check if not running and attacking
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run02_Attack"))
            {
                anim.Play("Run02_Attack");
                StartCoroutine(RunAttackCoroutine(0.1f));
            }
        }
        else
        {
            // check if not attacking
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.Play("Attack");
                StartCoroutine(AttackCoroutine(0.2f));
            }
        }
    }

    IEnumerator AttackCoroutine(float time)
    {
        // delay to get the actual firing point
        yield return new WaitForSeconds(time);
        tempFiringPoint = firingPoint.transform;

        firiingAudio.Play();
        Instantiate(bullet, tempFiringPoint.position, tempFiringPoint.rotation);

        yield return new WaitForSeconds(time);
        firiingAudio.Play();
        Instantiate(bullet, tempFiringPoint.position, tempFiringPoint.rotation);

        yield return new WaitForSeconds(time);
        firiingAudio.Play();
        Instantiate(bullet, tempFiringPoint.position, tempFiringPoint.rotation);
    }

    IEnumerator RunAttackCoroutine(float time)
    {
        // delay to get the actual firing point
        yield return new WaitForSeconds(time);
        tempFiringPoint = firingPoint.transform;
        firiingAudio.Play();
        Instantiate(bullet, tempFiringPoint.position, tempFiringPoint.rotation);
    }
}