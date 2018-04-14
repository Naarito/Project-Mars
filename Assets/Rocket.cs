using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	
	Rigidbody rigidbody; //Cria uma variável global do tipo Rigidbody
	
	AudioSource audiosource; // Cria uma variável global do tipo AudioSource
	
    [SerializeField]float rThrust; //Cria um campo float editavel dentro do unity (Giro)
    
    [SerializeField]float mThrust; //Cria um campo float editavel dentro do unity (Aceleração)
	void Start () {
		rigidbody = GetComponent<Rigidbody>(); //Age apenas em componentes do tipo Rigidbody
		audiosource = GetComponent<AudioSource>(); //Age apenas em compentes do tipo AudioSource
	}
	
	// Update is called once per frame
	void Update () {
		Thrust(); //Invoca a função que processa a entrada Espaço
		Rotate(); //Invoca a função que processa entradas Esq e Dir
	}

	private void Thrust()
    {
        float mainThisFrame = mThrust * Time.deltaTime; //normaliza a aceleração com base nos frames

        if (Input.GetKey(KeyCode.UpArrow))
        { //se a tecla está pressionada
            rigidbody.AddRelativeForce(Vector3.up * mainThisFrame); //Gera uma força relativa ao objeto
            print("Propulsion");
            if (!audiosource.isPlaying) //Se audio não estiver tocando
            {
                audiosource.Play(); //Tocar audio
            }
        }
        else // Se espaço não estiver pressionado
        {
            audiosource.Stop(); //Parar audio
        }
    }

    private void Rotate()
    {
        
		rigidbody.freezeRotation = true; //Controle manual de rotação 
        
        float rotationThisFrame = rThrust * Time.deltaTime; //normaliza a aceleração com base nos frames

		if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame); //Força na direção positiva de Z (Regra de fluxo da mão esquerda)
            print("Left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
            print("Right");
        }

		rigidbody.freezeRotation = false; //Controle físico da rotação

    }

}
