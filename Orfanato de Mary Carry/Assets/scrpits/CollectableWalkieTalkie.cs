using UnityEngine;

public class CollectableWalkieTalkie : MonoBehaviour
{
    // O objeto da Câmera do jogador (arraste a Main Camera aqui pelo Inspector)
    public Transform playerCamera;

    [Header("Configurações na Mão")]
    // Posição local onde o Walkie Talkie deve ficar (canto inferior direito)
    public Vector3 handPosition = new Vector3(0.4f, -0.3f, 0.7f);
    // Rotação local quando na mão
    public Vector3 handRotation = new Vector3(0f, 0f, 0f);

    private bool playerIsNear = false;
    private bool isCollected = false;
    public KeyCode interactionKey = KeyCode.F;

    void Start()
    {
        // Tente encontrar a câmera principal se não for definida no Inspector
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        // Só interage se estiver perto e não tiver sido coletado
        if (playerIsNear && !isCollected && Input.GetKeyDown(interactionKey))
        {
            CollectObject();
        }
    }

    void CollectObject()
    {
        // 1. Torna o Walkie Talkie filho da câmera do jogador
        transform.SetParent(playerCamera);

        // 2. Define a posição e rotação local (relativa à câmera)
        transform.localPosition = handPosition;
        transform.localRotation = Quaternion.Euler(handRotation);

        // 3. Define o objeto como coletado e remove seu Collider/Rigidbody para evitar colisões
        isCollected = true;

        // Desativa a capacidade de interagir novamente
        playerIsNear = false;

        // Opcional: Desativa o Collider e Rigidbody para que ele não interaja mais com o mundo
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Debug.Log("Walkie Talkie coletado!");
    }

    // --- Lógica do Gatilho ---
    private void OnTriggerEnter(Collider other)
    {
        // Confirme se a Tag do seu jogador é "Player"
        if (other.CompareTag("MainCamera") && !isCollected)
        {
            playerIsNear = true;
            Debug.Log("Pressione F para pegar o Walkie Talkie.");
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