using UnityEngine;

public class ConfigVenus : MonoBehaviour
{
    [Header("Física del Planeta")]
    public Vector3 gravedad = new Vector3(0, -9.81f, 0);
    [Tooltip("Resistencia del aire: 0 para vacío, 2 o más para atmósfera densa")]
    public float densidadAtmosfera = 2f; // Ahora es variable

    [Header("Visuales (Niebla)")]
    public bool activarNiebla = true;
    public Color colorNiebla = Color.yellow;
    public float densidadNieblaVisual = 0.08f;

    void Start()
    {
        AplicarFisica();
        //AplicarVisuales();
    }

    void AplicarFisica()
    {
        // 1. Gravedad Global
        Physics.gravity = gravedad;

        // 2. Resistencia en la Nave (El "Efecto Sopa" de Venus)
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            // Asignamos el valor que pongas en el Inspector
            jugador.GetComponent<Rigidbody>().linearDamping = densidadAtmosfera;
        }
    }

    void AplicarVisuales()
    {
        RenderSettings.fog = activarNiebla;
        RenderSettings.fogColor = colorNiebla;
        RenderSettings.fogDensity = densidadNieblaVisual;

        // En Venus, la luz ambiente es fuerte porque rebota en las nubes
        if (activarNiebla)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = colorNiebla;
        }
    }
}