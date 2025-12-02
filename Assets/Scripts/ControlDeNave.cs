using UnityEngine;

public class ControlDeNave : MonoBehaviour
{

    public float rapidez = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audioSource;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarInput();
        //Debug.Log(Time.deltaTime + "seg. " + (1.0f / Time.deltaTime) + " FPS");
    }

    private void ProcesarInput()
    {
        //Movimientos de la nave
        Propulsion();
        Rotaciones();
    }
    private void Propulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //transform.Translate(Vector3.up * rapidez);
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Rotaciones()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.left * rapidez);
            var rotarDerecha = transform.rotation;
            rotarDerecha.z -= Time.deltaTime * 1;
            transform.rotation = rotarDerecha;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.right * rapidez);
            var rotarIzquierda = transform.rotation;
            rotarIzquierda.z += Time.deltaTime * 1;
            transform.rotation = rotarIzquierda;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ColisionSegura":
                print("Colision Segura...");
                break;
            case "Combustible":
                print("Combustible...");
                break;
            default:
                print("Estas muerto...!!!");
                break;

        }

        /*if (collision.gameObject.CompareTag("ColisionSegura"))
        {
            print("Colision Segura...");
        }
        else if (collision.gameObject.CompareTag("ColisionPeligrosa"))
        {
            print("Colision Peligrosa...!!!");
        }
        print("Choque...");*/
    }

}
