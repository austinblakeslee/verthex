using UnityEngine;
using System.Collections;

public class MainMenu : Menu {

	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	private int numButtons;
	public AudioClip balfire;
	public AudioClip balhit;
	public AudioClip trefire;
	public AudioClip trehit;
	public AudioClip canfire;
	public AudioClip canhit;
	public AudioClip miss;
	public Texture2D splitscreen;
	public Font missfont;
	public GameObject hitp;
	public GameObject collp;
	public AudioClip sound;
	public Texture2D empty;
	public Texture2D full;
	public GUIText damagetext;
	
	void Start() {
		buttonSize.x = 60;
		buttonSize.y = 50;
		this.on = true;
	}

	public override void Update() {
		if(!hasLoaded) {
			Faction f = TurnOrder.myPlayer.faction;
			Rect buildButtonRect = new Rect(200,200,buttonSize.x, buttonSize.y);
			buildButtonRect = FindPos(numButtons, buildButtonRect);
			GameObject build = MakeButton("Build",buildButtonRect);
			build.AddComponent("SwitchMenu");
			build.AddComponent ("BuildMenu");
			build.transform.parent = transform;
			MenuItem m1 = build.GetComponent<MenuItem>();
			m1.action = build.GetComponent<SwitchMenu>();
			build.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
			build.GetComponent<SwitchMenu>().toMenu = build;
			build.GetComponent<BuildMenu>().click = click;
			m1.action.click = click;
			menuItems.Add(m1);
			numButtons++;
			GameObject fight = makeFireButton(numButtons);
			numButtons++;
			Rect fortifyButtonRect = new Rect(200,200,buttonSize.x, buttonSize.y);
			fortifyButtonRect = FindPos(numButtons, fortifyButtonRect);
			GameObject fortify = MakeButton("Fortify",fortifyButtonRect);
			fortify.AddComponent("SwitchMenu");
			fortify.AddComponent ("FortifyMenu");
			fortify.transform.parent = transform;
			MenuItem m3 = fortify.GetComponent<MenuItem>();
			m3.action = fortify.GetComponent<SwitchMenu>();
			fortify.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
			fortify.GetComponent<SwitchMenu>().toMenu = fortify;
			fortify.GetComponent<FortifyMenu>().click = click;
			m3.action.click = click;
			menuItems.Add(m3);
			numButtons++;
			Rect upgradeButtonRect = new Rect(200,200,buttonSize.x, buttonSize.y);
			upgradeButtonRect = FindPos(numButtons, upgradeButtonRect);
			GameObject upgrade = MakeButton("Upgrade",upgradeButtonRect);
			upgrade.AddComponent("SwitchMenu");
			upgrade.AddComponent ("UpgradeMenu");
			upgrade.transform.parent = transform;
			MenuItem m4 = upgrade.GetComponent<MenuItem>();
			m4.action = upgrade.GetComponent<SwitchMenu>();
			upgrade.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
			upgrade.GetComponent<SwitchMenu>().toMenu = upgrade;
			upgrade.GetComponent<UpgradeMenu>().click = click;
			m4.action.click = click;
			menuItems.Add(m4);
			numButtons++;
			Rect passButtonRect = new Rect(200,200,buttonSize.x, buttonSize.y);
			passButtonRect = FindPos(numButtons, passButtonRect);
			GameObject pass = MakeButton("Pass",passButtonRect);
			pass.AddComponent("PassAction");
			pass.transform.parent = transform;
			MenuItem m5 = pass.GetComponent<MenuItem>();
			m5.action = pass.GetComponent<PassAction>();
			m5.action.click = click;
			menuItems.Add(m5);
			numButtons++;
			hasLoaded = true;
		}
		base.Update();
	}
	
	private GameObject MakeButton(string text, Rect rect) {
		GameObject item = new GameObject(text);
		item.AddComponent("MenuItem");
		MenuItem m = item.GetComponent<MenuItem>();
		m.left = ""+rect.xMin;
		m.top = ""+rect.yMin;
		m.width = ""+buttonSize.x;
		m.height = ""+buttonSize.y;
		m.type = MenuItem.MenuItemType.Button;
		m.visible = true;
		m.text = text;
		return item;
	}
	
