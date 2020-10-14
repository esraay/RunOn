using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [System.Serializable]
    public class MovementSettings{

        public static float forwardVelocity = 0;

    }

    public Animator anim;

    public MovementSettings movementSettings = new MovementSettings();

    public Transform player;
    private Vector3 _velocity;
    private Rigidbody _rigidbody;
    private Vector3 _target = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _velocity = Vector3.zero;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
        _rigidbody.velocity = _velocity;
        _target = new Vector3(player.position.x, 0.4f, player.position.z);
        transform.position = Vector3.Lerp(transform.position, _target, 1f);
       
    }

    void Update()
    {
        anim.SetFloat("speed", PlayerControl.MovementSettings.forwardVelocity);
    }

    void Run()
    {
        _velocity.z = -1*MovementSettings.forwardVelocity;
    }
}
