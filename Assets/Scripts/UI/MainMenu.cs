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
			Rect buildButtonRect = new Rect(760,(435) + ((numButtons-1) *(150/(numButtons))) ,190, (150/(2)));
			build = MakeButton("Build",buildButtonRect);
			build.AddComponent("SwitchMenu");
			build.AddComponent ("BuildMenu");
			SwitchMenu buildSM = build.GetComponent<SwitchMenu>();
			BuildMenu buildBM = build.GetComponent<BuildMenu>();
			build.transform.parent = transform;
			MenuItem m1 = build.GetComponent<MenuItem>();
			m1.action = buildSM;
			m1.guiSkin = squareStyle;
			buildSM.fromMenu = this.gameObject;
			buildSM.toMenu = build;
			buildBM.click = click;
			buildBM.squareStyle = squareStyle;
			m1.action.click = click;
			menuItems.Add(m1);
			numButtons++;
			
			fight = makeFireButton(numButtons);
			Rect fortifyButtonRect = new Rect(610,180,buttonSize.x, buttonSize.y);
			//fortifyButtonRect = FindPos(numButtons, fortifyButtonRect);
			fortify = MakeButton("Fortify",fortifyButtonRect);
			fortify.AddComponent("SwitchMenu");
			fortify.AddComponent ("FortifyMenu");
			SwitchMenu fortifySM = fortify.GetComponent<SwitchMenu>();
			FortifyMenu fortifyFM = fortify.GetComponent<FortifyMenu>();
			fortify.transform.parent = transform;
			MenuItem m3 = fortify.GetComponent<MenuItem>();
			m3.action = fortifySM;
			m3.guiSkin = squareStyle;
			fortifySM.fromMenu = this.gameObject;
			fortifySM.toMenu = fortify;
			fortifyFM.click = click;
			fortifyFM.squareStyle = squareStyle;
			m3.action.click = click;
			menuItems.Add(m3);
			Rect upgradeButtonRect = new Rect(610,240 ,buttonSize.x, buttonSize.y);
			//upgradeButtonRect = FindPos(numButtons, upgradeButtonRect);
			upgrade = MakeButton("Upgrade",upgradeButtonRect);
			upgrade.AddComponent("SwitchMenu");
			upgrade.AddComponent ("UpgradeMenu");
			SwitchMenu upgradeSM = upgrade.GetComponent<SwitchMenu>();
			UpgradeMenu upgradeUM = upgrade.GetComponent<UpgradeMenu>();
			upgrade.transform.parent = transform;
			MenuItem m4 = upgrade.GetComponent<MenuItem>();
			m4.action = upgradeSM;
			m4.guiSkin = squareStyle;
			upgradeSM.fromMenu = this.gameObject;
			upgradeSM.toMenu = upgrade;
			upgradeUM.click = click;
			upgradeUM.squareStyle = squareStyle;
			m4.action.click = click;
			menuItems.Add(m4);
			Rect passButtonRect = new Rect(760,(435) + ((numButtons-1) *(150/(numButtons)))+10,190,150/(2));
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
		if(TowerSelection.GetSelectedTower() == null || TowerSelection.GetSelectedTower().isAlive() == false) {
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
		Rect fightButtonRect = new Rect(610,300,buttonSize.x, buttonSize.y);
		//fightButtonRect = FindPos(numButtons, fightButtonRect);
		GameObject fight = MakeButton("Fight",fightButtonRect);
		fight.AddComponent("Menu");
		fight.AddComponent ("FireAction");
		fight.GetComponent<FireAction>().fightMenu = fight.GetComponent<Menu>();
		fight.AddComponent ("WeaponAnimator");
		WeaponAnimator fightWA = fight.GetComponent<WeaponAnimator>();
		fightWA.hitParticle = hitp;
		fightWA.ballistaFire = balfire;
		fightWA.ballistaHit = balhit;
		fightWA.catapultFire = trefire;
		fightWA.catapultHit = trehit;
		fightWA.cannonFire = canfire;
		fightWA.cannonHit = canhit;
		fightWA.missSound = miss;
		fightWA.missFont = missfont;
		fightWA.splitScreenTexture = splitscreen;
		fightWA.damageText = damagetext;
		fight.AddComponent ("CollapseAnimator");
		CollapseAnimator fightCA = fight.GetComponent<CollapseAnimator>();
		fightCA.collapseParticle = collp;
		fightCA.soundEffect = sound;
		fight.AddComponent<AudioSource>();
		fight.transform.parent = transform;
		MenuItem m2 = fight.GetComponent<MenuItem>();
		m2.action = fight.GetComponent<FireAction>();
		//m2.guiSkin = squareStyle;
		m2.action.click = click;
		menuItems.Add(m2);
		return fight;
	}
}
