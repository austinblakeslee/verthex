using UnityEngine;
using System.Collections;

//This is used to set whether it is a Local or Online game.
//If Local, all turns can be played from the same machine.
public class GameType : MonoBehaviour {
	private static string gameType;
	
	public static void setGameType(string type) {
		gameType = type;
	}
	
	public static string getGameType() {
		return gameType;
	}

}
