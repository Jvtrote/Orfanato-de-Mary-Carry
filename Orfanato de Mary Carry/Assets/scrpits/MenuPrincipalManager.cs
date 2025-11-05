using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour 
{
   [SerializeField] private string nomeDoLevelDeJogo;
   [SerializeField] private GameObject painelMenuInicial;

    public void Jogar() 
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    } 

    public void Sair() 
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }



}
