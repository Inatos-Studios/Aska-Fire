using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 							// Chamar biblioteca de user interface
using UnityEngine.SceneManagement; 				// Chamar biblioteca de Manuseio de cenas.
using System.IO;

public class Menu : MonoBehaviour {

	[Header("Buttons")]
	public Button Continue;

	private	float tempo = 1.7f;					// CHAMAR ESSAS FUNÇÕES NO BOTÃO.

	void Start(){
//		CheckSaveGame();
	}

	public void irParaCena(string nomeCena){ 	// Criando função ir para cena.
		SceneManager.LoadScene(nomeCena); 		// Definindo que a função irParaCena, vai ser um LOADSCENE, ou seja, vai carregar a cena.
	}

	public void sair(){ 						// Criando função para sair do app.
		Application.Quit(); 					// Definindo que ao ser chamado, o sistema fecha o aplicativo.
	}	

	public void Loading(){
    	SceneManager.LoadScene("LOADING");   
	}

	public void ir(){		
		Invoke("Loading", tempo);
	}

	public void continuar()
	{
		if (PlayerPrefs.HasKey("faseSalva"))
		{
			SceneManager.LoadScene(PlayerPrefs.GetInt("faseSalva"));
		}
	}

//	void CheckSaveGame(){
//		Continue.interactable = false;
//
//		if(File.Exists(Application.persistentDataPath + "/saveGame.inatos")){
//			Continue.interactable = true;
//		}
//	}
//
//	public void newGame(int slot){
//
//		switch(slot){
//
//			case 1:
//				PlayerPrefs.SetString("slot", "saveGame.inatos");
//				break;
//		}
//	}
//
//	public void loadGame(int slot){
//		}
}