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
    private SpriteRenderer _swordArcSprite;
    // Start is called before the first frame update
    void Start()
    {
        
        _rigid= GetComponent<Rigidbody2D>();
        _playerAnimation= GetComponent<PlayerAnimation>();
        _playerSprite =GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite= transform.GetChild(1).GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update(){
        Movement();
        if(Input.GetMouseButtonDown(0)&& IsGrounded()==true){

            _playerAnimation.Attack();
        }
       
       
        
        
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
            _playerAnimation.Jump(true);
             StartCoroutine(ResetJumpRoutine());
        }
        if(IsGrounded()==true) _playerAnimation.Jump(false);
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnimation.Move(move);
        

    }
    bool IsGrounded(){
         RaycastHit2D hitInfo=Physics2D.Raycast(transform.position, Vector2.down,0.06f,1<<8);
        Debug.DrawRay(transform.position, Vector2.down* 0.06f , Color.green);
        if(hitInfo.collider != null ) {
            if(_resetJump==false){
                
                return true;
            
            }           
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
            _swordArcSprite.flipY=false;
            _swordArcSprite.flipX = false; 
            Vector3 newpos= _swordArcSprite.transform.position;
            newpos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newpos;
        }else{
            _playerSprite.flipX=true;
            _swordArcSprite.flipY = true;
            _swordArcSprite.flipX = true;
            Vector3 newpos = _swordArcSprite.transform.position;
            newpos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newpos;
        }

    }
}
