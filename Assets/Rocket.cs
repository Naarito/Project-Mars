using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	
	Rigidbody rigidbody; //Cria uma variável global do tipo Rigidbody

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>(); //Age apenas em componentes do tipo Rigidbody
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput(); //Invoca a função que processa entradas a cada update
	}

    private void ProcessInput()
    {
        
		if(Input.GetKey(KeyCode.Space)) { //se a tecla está pressionada
			rigidbody.AddRelativeForce(Vector3.up); //Gera uma força relativa ao objeto
			print("Propulsão");
		}
		 if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.forward);
			print("Esquerda");
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.back);
			print("Direita");
		}
		
    }
}
