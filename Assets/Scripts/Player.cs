using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _resetJump=false;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        
        _rigid= GetComponent<Rigidbody2D>();
        _playerAnimation= GetComponent<PlayerAnimation>();
        _playerSprite =GetComponentInChildren<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update(){
        Movement();
       
       
       
        
        
    }
   
    void Movement(){
         float move= Input.GetAxis("Horizontal");
        if(move>0){
            Flip(true);
        }else if(move<0){
            Flip(false);
        }
         if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true) {
            _rigid.velocity=new Vector2(_rigid.velocity.x, _jumpForce);
        
             StartCoroutine(ResetJumpRoutine());
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnimation.Move(move);

    }
    bool IsGrounded(){
         RaycastHit2D hitInfo=Physics2D.Raycast(transform.position, Vector2.down,0.06f,1<<8);
        Debug.DrawRay(transform.position, Vector2.down* 0.06f , Color.green);
        if(hitInfo.collider != null ) {
            if(_resetJump==false)return true;
           
        }
        return false;
        

    }
     IEnumerator ResetJumpRoutine(){
        _resetJump =true;
        yield return new WaitForSeconds(0.1f);
        _resetJump =false;

    }
    void Flip(bool FaceRigth){
        if(FaceRigth){
            _playerSprite.flipX=false;
        }else{
            _playerSprite.flipX=true;
        }

    }
}
