using System Collections;
using System Collections Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float speed = 5.0f;
	private float turnSpeed = 25.0f;
	private float leftInput;
	private float rightInput;
	
	//Start is called before the first frame update
	void Start()
	{
		//Contition before the first move of the whole game
	}
	//Update is called once per frame
	void Update()
	{
		//Get the player input
		leftInput = Input.GetAxis("Left");
		rightInput = Input.GetAxis("Right");

		//Rotate the object using unity engine functions
		transform.Translate(Vector3.up * turnSpeed * leftInput);
		transform.Translate(Vector3.up * turnSpeed * rightInput);
	}
}
