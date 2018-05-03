using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator6_1R : MonoBehaviour {

	Vector3 movementVector = new Vector3 (0f,0f,-20f); //Vetor de deslocamento, usar [SerializeField] Vector3 movementVector; para experimentar outros vetores
	float period = 5f; //Periodo de 8 segundos
	Vector3 startingPos;

	void Start () {
		startingPos = transform.position;
	}
	
	void Update () {
		if (period <= Mathf.Epsilon) return; //Cria uma condição onde o periodo não pode ser inferior a zero, epsilon representa do menor float possível

		float cycles = Time.time / period; //Cresce continuamente, a partir de zero.

		const float tau = Mathf.PI * 2; //6.28 ou 2Pi, circulo completo
		float rawSinWave = Mathf.Sin(cycles * tau); // multiplica o numero do ciclo por uma volta completa senoidal
		float movementFactor = (rawSinWave + 1)/2; //pega o valor de -1 a 1, transforma em 0 a 2 e divide por 0.5, resultando em 0 a  1

		Vector3 offset = movementVector * movementFactor; //A cada update armazena o valor do fator e multiplica pelo vetor de movimento
		transform.position = startingPos + offset; //soma a posição inicial o valor do offset gerado na linha acima
	}
}
