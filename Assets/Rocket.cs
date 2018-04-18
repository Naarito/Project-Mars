using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
	Rigidbody rigidbody; //Cria uma variável global do tipo Rigidbody
	AudioSource audiosource; // Cria uma variável global do tipo AudioSource
    [SerializeField]float rThrust; //Cria um campo float editavel dentro do unity (Giro)
    [SerializeField]float mThrust; //Cria um campo float editavel dentro do unity (Aceleração)
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelFinish;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem levelFinishParticles;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;

	void Start () {
		rigidbody = GetComponent<Rigidbody>(); //Age apenas em componentes do tipo Rigidbody
		audiosource = GetComponent<AudioSource>(); //Age apenas em compentes do tipo AudioSource
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Alive){
            RespondToThrust(); //Invoca a função que processa a entrada Espaço
		    RespondToRotate(); //Invoca a função que processa entradas Esq e Dir
        }
	}

    void OnCollisionEnter(Collision collision) {
        
        if(state != State.Alive)
            return; //Só computa colisões se estiver vivo

        switch (collision.gameObject.tag){
            case "Friendly":
                print("Okay.");
                break;

            case "Finish":
                FinishSequence();
                break;

            default:
                DeathSequence();
                break;
        }
    }

    private void DeathSequence()
    {
        print("Dead!");
        audiosource.Stop();
        audiosource.PlayOneShot(death);
        deathParticles.Play();
        state = State.Dying;
        Invoke("LoadFirstLevel", 1f);
    }

    private void FinishSequence()
    {
        print("Level Finished!");
        audiosource.Stop();
        audiosource.PlayOneShot(levelFinish);
        levelFinishParticles.Play();
        state = State.Transcending;
        Invoke("LoadNextScene", 1f); //Carrega a proxima fase após 1 segundo
    }


    private void RespondToThrust()
    {
        float mainThisFrame = mThrust * Time.deltaTime; //normaliza a aceleração com base nos frames

        if (Input.GetKey(KeyCode.UpArrow))
        { //se a tecla está pressionada e a nave está viva
            ApplyThrust(mainThisFrame);
        }
        else // Se espaço não estiver pressionado
        {
            audiosource.Stop(); //Parar audio
            mainEngineParticles.Stop();
        }

    }

    private void ApplyThrust(float mainThisFrame)
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThisFrame); //Gera uma força relativa ao objeto
        print("Propulsion");
        if (!audiosource.isPlaying) //Se audio não estiver tocando
        {
            audiosource.PlayOneShot(mainEngine); //Tocar audio
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotate()
    {
        if(state == State.Alive) {
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

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

}
