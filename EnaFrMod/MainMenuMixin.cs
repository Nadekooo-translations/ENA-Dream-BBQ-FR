using HarmonyLib;
using JoelG.ENA4.UI;
using TMPro;

namespace EnaFrMod;

public class MainMenuMixin
{
	[HarmonyPatch(typeof(MainMenuPanelGroup), "InsertItem")]
	[HarmonyPrefix]
	private static void InsertMenuItem(MainMenuPanel value)
	{
		if (value.name == "Main")
		{
			var components = value.GetComponentsInChildren<TMP_Text>();

			foreach (var comp in components)
			{
				var translated = comp.text switch {
					"Continue" => "Continuer",
					"Options" => "Options",
					"Extras" => "Extras",
					"Quit" => "Quitter",
					_ => null,
				};

				if (translated != null) {
					comp.text = translated;
				}
			}
		}
	}
}
