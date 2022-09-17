﻿using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using SmashTools;
using UnityEngine;

namespace Vehicles
{
	public class Section_Main : SettingsSection
	{
		/* Map/World Generation */
		public float beachMultiplier = 0f;
		public int forceFactionCoastRadius = 1;

		/* General */
		public bool modifiableSettings = true;
		public bool useCustomShaders = true;
		public bool allowDiagonalRendering = true;
		public bool fullVehiclePathing = true;
		public bool smoothVehiclePaths = true;
		public bool vehiclePathingRoadNoCost = true;
		public bool multiplePawnsPerJob = true;
		public bool showDisabledVehicles = false;

		public float meleeDamageMultiplier = 1;
		public float rangedDamageMultiplier = 1;
		public float explosiveDamageMultiplier = 1;

		/* Turrets */
		public bool overheatMechanics = true;

		/* Boats */
		public bool passiveWaterWaves = true;

		/* Fishing */
		public float fishingMultiplier = 1f;
		public int fishingDelay = 10000;
		public int fishingSkillIncrease = 5;
		public bool fishingPersists = true;

		/* Aerial */
		public bool burnRadiusOnRockets = true;
		public bool deployOnLanding = true;
		public bool airDefenses = true;
		public bool dynamicWorldDrawing = true;
		public float delayDeployOnLanding = 0;

		/* Upgrades */
		public bool drawUpgradeInformationScreen = true;
		public bool overrideDrawColors = true;
		//REDO - Add hover over option for displaying info window?

		public override void ResetSettings()
		{
			base.ResetSettings();
			/* Map/World Generation */
			beachMultiplier = 0f;
			forceFactionCoastRadius = 1;

			/* General */
			modifiableSettings = true;
			useCustomShaders = true;

			allowDiagonalRendering = true;
			fullVehiclePathing = true;
			smoothVehiclePaths = true;

			vehiclePathingRoadNoCost = true;
			multiplePawnsPerJob = true;
			showDisabledVehicles = false;

			meleeDamageMultiplier = 1;
			rangedDamageMultiplier = 1;
			explosiveDamageMultiplier = 1;

			/* Turrets */
			overheatMechanics = true;

			/* Boats */
			passiveWaterWaves = true;

			/* Fishing */
			fishingMultiplier = 1f;
			fishingDelay = 10000;
			fishingSkillIncrease = 5;
			fishingPersists = true;

			/* Aerial */
			burnRadiusOnRockets = true;
			deployOnLanding = true;
			airDefenses = true;
			dynamicWorldDrawing = true;
			delayDeployOnLanding = 0;

			/* Upgrades */
			drawUpgradeInformationScreen = true;
			overrideDrawColors = true;
		}

		public override void ExposeData()
		{
			Scribe_Values.Look(ref beachMultiplier, nameof(beachMultiplier), defaultValue: 0f);
			Scribe_Values.Look(ref forceFactionCoastRadius, nameof(forceFactionCoastRadius), defaultValue: 1);

			Scribe_Values.Look(ref modifiableSettings, nameof(modifiableSettings), defaultValue: true);
			Scribe_Values.Look(ref useCustomShaders, nameof(useCustomShaders), defaultValue: true);

			Scribe_Values.Look(ref allowDiagonalRendering, nameof(allowDiagonalRendering), defaultValue: true);
			Scribe_Values.Look(ref fullVehiclePathing, nameof(fullVehiclePathing), defaultValue: true);
			Scribe_Values.Look(ref smoothVehiclePaths, nameof(smoothVehiclePaths), defaultValue: true);

			Scribe_Values.Look(ref vehiclePathingRoadNoCost, nameof(vehiclePathingRoadNoCost), defaultValue: true);
			Scribe_Values.Look(ref multiplePawnsPerJob, nameof(multiplePawnsPerJob), defaultValue: true);
			Scribe_Values.Look(ref showDisabledVehicles, nameof(showDisabledVehicles), defaultValue: false);

			Scribe_Values.Look(ref meleeDamageMultiplier, nameof(meleeDamageMultiplier), defaultValue: 1);
			Scribe_Values.Look(ref rangedDamageMultiplier, nameof(rangedDamageMultiplier), defaultValue: 1);
			Scribe_Values.Look(ref explosiveDamageMultiplier, nameof(explosiveDamageMultiplier), defaultValue: 1);

			Scribe_Values.Look(ref overheatMechanics, nameof(overheatMechanics), defaultValue: true);

			Scribe_Values.Look(ref passiveWaterWaves, nameof(passiveWaterWaves), defaultValue: true);

			Scribe_Values.Look(ref fishingMultiplier, nameof(fishingMultiplier), defaultValue: 1f);
			Scribe_Values.Look(ref fishingDelay, nameof(fishingDelay), defaultValue: 10000);
			Scribe_Values.Look(ref fishingSkillIncrease, nameof(fishingSkillIncrease), defaultValue: 5);
			Scribe_Values.Look(ref fishingPersists, nameof(fishingPersists), defaultValue: true);

			Scribe_Values.Look(ref burnRadiusOnRockets, nameof(burnRadiusOnRockets), defaultValue: true);
			Scribe_Values.Look(ref deployOnLanding, nameof(deployOnLanding), defaultValue: true);
			Scribe_Values.Look(ref airDefenses, nameof(airDefenses), defaultValue: true);
			Scribe_Values.Look(ref dynamicWorldDrawing, nameof(dynamicWorldDrawing), defaultValue: true);
			Scribe_Values.Look(ref delayDeployOnLanding, nameof(delayDeployOnLanding), defaultValue: 0);

			Scribe_Values.Look(ref drawUpgradeInformationScreen, nameof(drawUpgradeInformationScreen), defaultValue: true);
			Scribe_Values.Look(ref overrideDrawColors, nameof(overrideDrawColors), defaultValue: true);
		}

