using UnityEngine;

public class ScareActivator : MonoBehaviour
{
    [Header("Monstro a Ativar")]
    [Tooltip("Arraste o GameObject do monstro (com JumpscareHandler) aqui.")]
    public JumpscareHandler scareHandler; // Referência ao novo script!

    private const string PlayerTag = "MainCamera";
    private bool hasBeenActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) && !hasBeenActivated)
        {
            if (scareHandler != null)
            {
                // Chama a nova função StartScareSequence
                scareHandler.StartScareSequence();
                hasBeenActivated = true;

                // Desativa o próprio Trigger para não disparar novamente
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                Debug.LogError("O script JumpscareHandler não foi atribuído no ScareActivator!");
            }
        }
    }
}