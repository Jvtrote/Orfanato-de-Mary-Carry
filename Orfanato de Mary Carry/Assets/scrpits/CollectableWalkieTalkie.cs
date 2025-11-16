using UnityEngine;

public class CollectableWalkieTalkie : MonoBehaviour
{
    // Variável para o componente de Áudio (ADICIONADO)
    private AudioSource audioSource;
    
    // O objeto da Câmera do jogador (arraste a Main Camera aqui pelo Inspector)
    public Transform playerCamera;
    
    // ... suas outras variáveis ...

    [Header("Configurações na Mão")]
    public Vector3 handPosition = new Vector3(0.4f, -0.3f, 0.7f);
    public Vector3 handRotation = new Vector3(0f, 0f, 0f);

    private bool playerIsNear = false;
    private bool isCollected = false;
    public KeyCode interactionKey = KeyCode.F;

    void Start()
    {
        // Pega o componente AudioSource neste GameObject (ADICIONADO)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("O WalkieTalkie precisa de um componente AudioSource para tocar o áudio.");
        }
        
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
        // 1. TOCA O ÁUDIO ANTES DE MOVER/DESATIVAR O OBJETO (ADICIONADO)
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        
        // 2. Torna o Walkie Talkie filho da câmera do jogador
        transform.SetParent(playerCamera);

        // 3. Define a posição e rotação local (relativa à câmera)
        transform.localPosition = handPosition;
        transform.localRotation = Quaternion.Euler(handRotation);

        // 4. Define o objeto como coletado e remove seu Collider/Rigidbody
        isCollected = true;
        
        // Desativa a capacidade de interagir novamente
        playerIsNear = false;

        // Opcional: Desativa o Collider e Rigidbody para que ele não interaja mais com o mundo
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Debug.Log("Walkie Talkie coletado! Áudio iniciado.");
    }

    // ... (restante da lógica OnTriggerEnter e OnTriggerExit permanece inalterada) ...
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