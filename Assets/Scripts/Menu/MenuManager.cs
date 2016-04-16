using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	public Transform menuManager, optionsMenu;

	public void LoadScene(string name){
		Lib.LoadSceneSync (name);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void OptionsMenu(bool clicked){
		optionsMenu.gameObject.SetActive (clicked);
		menuManager.gameObject.SetActive (!clicked);
	}
}