	private GameObject MakeBox(string text, Rect rect) {
		GameObject item = new GameObject(text);
		item.AddComponent("MenuItem");
		MenuItem m = item.GetComponent<MenuItem>();
		m.left = ""+rect.xMin;
		m.top = ""+rect.yMin;
		m.width = ""+boxSize.x;
		m.height = ""+boxSize.y;
		m.type = MenuItem.MenuItemType.Box;
		m.visible = true;
		m.text = "";
		return item;
	}
	
	private Rect FindPos(int i, Rect rect) {
		if(i == 1 || i == 4 || i == 7) {
			rect.x = Screen.width - 150;
		}
		else if(i == 0 || i == 3 || i == 6) {
			rect.x = Screen.width - 225;
		}
		else if(i == 2 || i == 5 || i == 8) {
			rect.x = Screen.width - 75;
		}
		else {
			Debug.Log ("Too many buttons!");	
		}
		
		if(i <= 2) {
			rect.y = Screen.height - 165;
		}
		else if(i > 2 && i <= 5) {
			rect.y = Screen.height - 110;
		}
		else if(i > 5 && i <= 8) {
			rect.y = Screen.height - 55;
		}
		
		return rect;
	}
	
	private GameObject createGUIButton(string scriptName, string goName) {
		Rect itemButtonRect = new Rect(0,0,buttonSize.x,buttonSize.y); 
		itemButtonRect = FindPos(numButtons, itemButtonRect);
		GameObject item = MakeButton(goName,itemButtonRect);
		item.AddComponent(scriptName);
		item.transform.parent = transform;
		MenuItem m = item.GetComponent<MenuItem>();
		m.action = item.GetComponent(scriptName) as DefaultMenuAction;
		menuItems.Add(m);
		return item;
	}
	
	private GameObject makeFireButton(int i) {
		Rect fightButtonRect = new Rect(200,200,buttonSize.x, buttonSize.y);
		fightButtonRect = FindPos(numButtons, fightButtonRect);
		GameObject fight = MakeButton("Fight",fightButtonRect);
		fight.AddComponent("Menu");
		fight.AddComponent ("FireAction");
		fight.GetComponent<FireAction>().hitParticle = hitp;
		fight.GetComponent<FireAction>().fightMenu = fight.GetComponent<Menu>();
		fight.AddComponent ("PowerBar");
		fight.GetComponent<PowerBar>().emptyTexture = empty;
		fight.GetComponent<PowerBar>().fillTexture = full;
		fight.AddComponent ("WeaponAnimator");
		fight.GetComponent<WeaponAnimator>().hitParticle = hitp;
		fight.GetComponent<WeaponAnimator>().ballistaFire = balfire;
		fight.GetComponent<WeaponAnimator>().ballistaHit = balhit;
		fight.GetComponent<WeaponAnimator>().catapultFire = trefire;
		fight.GetComponent<WeaponAnimator>().catapultHit = trehit;
		fight.GetComponent<WeaponAnimator>().cannonFire = canfire;
		fight.GetComponent<WeaponAnimator>().cannonHit = canhit;
		fight.GetComponent<WeaponAnimator>().missSound = miss;
		fight.GetComponent<WeaponAnimator>().missFont = missfont;
		fight.GetComponent<WeaponAnimator>().splitScreenTexture = splitscreen;
		fight.GetComponent<WeaponAnimator>().damageText = damagetext;
		fight.AddComponent ("CollapseAnimator");
		fight.GetComponent<CollapseAnimator>().collapseParticle = collp;
		fight.GetComponent<CollapseAnimator>().soundEffect = sound;
		AudioSource audio = fight.AddComponent<AudioSource>();
		fight.transform.parent = transform;
		MenuItem m2 = fight.GetComponent<MenuItem>();
		m2.action = fight.GetComponent<FireAction>();
		m2.action.click = click;
		menuItems.Add(m2);
		return fight;
	}
}
