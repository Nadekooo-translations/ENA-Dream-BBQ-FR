using System.Collections.Generic;
using HarmonyLib;
using JoelG.ENA4.UI;
using TMPro;

namespace EnaFrMod;

public class MainMenuMixin
{
	private static readonly Dictionary<string, Dictionary<string, string>> MenuTranslations = [];

	static MainMenuMixin()
	{
		MenuTranslations.Add("Title", new Dictionary<string, string>
		{
			{"Press any key to continue...", "Appuyez sur une touche pour continuer..."},
		});

		MenuTranslations.Add("Main", new Dictionary<string, string>
		{
			{"Continue", "Continuer"},
			{"Options", "Paramètres"},
			{"Extras", "Bonus"},
			{"Quit", "Quitter"},
		});

		MenuTranslations.Add("Options", new Dictionary<string, string>
		{
			{"Gameplay", "Gameplay"},
			{"Input", "Entrée"},
			{"Audio", "Son"},
			{"Video", "Vidéo"},
			{"Controls", "Contrôles"},
			{"Accessibility", "Accessibilité"},
			{"Reset", "Paramètres par défaut"},
		});
	}

	[HarmonyPatch(typeof(MainMenuPanelGroup), "InsertItem")]
	[HarmonyPrefix]
	private static void InsertMenuItem(MainMenuPanel value)
	{
		// Plugin.Logger.LogWarning($"Panel: {value}");

		var components = value.GetComponentsInChildren<TMP_Text>();

		// foreach (var comp in components)
		// {
		// 	Plugin.Logger.LogWarning($"Component: {comp.text}");
		// }

		if (MenuTranslations.TryGetValue(value.name, out Dictionary<string, string> translations))
		{
			foreach (var comp in components)
			{
				if (translations.TryGetValue(comp.text, out string translated))
				{
					comp.text = translated;
				}
			}
		}
	}
}
