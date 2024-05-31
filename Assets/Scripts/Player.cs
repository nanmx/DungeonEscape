using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private LayerMask _groundLayer;
    private bool _resetJump=false;
    // Start is called before the first frame update
    void Start()
    {
        _rigid= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float move= Input.GetAxis("Horizontal");
         if (Input.GetKeyDown(KeyCode.Space) && _grounded == true) {
        _rigid.velocity=new Vector2(_rigid.velocity.x, _jumpForce);
        
            _grounded = false;
             _resetJump=true;
             StartCoroutine(ResetJumpRoutine());
        }
        RaycastHit2D hitInfo=Physics2D.Raycast(transform.position, Vector2.down,0.06f,1<<8);
        Debug.DrawRay(transform.position, Vector2.down* 0.06f , Color.green);
        if(hitInfo.collider != null ) {
            Debug.Log("Hit: "+hitInfo.collider.name); 
            if(_resetJump==false)
                _grounded= true;
           
        }
       
        
        _rigid.velocity= new Vector2(move, _rigid.velocity.y);
    }
    IEnumerator ResetJumpRoutine(){
        yield return new WaitForSeconds(0.1f);
        _resetJump =false;

    }
}
