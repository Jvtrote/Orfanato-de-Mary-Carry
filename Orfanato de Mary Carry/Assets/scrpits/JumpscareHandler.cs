using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class JumpscareHandler : MonoBehaviour
{
    private Animator animator;
    private SkinnedMeshRenderer monsterRenderer;

    [Header("Configurações do Susto")]
    [Tooltip("Nome do Trigger para a animação de Susto. Ex: 'Susto'")]
    public string scareAnimationName = "Susto";

    [Tooltip("Duração (em segundos) que o monstro fica visível e na animação de susto.")]
    public float scareDuration = 1.5f;

    [Tooltip("Pode ser 0.1f para sumir imediatamente após o susto.")]
    public float destructionDelay = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (animator == null || monsterRenderer == null)
        {
            Debug.LogError("Componentes ausentes! JumpscareHandler precisa de Animator e SkinnedMeshRenderer.");
            enabled = false;
            return;
        }

        // Esconde o monstro no início do jogo
        monsterRenderer.enabled = false;
    }

    // Função pública chamada pelo HITBOX (ScareActivator)
    public void StartScareSequence()
    {
        // 1. Aparece o monstro
        monsterRenderer.enabled = true;

        // 2. Inicia a sequência Susto -> Sumir
        StartCoroutine(ScareSequence());
    }

    private IEnumerator ScareSequence()
    {
        // Dispara a animação de susto
        if (!string.IsNullOrEmpty(scareAnimationName))
        {
            animator.SetTrigger(scareAnimationName);
        }

        // Espera o tempo definido para a animação
        yield return new WaitForSeconds(scareDuration);

        // Sumir: Torna o monstro invisível e o destrói após um pequeno atraso
        Vanish();
    }

    // Função para sumir e destruir
    private void Vanish()
    {
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = false;
        }

        // Destrói o GameObject após o atraso (para garantir que o som termine)
        Destroy(gameObject, destructionDelay);
        enabled = false;
    }
}