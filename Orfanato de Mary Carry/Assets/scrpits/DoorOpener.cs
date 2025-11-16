using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [Header("Componentes da Porta")]
    // Objeto da porta que será movido ou rotacionado (arraste o modelo 3D aqui)
    public GameObject doorObject;

    // Tag da chave necessária (deve ser "ChaveCozinha")
    public string requiredKeyTag = "ChaveCozinha";

    [Header("Ação da Porta")]
    // Define se a porta abre com rotação ou movimento
    public bool useRotation = true;
    public Vector3 openValue = new Vector3(0, 90, 0); // Ângulo ou Posição quando aberta (Ex: 90 graus no eixo Y)
    public float openSpeed = 2.0f;

    private bool isPlayerNear = false;
    private bool isOpen = false;
    private GameObject heldKey = null; // Armazena a chave encontrada
    public KeyCode interactionKey = KeyCode.F;

    void Update()
    {
        // Se a porta está aberta, move/gira para a posição final
        if (isOpen)
        {
            OpenDoorTransition();
        }
        // Se o jogador está perto e a tecla de interação é pressionada
        else if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            // Tenta abrir a porta
            TryToOpenDoor();
        }
    }

    void TryToOpenDoor()
    {
        // 1. Tenta encontrar a chave na mão (filha da câmera principal)
        Transform cameraTransform = Camera.main.transform;

        // Percorre todos os objetos filhos da câmera
        foreach (Transform child in cameraTransform)
        {
            if (child.CompareTag(requiredKeyTag))
            {
                heldKey = child.gameObject;
                break; // Chave encontrada!
            }
        }

        // 2. Se a chave foi encontrada:
        if (heldKey != null)
        {
            Debug.Log("Chave correta encontrada! Abrindo porta e consumindo chave.");

            // Ação 1: Abre a porta
            isOpen = true;

            // Ação 2: Faz a chave sumir
            Destroy(heldKey);
        }
        else
        {
            Debug.Log("Você precisa da " + requiredKeyTag + " para abrir esta porta.");
        }
    }

    void OpenDoorTransition()
    {
        if (doorObject == null) return;

        if (useRotation)
        {
            // Abre a porta girando (ex: para 90 graus)
            Quaternion targetRotation = Quaternion.Euler(openValue);
            doorObject.transform.localRotation = Quaternion.Slerp(
                doorObject.transform.localRotation,
                targetRotation,
                Time.deltaTime * openSpeed
            );
        }
        else
        {
            // Abre a porta movendo (ex: para cima ou para o lado)
            Vector3 targetPosition = doorObject.transform.localPosition + openValue;
            doorObject.transform.localPosition = Vector3.Lerp(
                doorObject.transform.localPosition,
                targetPosition,
                Time.deltaTime * openSpeed
            );
        }
    }

    // --- Lógica de Proximidade (Trigger) ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            isPlayerNear = true;
            Debug.Log("Pressione F para interagir com a porta.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            isPlayerNear = false;
        }
    }
}