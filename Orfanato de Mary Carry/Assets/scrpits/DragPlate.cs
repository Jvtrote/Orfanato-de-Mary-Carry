using UnityEngine;

public class DragPlate : MonoBehaviour
{
    // Armazena o deslocamento do mouse em relação ao centro do prato, 
    // para que o prato não "pule" para o centro do mouse ao clicar.
    private Vector3 offset;

    // A distância do prato até a câmera na hora do clique.
    // Usado para manter o prato na mesma profundidade (eixo Z).
    private float zCoord;

    // --- FUNÇÃO CHAMADA NO PRIMEIRO CLIQUE DO MOUSE ---
    private void OnMouseDown()
    {
        // 1. Converte a posição atual do prato do mundo 3D para a tela 2D
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // 2. Armazena a coordenada Z (profundidade)
        zCoord = screenPoint.z;

        // 3. Calcula o offset (deslocamento) entre o mouse e o centro do prato
        // O prato ficará "agarrado" onde o mouse clicou, e não no centro.
        offset = transform.position - GetMouseWorldPos();
    }

    // --- FUNÇÃO QUE CALCULA A POSIÇÃO 3D DO MOUSE ---
    private Vector3 GetMouseWorldPos()
    {
        // 1. Pega a posição 2D do mouse na tela
        Vector3 mousePoint = Input.mousePosition;

        // 2. Define a coordenada Z (profundidade) para a posição do prato
        mousePoint.z = zCoord;

        // 3. Converte as coordenadas de tela 2D para coordenadas de mundo 3D
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // --- FUNÇÃO CHAMADA ENQUANTO O MOUSE ESTÁ PRESSIONADO E MOVIDO ---
    private void OnMouseDrag()
    {
        // Move o prato para a nova posição do mouse, aplicando o offset.
        // O uso do Rigidbody Kinematic (como sugerido no pré-requisito) 
        // permite esta movimentação direta.
        transform.position = GetMouseWorldPos() + offset;
    }

    // --- FUNÇÃO CHAMADA QUANDO O BOTÃO DO MOUSE É SOLTO ---
    private void OnMouseUp()
    {
        // Aqui você pode adicionar lógica de "snap" (encaixe) se quiser que
        // o prato pule para o centro da mesa quando solto.
        // Para este puzzle, é opcional, pois o sistema de Trigger já resolve a validação.
    }
}