using UnityEngine;
using UnityEngine.AI;
using System.Collections; // ESSENCIAL para a Coroutine (Delay no movimento)

public class MonsterRunner : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private SkinnedMeshRenderer monsterRenderer; // Adicionado para controlar a visibilidade

    [Header("Configurações de Alvo e Velocidade")]
    public Transform targetDestination;
    public float runningSpeed = 6.0f;
    public float destructionDelay = 0.5f; // Atraso para sumir após a parada

    [Header("Configurações do Animator")]
    public string runningParameterName = "correndo";

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // Pega o Skinned Mesh Renderer (o que renderiza modelos animados)
        monsterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        // Verificação robusta de todos os componentes
        if (agent == null || animator == null || targetDestination == null || monsterRenderer == null)
        {
            Debug.LogError("ERRO CRÍTICO: Componentes ou Destino ausentes no monstro! Verifique se todos os campos estão preenchidos no Inspector.");
            enabled = false;
            return;
        }

        agent.isStopped = true;
        SetRunningAnimation(false);

        // Esconde o monstro no início
        monsterRenderer.enabled = false;
    }

    void Update()
    {
        // Se o agente ainda não está pronto no NavMesh, não faça nada.
        if (agent == null || !agent.isActiveAndEnabled || !agent.isOnNavMesh)
        {
            return;
        }

        if (!agent.isStopped)
        {
            // Mantém a animação 'Correndo' ligada
            SetRunningAnimation(true);

            // VERIFICAÇÃO ROBUSTA DE CHEGADA:
            if (agent.hasPath)
            {
                // Se a distância restante for menor ou igual à distância de parada (Stopping Distance)
                bool isPathFinished = agent.remainingDistance <= agent.stoppingDistance;
                // E a velocidade do monstro for quase zero (ele parou)
                bool isVelocityZero = agent.velocity.sqrMagnitude < 0.1f;

                if (isPathFinished && isVelocityZero)
                {
                    // Se chegou no destino E a velocidade é zero, chame a função
                    StopAndVanish();
                }
            }
        }
    }

    // Função pública chamada pelo MonsterActivator (Trigger)
    public void StartRunning()
    {
        // 1. Torna o monstro visível imediatamente antes de correr
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = true;
        }

        // 2. Inicia a Coroutine para evitar o erro de timing do NavMeshAgent
        StartCoroutine(StartRunningDelayed());
    }

    // Coroutine: Atraso necessário para o NavMeshAgent inicializar sem o erro 'isStopped'
    IEnumerator StartRunningDelayed()
    {
        // Espera um frame para que o NavMeshAgent inicialize
        yield return null;

        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            if (agent.isStopped)
            {
                agent.speed = runningSpeed;
                agent.SetDestination(targetDestination.position);
                agent.isStopped = false;

                SetRunningAnimation(true);
            }
        }
        else
        {
            Debug.LogError("ERRO FATAL: NavMeshAgent falhou ao se fixar no NavMesh. Verifique se o NavMesh existe.");
        }
    }


    private void StopAndVanish()
    {
        agent.isStopped = true;
        SetRunningAnimation(false);

        // Garante que o monstro suma da vista imediatamente
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = false;
        }

        Debug.Log("Monstro parou no destino. Sumindo em " + destructionDelay + " segundos.");

        // Destrói o GameObject após o atraso definido
        Destroy(gameObject, destructionDelay);
        enabled = false;
    }

    private void SetRunningAnimation(bool isRunning)
    {
        animator.SetBool(runningParameterName, isRunning);
    }
}