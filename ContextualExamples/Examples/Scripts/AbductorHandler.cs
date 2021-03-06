using UnityEngine;
using System.Collections;

public class AbductorHandler : SaucerBaseHandler
{
	public CtxMenu commandSubmenu;
	CtxMenu.Item[] menuItems;
	
	enum Commands
	{
		AbductSpecimens = 20,
		EraseMemory,
		UseProbeDevice,
	}
	
	public void OnShowPopupMenu()
	{
		BuildMenu();
		CtxPopup.current.menuItems = menuItems;
	}
	
	public void OnShowMenu(CtxObject obj)
	{
		BuildMenu();
		obj.menuItems = menuItems;
	}
	
	public void OnHidePopupMenu()
	{
		Debug.Log("Abductor menu hidden (popup)");
	}
	
	public void OnHideMenu(CtxObject obj)
	{
		Debug.Log("Abductor menu hidden (object)");
	}
	
	void BuildMenu()
	{
		int baseItem = base.MenuItemCount;
		
		if (menuItems == null)
		{
			int itemCnt = baseItem+1;
			menuItems = new CtxMenu.Item[itemCnt];
			
			for (int i=0; i<itemCnt; i++)
				menuItems[i] = new CtxMenu.Item();
		}
		
		base.FillMenuItems(menuItems);
	
		menuItems[baseItem].text = "Abductor";
		menuItems[baseItem].isSubmenu = true;
		menuItems[baseItem].submenu = commandSubmenu;
		
#if CTX_NO_SERIALIZATION_FIX
		CtxMenu.Item[] submenuItems = new CtxMenu.Item[3];
		menuItems[baseItem].submenuItems = submenuItems;
#else
		CtxMenu.Item[] submenuItems = new CtxMenu.Item[3];
		commandSubmenu.items = submenuItems;
#endif

		for (int i=0; i<3; i++)
		{
			submenuItems[i] = new CtxMenu.Item();
			submenuItems[i].id = (int)Commands.AbductSpecimens+i;
		}
		
		submenuItems[0].text = "Abduct Specimens";
		submenuItems[1].text = "Erase Memory";
		submenuItems[2].text = "Use 'Probe' Device";
	}
	
	public new void OnMenuSelection()
	{
		int selection = CtxMenu.current.selectedItem;
		
		Commands cmd = (Commands)selection;
		switch (cmd)
		{
		case Commands.AbductSpecimens:
		case Commands.EraseMemory:
		case Commands.UseProbeDevice:
			Debug.Log("Abductor: "+cmd.ToString());
			break;
		default:
			base.OnMenuSelection();
			break;
		}
	}
}
