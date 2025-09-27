using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;
using HarmonyLib;
using JoelG.ENA4.UI.HUD.Dialogue;
using UnityEngine.UIElements.Collections;
using Yarn.Unity;

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
		JsonNode node = JsonNode.Parse(rawJson);

		JsonObject strings = node.AsObject().Get("yarn").AsObject();

		foreach (var key in (strings as IDictionary<string, JsonNode>).Keys)
		{
			localization.AddLocalizedString(key, (string)strings.Get(key));
		}
	}
}
