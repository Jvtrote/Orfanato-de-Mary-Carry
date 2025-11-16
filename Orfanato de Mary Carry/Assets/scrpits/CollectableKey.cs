using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    // Câmera do jogador (arraste a Main Camera aqui pelo Inspector)
    public Transform playerCamera;

    [Header("Configuração de Posição da Chave")]
    // Posição local onde a Chave deve ficar (canto inferior esquerdo da câmera)
    // Valores negativos em X movem o objeto para a ESQUERDA da câmera.
    public Vector3 displayPosition = new Vector3(-0.4f, -0.3f, 0.7f);
    // Rotação local da Chave (para que ela fique virada corretamente)
    public Vector3 displayRotation = new Vector3(0f, 0f, 0f);

    private bool playerIsNear = false;
    private bool isCollected = false;
    public KeyCode interactionKey = KeyCode.F; // Tecla para interagir

    void Start()
    {
        // Garante que a Main Camera seja encontrada se não for definida
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        // Verifica se o jogador está perto, se a chave não foi coletada e se a tecla de interação foi pressionada.
        if (playerIsNear && !isCollected && Input.GetKeyDown(interactionKey))
        {
            CollectObject();
        }
    }

    void CollectObject()
    {
        // 1. Torna a Chave filha da câmera do jogador
        // Isso faz com que ela se mova e gire com a câmera.
        transform.SetParent(playerCamera);

        // 2. Define a posição e rotação local (relativa à câmera)
        // Isso a posiciona no canto inferior esquerdo.
        transform.localPosition = displayPosition;
        transform.localRotation = Quaternion.Euler(displayRotation);

        // 3. Define o objeto como coletado
        isCollected = true;
        playerIsNear = false;

        // Opcional: Desativa componentes para que a chave não interaja mais com o mundo
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Debug.Log("Chave coletada e posicionada!");
    }

    // --- Lógica de Proximidade (Trigger) ---
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou é o jogador e se a chave não foi coletada
        if (other.CompareTag("MainCamera") && !isCollected)
        {
            playerIsNear = true;
            Debug.Log("Pressione F para pegar a Chave.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") && !isCollected)
        {
            playerIsNear = false;
        }
    }
}