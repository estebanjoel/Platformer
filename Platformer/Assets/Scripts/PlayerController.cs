using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //walking spead
    public float walkSpeed;
    //jumping speed
    public float jumpForce;
    public bool isPressedJump;
	public Rigidbody rb;
    Collider col;
    Vector3 playerSize;
    public AudioSource coinAudio;
    //camera distance in z
    public float cameraDistanceZ = 6;
	void Start () {
        rb=GetComponent<Rigidbody>();
        col=GetComponent<Collider>();
        //get player size
        playerSize = col.bounds.size;
        //set the camera position
        CameraFollowPlayer();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        walkHandler(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        jumpHandler(Input.GetAxis("Jump"));
        CameraFollowPlayer();
    }

    //takes care of walking logic
    public void walkHandler(float hAxis, float vAxis){
        Vector3 movement = new Vector3(hAxis * walkSpeed * Time.deltaTime, 0, vAxis * walkSpeed * Time.deltaTime);
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);
        if(hAxis!=0||vAxis!=0){
            Vector3 direction = new Vector3(hAxis,0,vAxis);
            // transform.forward=direction;
            rb.rotation = Quaternion.LookRotation(direction);
        }
    }

    //takes care of jumping logic
    public void jumpHandler(float jumpAxis){

        if(jumpAxis>0){

            bool isGrounded = CheckGrounded();
            if(!isPressedJump && isGrounded){
                isPressedJump=true;
                if(rb.velocity.y==0){
                    Vector3 jumpVector = new Vector3(0,jumpAxis * jumpForce,0);
                    rb.AddForce(jumpVector,ForceMode.VelocityChange);
                }
            }
        }

        else{
            isPressedJump=false;
        }
    }

    public bool CheckGrounded(){

        //location of all 4 corners

        Vector3 corner1=transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner2=transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner3=transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);
        Vector3 corner4=transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);

        //check if we are grounded

        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.01f);

        return(grounded1 || grounded2 || grounded3 || grounded4);
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Coin")){

            GameManager.instance.IncreaseScore(other.GetComponent<CoinController>().value);
            coinAudio.Play();
            Destroy(other.gameObject);
        }

        else if(other.CompareTag("Enemy")){
            //Reset the Game
            GameManager.instance.GameOver();
        }
        
        else if(other.CompareTag("Goal")){
            //Sebd player to the next level
            GameManager.instance.IncreaseLevel();
        }
    }

    public void CameraFollowPlayer(){

        //grab the camera
        Vector3 cameraPosition = Camera.main.transform.position;
        //modify its position according to cameraDistanceZ
        cameraPosition.z=transform.position.z-cameraDistanceZ;
        //set the camera position
        Camera.main.transform.position=cameraPosition;
    
    }
}
