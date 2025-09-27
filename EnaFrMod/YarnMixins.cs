using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using JoelG.ENA4.UI.HUD.Dialogue;
using UnityEngine.UIElements.Collections;
using Yarn.Unity;
using Newtonsoft.Json.Linq;

namespace EnaFrMod;

public class YarnMixins
{
	[HarmonyPatch(typeof(HUDDialogueProjectManager), "Initialize")]
	[HarmonyPrefix]
	private static void GetLocalization()
	{
		Localization localization = HUDDialogueProjectManager.YarnProject.GetLocalization("en-US");

		using StreamReader reader = new("fr.json");
		string rawJson = reader.ReadToEnd();
		JObject node = JObject.Parse(rawJson);
		JObject strings = node["yarn"] as JObject;

		foreach (var key in (strings as IDictionary<string, JToken>).Keys)
		{
			localization.AddLocalizedString(key, (string)strings.Get(key));
		}
	}
}
