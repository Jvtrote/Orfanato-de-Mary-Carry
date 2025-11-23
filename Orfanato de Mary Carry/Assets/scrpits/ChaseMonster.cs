using UnityEngine;
using UnityEngine.AI;

public class ChaserMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform playerTarget;
    private SkinnedMeshRenderer monsterRenderer; // Componente gráfico
    private bool isChasing = false;

    [Header("Configurações")]
    public float chaseSpeed = 6.0f;
    public string runningAnimationName = "correndo";

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // CORREÇÃO: Procura o SkinnedMeshRenderer em qualquer objeto filho
        monsterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (agent == null)
        {
            Debug.LogError("ChaserMonster precisa de um NavMeshAgent!");
            enabled = false;
            return;
        }

        if (monsterRenderer == null)
        {
            Debug.LogError("ChaserMonster: Renderizador não encontrado. A visibilidade pode falhar.");
        }

        // Garante que o monstro esteja ativo e visível no início da perseguição
        gameObject.SetActive(true);
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = true;
        }

        // Garante que ele só se move quando StartChasing é chamado
        agent.isStopped = true;
    }

    // Função pública chamada pelo JumpscareHandler para iniciar a perseguição
    public void StartChasing(Transform target)
    {
        playerTarget = target;
        isChasing = true;

        agent.speed = chaseSpeed;
        agent.isStopped = false; // Inicia o movimento

        if (animator != null)
        {
            animator.SetBool(runningAnimationName, true);
        }
    }

    void Update()
    {
        if (isChasing && playerTarget != null)
        {
            // Atualiza o destino a cada frame para seguir o jogador
            agent.SetDestination(playerTarget.position);
        }
    }

    // Função para ser chamada pelo Trigger de Fim para sumir
    public void Vanish()
    {
        isChasing = false;

        if (animator != null)
        {
            animator.SetBool(runningAnimationName, false);
        }

        // Torna o monstro invisível antes de destruir
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = false;
        }

        // Destrói o GameObject
        Destroy(gameObject);
    }
}