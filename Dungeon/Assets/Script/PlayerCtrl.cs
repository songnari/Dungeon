using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private bool isRun = false;

    private Rigidbody rigidbody;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        
        if(_horizontal ==0 && _vertical == 0)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            return;
        }

        // Run check
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("Running", true);
            applySpeed = runSpeed;
        }
        else
        {
            animator.SetBool("Walking", true);
            applySpeed = walkSpeed;
        }


        Vector3 _movement = new Vector3(_horizontal, 0, _vertical);
        Quaternion _rot = Quaternion.LookRotation(_movement);

        _movement = _movement.normalized * applySpeed * Time.deltaTime;
        _rot = Quaternion.Slerp(transform.rotation, _rot, rotationSpeed * Time.deltaTime);

        rigidbody.MovePosition(transform.position + _movement);
        rigidbody.MoveRotation(_rot);

    }
}
