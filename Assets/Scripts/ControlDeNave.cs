using UnityEngine;

public class ControlDeNave : MonoBehaviour
{
    // Aumenté un poco la rapidez base porque en 3D las distancias son mayores
    public float rapidez = 20f;
    public float velocidadRotacion = 60f; // Nueva variable para controlar qué tan rápido gira

    Rigidbody rigidbody;
    Transform transform;
    AudioSource audioSource;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        ProcesarInput();
    }

    private void ProcesarInput()
    {
        // Movimientos de la nave
        Propulsion();      // Ir arriba (Espacio)
        MovimientoFrontal(); // Ir adelante/atras (W y S) - NUEVO
        Rotaciones();      // Girar lados (A y D)
        Estabilizacion(); // Mantener nivelada la nave
    }

    private void Propulsion()
    {
        // Mantenemos tu lógica original: Espacio para subir
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * rapidez * Time.deltaTime * 50); // Ajusté el multiplicador para que tenga fuerza

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

    private void MovimientoFrontal() // NUEVA FUNCIÓN (Siguiendo tu estilo)
    {
        // Tecla W para avanzar hacia donde mira la nave
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.forward * rapidez * Time.deltaTime * 50);
        }
        // Tecla S para frenar o retroceder
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddRelativeForce(Vector3.back * rapidez * Time.deltaTime * 50);
        }
    }

    private void Rotaciones()
    {
        // CAMBIO CLAVE PARA 3D:
        // En tu código usabas .z (eso hace volteretas). 
        // Aquí usamos Vector3.up (Eje Y) para que gire como un auto/avión.

        if (Input.GetKey(KeyCode.D))
        {
            // Girar a la derecha
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Girar a la izquierda
            transform.Rotate(Vector3.down * velocidadRotacion * Time.deltaTime);
        }
    }

    // --- NUEVA FUNCIÓN DE ESTABILIZACIÓN ---
    private void Estabilizacion()
    {
        if (Input.GetKey(KeyCode.E)) // Tecla E para emergencias
        {
            // 1. DETENER GIROS: Mata instantáneamente cualquier rotación física loca
            rigidbody.angularVelocity = Vector3.zero;

            // 2. ENDEREZAR: 
            // Obtenemos hacia dónde está mirando la nave actualmente (Y)
            float rotacionYActual = transform.eulerAngles.y;

            // Forzamos la rotación para que X y Z sean 0 (plana), pero conservamos la Y
            transform.rotation = Quaternion.Euler(0, rotacionYActual, 0);

            // Opcional: Si quieres que también se frene en el aire, descomenta la siguiente línea:
            // rigidbody.linearVelocity = Vector3.zero; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Tu lógica de colisiones intacta
        switch (collision.gameObject.tag)
        {
            case "ColisionSegura":
                print("Colision Segura...");
                break;
            case "Combustible":
                print("Combustible...");
                break;
            case "LimiteMundo": 
                print("¡Estás yendo demasiado lejos! Regresa.");
                rigidbody.linearVelocity = Vector3.zero;
                break;
            default:
                print("Estas muerto...!!!");
                break;
        }
    }
}