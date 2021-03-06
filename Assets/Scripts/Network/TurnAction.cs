using UnityEngine;
using System.Collections;

//represents an action taken by a player
public abstract class TurnAction {

	protected const char TOKEN_SEPARATOR = '~';
	protected const int FIRST_AVAILABLE_INDEX = 3;
	
	public int playerNumber;
	public int towerNumber;
	public string actionName;
	public int cost = 0;
	
	public TurnAction(string actionName) {
		this.actionName = actionName;
	}
	
	public abstract string GetActionMessage();
	
	public abstract void Perform();
	
	protected abstract void ParseActionMessage(string actionMessage);
	
	protected string FormatActionMessage(params string[] args) {
		string[] messageParams = new string[3 + args.Length];
		messageParams[0] = playerNumber + "";
		messageParams[1] = towerNumber + "";
		messageParams[2] = this.actionName;
		args.CopyTo(messageParams, 3);
		return string.Join(TOKEN_SEPARATOR+"", messageParams);
	}
	
	public static TurnAction GetActionForMessage(string message) {
		if(message.Contains("Build")) {
			return new Build(message);
		} else if(message.Contains("Fortify")) {
			return new Fortify(message);
		} else if(message.Contains("Fight")) {
			return new Fight(message);
		} else if(message.Contains("Upgrade")) {
			return new Upgrade(message);
		} else {
			return new Pass(message);
		}
	}
}
