using UnityEngine;
using System.Collections;

public abstract class DefaultMenuAction:MonoBehaviour,MenuAction{
	public AudioClip click;
	public abstract void Action();
	
	protected void PlayClickSound() {
		AudioSource.PlayClipAtPoint(click, new Vector3(0,0,0));
	}
}
