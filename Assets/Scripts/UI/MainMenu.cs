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
	public GameObject build;
	public GameObject fight;
	public GameObject fortify;
	public GameObject upgrade;
	public GUISkin squareStyle;
	//public GUISkin rectStyle;
	
	void Start() {
		buttonSize.x = 60;
		buttonSize.y = 50;
		this.on = true;
		numButtons = 1;
	}

	public override void Update() {
		if(!hasLoaded) {
			Rect buildButtonRect = new Rect(Screen.width - 200,(Screen.height - 165) + ((numButtons-1) *(150/(numButtons))) ,190, (150/(2)));
			build = MakeButton("Build",buildButtonRect);
			build.AddComponent("SwitchMenu");
			build.AddComponent ("BuildMenu");
			build.transform.parent = transform;
			MenuItem m1 = build.GetComponent<MenuItem>();
			m1.action = build.GetComponent<SwitchMenu>();
			m1.guiSkin = squareStyle;
			build.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
			build.GetComponent<SwitchMenu>().toMenu = build;
			build.GetComponent<BuildMenu>().click = click;
			build.GetComponent<BuildMenu>().guiSkin = squareStyle;
			m1.action.click = click;
			menuItems.Add(m1);
			numButtons++;
			
			fight = makeFireButton(numButtons);
			Rect fortifyButtonRect = new Rect(Screen.width/2 + 130,Screen.height/2 - 120,buttonSize.x, buttonSize.y);
			//fortifyButtonRect = FindPos(numButtons, fortifyButtonRect);
			fortify = MakeButton("Fortify",fortifyButtonRect);
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
			Rect upgradeButtonRect = new Rect(Screen.width/2 + 130,Screen.height/2 - 60 ,buttonSize.x, buttonSize.y);
			//upgradeButtonRect = FindPos(numButtons, upgradeButtonRect);
			upgrade = MakeButton("Upgrade",upgradeButtonRect);
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
			Rect passButtonRect = new Rect(Screen.width - 200,(Screen.height - 165) + ((numButtons-1) *(150/(numButtons)))+10,190,150/(2));
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
		
		if(TowerSelection.GetSelectedSection() == null) {
			fight.GetComponent<MenuItem>().visible = false;
		}
		else if (TowerSelection.GetSelectedSection() != null && (TowerSelection.GetSelectedSection().GetWeaponInfo() == "Nothing" || TurnOrder.ceasefire > TurnOrder.turnNum)) {			

			fight.GetComponent<MenuItem>().visible = false;
		}
		else {
			fight.GetComponent<MenuItem>().visible = true;
		}
		if(TowerSelection.GetSelectedSection() == null) {
			upgrade.GetComponent<MenuItem>().visible = false;
		}
		else if(TowerSelection.GetSelectedSection() != null && TowerSelection.GetSelectedSection().attributes.weapon.GetUpgradeLevel() >= TowerSelection.GetSelectedSection().attributes.weapon.maxUpgradeLevel) {
			upgrade.GetComponent<MenuItem>().visible = false;
		}
		else {
			upgrade.GetComponent<MenuItem>().visible = true;
		}
		if(TowerSelection.GetSelectedSection() == null) {
			fortify.GetComponent<MenuItem>().visible = false;
		}
		else if(TowerSelection.GetSelectedSection() != null && TowerSelection.GetSelectedSection().attributes.sp >= TowerSelection.GetSelectedSection().attributes.maxSP) {
			fortify.GetComponent<MenuItem>().visible = false;
		}
		else {
			fortify.GetComponent<MenuItem>().visible = true;
		}
		
		//Don't allow player to build if Tower is not alive
		if(TowerSelection.GetSelectedTower().isAlive() == false) {
			build.GetComponent<MenuItem>().visible = false;
		}else {
			build.GetComponent<MenuItem>().visible = true;
		}
		
	}
	
	private GameObject MakeButton(string text, Rect rect) {
		GameObject item = new GameObject(text);
		item.AddComponent("MenuItem");
		MenuItem m = item.GetComponent<MenuItem>();
		m.left = ""+rect.xMin;
		m.top = ""+rect.yMin;
		m.width = ""+rect.width;
		m.height = ""+rect.height;
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
	
	private GameObject createGUIButton(string scriptName, string goName) {
		Rect itemButtonRect = new Rect(0,0,buttonSize.x,buttonSize.y); 
		GameObject item = MakeButton(goName,itemButtonRect);
		item.AddComponent(scriptName);
		item.transform.parent = transform;
		MenuItem m = item.GetComponent<MenuItem>();
		m.action = item.GetComponent(scriptName) as DefaultMenuAction;
		menuItems.Add(m);
		return item;
	}
	
	private GameObject makeFireButton(int i) {
		Rect fightButtonRect = new Rect(Screen.width/2 + 130,Screen.height/2,buttonSize.x, buttonSize.y);
		//fightButtonRect = FindPos(numButtons, fightButtonRect);
		GameObject fight = MakeButton("Fight",fightButtonRect);
		fight.AddComponent("Menu");
		fight.AddComponent ("FireAction");
		fight.GetComponent<FireAction>().fightMenu = fight.GetComponent<Menu>();
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
		fight.AddComponent<AudioSource>();
		fight.transform.parent = transform;
		MenuItem m2 = fight.GetComponent<MenuItem>();
		m2.action = fight.GetComponent<FireAction>();
		m2.action.click = click;
		menuItems.Add(m2);
		return fight;
	}
}
