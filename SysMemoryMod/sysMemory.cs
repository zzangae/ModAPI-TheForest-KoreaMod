using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ModAPI;
using TheForest.Utils;
using UnityEngine.VR;
using UnityEngine.Profiling;
using ModAPI.Attributes;
using System.Collections;

namespace SysMemoryMod
{
	public class sysMemory : MonoBehaviour
	{
		public bool ShowWindow;
		public bool ShowPlayerStat;
		private float ShowFPS = 60f;		
		private int _totalEntities = 0;
		private int _activeEntities = 0;
		private int _frozenTrees = 0;
		private int _activeTrees = 0;
		private Dictionary<Type, int> _counters;
		private static Dictionary<Type, int> Counters;		

		[ExecuteOnGameStart]
		private static void AddMeToScene()
		{
			new GameObject("__SystemMenu__").AddComponent<sysMemory>();
		}

		private void Awake()
		{			
			Counters = (_counters = new Dictionary<Type, int>());
			DontDestroyOnLoad(this);
		}

		private void OnGUI()
		{
			Color color = GUI.color;
			if (ToggleHandle())
			{
				return;
			}
			if (ShowWindow)
			{
				GUILayout.BeginHorizontal(GUILayout.Width(Screen.width), GUILayout.Height(24f));
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical();
				GUILayout.Label("IsGPad: " + TheForest.Utils.Input.IsGamePad, GUI.skin.button);
				GUILayout.Label("WasGPad: " + TheForest.Utils.Input.WasGamePad, GUI.skin.button);
				GUILayout.Label("AnyKey: " + TheForest.Utils.Input.anyKeyDown, GUI.skin.button);
				GUILayout.Label("MouseLoc: " + TheForest.Utils.Input.IsMouseLocked, GUI.skin.button);
				GUILayout.EndVertical();
				GUILayout.BeginVertical();
				GUILayout.Label("FPS: " + (int)ShowFPS, GUI.skin.button);
				GUILayout.Label("DXType: " + SystemInfo.graphicsDeviceType, GUI.skin.button, GUILayout.MinWidth(100f));
				GUILayout.Label("DX11: " + (SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders), GUI.skin.button, GUILayout.MinWidth(100f));
				GUILayout.Label("GSL: " + SystemInfo.graphicsShaderLevel, GUI.skin.button, GUILayout.MinWidth(100f));
				GUILayout.Label("CompSh: " + SystemInfo.supportsComputeShaders, GUI.skin.button, GUILayout.MinWidth(100f));
				if (ForestVR.Enabled)
				{
					GUILayout.Label("VRRenderScale: " + VRSettings.renderScale, GUI.skin.button, GUILayout.MinWidth(100f));
				}
				GUILayout.EndVertical();
				GUILayout.BeginVertical();
				GUILayout.Label($"Total Alloc: {Profiler.GetTotalAllocatedMemoryLong()}", GUI.skin.button, GUILayout.MinWidth(140f));
				GUILayout.Label($"Total Reserved: {Profiler.GetTotalReservedMemoryLong()}", GUI.skin.button, GUILayout.MinWidth(140f));
				GUILayout.Label($"Heap Size: {Profiler.GetMonoHeapSizeLong()}", GUI.skin.button, GUILayout.MinWidth(140f));
				GUILayout.Label($"Used Size: {Profiler.GetMonoUsedSizeLong()}", GUI.skin.button, GUILayout.MinWidth(140f));
				GUILayout.Label($"GC: {(GC.GetTotalMemory(forceFullCollection: false))}", GUI.skin.button, GUILayout.MinWidth(100f));
				GUILayout.EndVertical();
				GUILayout.BeginVertical();
				GUILayout.Label((int)FMOD_StudioEventEmitter.HoursSinceMidnight + "h" + (int)((FMOD_StudioEventEmitter.HoursSinceMidnight - (float)(int)FMOD_StudioEventEmitter.HoursSinceMidnight) * 60f) + ((!Clock.Dark) ? " (d)" : " (n)"), GUI.skin.button, GUILayout.Width(80f));
				GUILayout.Label((!LocalPlayer.IsInCaves) ? "Not in cave" : "In cave", GUI.skin.button, GUILayout.Width(80f));
				if ((bool)LocalPlayer.Inventory)
				{
					GUILayout.Label("x: " + LocalPlayer.Transform.position.x + "\ny: " + LocalPlayer.Transform.position.y + "\nz: " + LocalPlayer.Transform.position.z, GUI.skin.button, GUILayout.Width(80f));
				}
				GUILayout.EndVertical();
				GUILayout.BeginVertical();
				ShowPlayerStat = GUILayout.Toggle(ShowPlayerStat, "Player Stats", GUI.skin.button);
				if ((bool)Scene.SceneTracker)
				{
					GUILayout.Label("Shadow Distance: " + Scene.Atmosphere.DebugShadowDist.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("Light Forward: " + Scene.Atmosphere.DebugLightForward.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("mod shadow blend: " + Scene.Atmosphere.DebugModShadow.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("Occlusion: " + LOD_Manager.TreeOcclusionBonusRatio.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("shadow resolution: " + QualitySettings.shadowResolution, GUI.skin.button);
					GUILayout.Label("Total Entities: " + _totalEntities.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("Total Active Entities: " + _activeEntities.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("Total Active Trees: " + _activeTrees.ToString("0.00"), GUI.skin.button);
					GUILayout.Label("Total Frozen Trees: " + _frozenTrees.ToString("0.00"), GUI.skin.button);
				}
				GUILayout.EndVertical();
				foreach (KeyValuePair<Type, int> counter in Counters)
				{
					if (GUILayout.Button(counter.Key.Name + ": " + counter.Value))
					{
						CheckAmount(counter.Key, counter.Value);
					}
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}
			if (ShowPlayerStat && (bool)LocalPlayer.Stats)
			{
				GUILayout.BeginArea(new Rect(Screen.width - 250, Screen.height / 2 - 200, 250f, 400f), GUI.skin.textArea);
				GUILayout.BeginVertical();
				GUILayout.Label("+ Athleticism real:" + LocalPlayer.Stats.Skills.AthleticismSkillLevel + ", display:" + LocalPlayer.Stats.Skills.AthleticismSkillLevelProgressApprox, GUI.skin.button);
				GUILayout.Label($"|- Sprint: {LocalPlayer.Stats.Skills.TotalRunDuration:F0} / {LocalPlayer.Stats.Skills.RunSkillLevelDuration:F0} = {LocalPlayer.Stats.Skills.TotalRunDuration / LocalPlayer.Stats.Skills.RunSkillLevelDuration:F0} ", GUI.skin.button);
				GUILayout.Label($"|- Diving: {LocalPlayer.Stats.Skills.TotalLungBreathingDuration:F0} / {LocalPlayer.Stats.Skills.BreathingSkillLevelDuration:F0} = {LocalPlayer.Stats.Skills.TotalLungBreathingDuration / LocalPlayer.Stats.Skills.BreathingSkillLevelDuration:F0} ", GUI.skin.button);
				GUILayout.Space(20f);
				GUILayout.Label($"+ Weight {LocalPlayer.Stats.PhysicalStrength.CurrentWeight:F3}lbs", GUI.skin.button);
				GUILayout.Label($"|- Current Calories Burnt: {LocalPlayer.Stats.Calories.CurrentCaloriesBurntCount:F3}", GUI.skin.button);
				GUILayout.Label($"|- Current Calories Eaten: {LocalPlayer.Stats.Calories.CurrentCaloriesEatenCount:F3}", GUI.skin.button);
				GUILayout.Label($"|- Excess Calories Final: {LocalPlayer.Stats.Calories.GetExcessCaloriesFinal()}", GUI.skin.button);
				GUILayout.Label($"|- Time to next resolution: {LocalPlayer.Stats.Calories.TimeToNextResolution():F3} Hours (IG)", GUI.skin.button);
				int excessCaloriesFinal = LocalPlayer.Stats.Calories.GetExcessCaloriesFinal();
				GUILayout.Label($"|- Weight change at resolution: {(float)excessCaloriesFinal * ((excessCaloriesFinal <= 0) ? LocalPlayer.Stats.Calories.WeightLossPerMissingCalory : LocalPlayer.Stats.Calories.WeightGainPerExcessCalory):F3} lbs", GUI.skin.button);
				GUILayout.Space(20f);
				GUILayout.Space(20f);
				GUILayout.Label(string.Format("+ Strength {0:F4} ({1})", LocalPlayer.Stats.PhysicalStrength.CurrentStrength, (excessCaloriesFinal <= 0) ? "Losing" : "Gaining"), GUI.skin.button);
				GUILayout.Space(20f);
				GUILayout.EndVertical();
				GUILayout.EndArea();
			}
		}

		private void CheckAmount(Type t, int amount)
		{
			UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(t);
			int num = ((array != null) ? array.Length : 0);
			ModAPI.Log.Write(string.Concat("GameObject.FindObjectsOfType<", t, ">().Length = ", num, " (", num == amount, ")"));
		}

		private void ToggleWindowOverlay()
		{
			ShowWindow = !ShowWindow;
		}

		private void TogglePlayerStats()
		{
			ShowPlayerStat = !ShowPlayerStat;
		}

		private bool ToggleHandle()
		{
			if (UnityEngine.Event.current.type == EventType.KeyDown)
			{
				switch (UnityEngine.Event.current.keyCode)
				{
					case KeyCode.F1:
						ToggleWindowOverlay();
						break;
					case KeyCode.F2:
						TogglePlayerStats();
						break;
					default:
						return false;
				}
				return true;
			}
			return false;
		}

		private void Update()
		{
			ShowFPS = Mathf.Lerp(ShowFPS, 1f / Time.deltaTime, 0.05f);
			if (float.IsNaN(ShowFPS) || ShowFPS == 0f)
			{
				ShowFPS = 1f;
			}
		}
	}
}
