using UnityEngine;
using UnityEngine.AI;

public class MonsterRunner : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Configurações de Alvo e Velocidade")]
    public Transform targetDestination;
    public float runningSpeed = 6.0f;
    public float destructionDelay = 0.5f;

    [Header("Configurações do Animator")]
    // NOVO: Usaremos o nome do parâmetro booleano (bool)
    public string runningParameterName = "correndo";

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null || animator == null || targetDestination == null)
        {
            Debug.LogError("Componentes ou Destino ausentes no monstro! Verifique os requisitos.");
            enabled = false;
            return;
        }

        agent.isStopped = true;
        SetRunningAnimation(false); // Garante que a animação Correndo esteja desligada
    }

    void Update()
    {
        if (agent == null || !agent.isActiveAndEnabled || !agent.isOnNavMesh)
        {
            return;
        }

        if (!agent.isStopped)
        {
            // 1. Mantém a animação 'Correndo' ligada
            SetRunningAnimation(true);

            // 2. Verifica se o monstro chegou perto o suficiente do destino
            if (agent.hasPath)
            {
                if (!agent.pathPending && agent.remainingDistance > 0f && agent.remainingDistance < agent.stoppingDistance)
                {
                    SetRunningAnimation(false); // Desliga a animação antes de sumir
                    StopAndVanish();
                }
            }
        }
    }

    public void StartRunning()
    {
        if (agent.isStopped)
        {
            agent.speed = runningSpeed;
            agent.SetDestination(targetDestination.position);
            agent.isStopped = false;

            // Liga a animação de corrida imediatamente
            SetRunningAnimation(true);
        }
    }

    private void StopAndVanish()
    {
        agent.isStopped = true;
        SetRunningAnimation(false); // Garante que o monstro não esteja na pose de corrida

        Debug.Log("Monstro parou no destino. Sumindo em " + destructionDelay + " segundos.");

        Destroy(gameObject, destructionDelay);
        enabled = false;
    }

    // Função para ligar/desligar a animação booleana
    private void SetRunningAnimation(bool isRunning)
    {
        animator.SetBool(runningParameterName, isRunning);
    }
}