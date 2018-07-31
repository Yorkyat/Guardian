using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Joystick joystick;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

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

    public void Firing()
    {
        if (anim.GetBool("Run"))
        {
            anim.Play("Run02_Attack");
        }
        else
        {
            anim.Play("Attack");
        }
    }
}