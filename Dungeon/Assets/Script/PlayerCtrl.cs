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
        
        //animation
        if(_horizontal !=0 || _vertical != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        
        Vector3 _rotate = new Vector3(0f, _horizontal, 0f);
        Vector3 _movement = transform.forward * _vertical;

        _rotate = _rotate * rotationSpeed;
        _movement = _movement.normalized * applySpeed;

        rigidbody.MovePosition(transform.position + _movement * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(_rotate));
        

    }
}