		public override void DrawSection(Rect rect)
		{
			GUIState.Push();
			{
				listingStandard = new Listing_Standard();

				Rect mainSettings = new Rect(rect.x + 20f, rect.y + 40f, rect.width - 40f, rect.height);

				listingStandard.ColumnWidth = mainSettings.width / 3;
				listingStandard.Begin(mainSettings);

				listingStandard.Header("VF_WorldMapGen".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.Gap(4);
				listingStandard.SliderLabeled("VF_BeachGenMultiplier".Translate(), "VF_BeachGenMultiplierTooltip".Translate(), "%", ref beachMultiplier, 0f, 2f, 100, 0);
				listingStandard.SliderLabeled("VF_ForceSettlementCoast".Translate(), "VF_ForceSettlementCoastTooltip".Translate(), $" {"VF_WorldTiles".Translate()}", ref forceFactionCoastRadius, 0,
					VehicleMod.MaxCoastalSettlementPush, 1, "VF_EverySettlementToCoast".Translate());

				listingStandard.Header("VF_SettingsGeneral".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.Gap(4);
				listingStandard.CheckboxLabeledWithMessage("VF_ModifiableSettings".Translate(), delegate (bool value)
				{
					return new Message("VF_WillRequireRestart".Translate(), MessageTypeDefOf.CautionInput);
				}, ref modifiableSettings, "VF_ModifiableSettingsTooltip".Translate());
				listingStandard.CheckboxLabeled("VF_CustomShaders".Translate(), ref useCustomShaders, "VF_CustomShadersTooltip".Translate());

				listingStandard.CheckboxLabeled("VF_DiagonalVehicleRendering".Translate(), ref allowDiagonalRendering, "VF_DiagonalVehicleRenderingTooltip".Translate());
				listingStandard.CheckboxLabeled("VF_FullVehiclePathing".Translate(), ref fullVehiclePathing, "VF_FullVehiclePathingTooltip".Translate());
				listingStandard.CheckboxLabeled("VF_SmoothVehiclePathing".Translate(), ref smoothVehiclePaths, "VF_SmoothVehiclePathingTooltip".Translate());
				GUIState.Disable();
				listingStandard.CheckboxLabeled("VF_RoadBiomeCostPathing".Translate(), ref vehiclePathingRoadNoCost, "VF_RoadBiomeCostPathingTooltip".Translate());
				GUIState.Enable();
				listingStandard.CheckboxLabeled("VF_MultiplePawnsPerJob".Translate(), ref multiplePawnsPerJob, "VF_MultiplePawnsPerJobTooltip".Translate());
				bool checkBefore = showDisabledVehicles;
				listingStandard.CheckboxLabeled("VF_ShowDisabledVehicles".Translate(), ref showDisabledVehicles, "VF_ShowDisabledVehiclesTooltip".Translate());
				listingStandard.Gap(4);

				if (checkBefore != showDisabledVehicles)
				{
					DefDatabase<DesignationCategoryDef>.AllDefs.ForEach(desCat => GizmoHelper.DesignatorsChanged(desCat));
				}

				listingStandard.Header("VF_SeaVehicles".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.CheckboxLabeled("VF_PassiveWaterWaves".Translate(), ref passiveWaterWaves, "VF_PassiveWaterWavesTooltip".Translate());

				listingStandard.NewColumn();
				string fishingHeader = "VF_Fishing".Translate();
				if (!Compatibility_VEFishing.Active)
				{
					GUIState.Disable();
					fishingHeader = "VF_FishingInactive".Translate();
				}

				listingStandard.Header(fishingHeader, ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.Gap(4);
				listingStandard.SliderLabeled("VF_FishingMultiplier".Translate(), "VF_FishingMultiplierTooltip".Translate(), "%", ref fishingMultiplier, 0.1f, 3, 100, 1);
				listingStandard.IntegerBox("VF_FishingDelay".Translate(), "VF_FishingDelayTooltip".Translate(), ref fishingDelay, listingStandard.ColumnWidth * 0.5f, 0, 0);
				listingStandard.Gap(8);
				listingStandard.IntegerBox("VF_FishingSkill".Translate(), "VF_FishingSkillTooltip".Translate(), ref fishingSkillIncrease, listingStandard.ColumnWidth * 0.5f, 0, 0);
				listingStandard.Gap(8);
				listingStandard.CheckboxLabeled("VF_FishingPersists".Translate(), ref fishingPersists, "VF_FishingPersistsTooltip".Translate());
				listingStandard.Gap(4);

				GUIState.Enable();

				listingStandard.Header("VF_AerialVehicles".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.Gap(4);
				listingStandard.CheckboxLabeled("VF_RocketsBurnRadius".Translate(), ref burnRadiusOnRockets, "VF_RocketsBurnRadiusTooltip".Translate());
				//listingStandard.CheckboxLabeled("VF_AirDefensesActive".Translate(), ref airDefenses, "VF_AirDefensesActiveTooltip".Translate());
				listingStandard.CheckboxLabeled("VF_DeployOnLanding".Translate(), ref deployOnLanding, "VF_DeployOnLandingTooltip".Translate());
				if (deployOnLanding)
				{
					listingStandard.Gap(16);
					listingStandard.SliderLabeled("VF_DelayOnLanding".Translate(), "VF_DelayOnLandingTooltip".Translate(), $" {"VF_DelaySeconds".Translate()}", ref delayDeployOnLanding, 0, 5, 1, 1);
				}
				listingStandard.CheckboxLabeled("VF_DynamicDrawing".Translate(), ref dynamicWorldDrawing, "VF_DynamicDrawingTooltip".Translate());
				listingStandard.Gap(8);

				GUIState.Disable();

				listingStandard.Header("VF_Upgrades".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.CheckboxLabeled("VF_DrawUpgradeInformationScreen".Translate(), ref drawUpgradeInformationScreen, "VF_DrawUpgradeInformationScreenTooltip".Translate());
				listingStandard.CheckboxLabeled("VF_OverrideDrawColor".Translate(), ref overrideDrawColors, "VF_OverrideDrawColorTooltip".Translate());

				GUIState.Enable();

				listingStandard.NewColumn();
				listingStandard.Header("VF_VehicleDamageMultipliers".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.Gap(4);
				listingStandard.SliderLabeled("VF_MeleeDamageMultiplier".Translate(), string.Empty, "%", ref meleeDamageMultiplier, 0, 2, multiplier: 100);
				listingStandard.SliderLabeled("VF_RangedDamageMultiplier".Translate(), string.Empty, "%", ref rangedDamageMultiplier, 0, 2, multiplier: 100);
				listingStandard.SliderLabeled("VF_ExplosiveDamageMultiplier".Translate(), string.Empty, "%", ref explosiveDamageMultiplier, 0, 2, multiplier: 100);

				listingStandard.Header("VF_VehicleTurrets".Translate(), ListingExtension.BannerColor, GameFont.Small, TextAnchor.MiddleCenter);
				listingStandard.CheckboxLabeled("VF_TurretOverheatMechanics".Translate(), ref overheatMechanics, "VF_TurretOverheatMechanicsTooltip".Translate());

				listingStandard.End();
			}
			GUIState.Pop();
		}
	}
}
