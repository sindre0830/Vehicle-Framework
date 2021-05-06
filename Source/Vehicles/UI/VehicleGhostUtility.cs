﻿using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using Verse;
using RimWorld;
using SmashTools;

namespace Vehicles.UI
{
	public static class VehicleGhostUtility
	{
		public static Dictionary<int, Graphic> cachedGhostGraphics = new Dictionary<int, Graphic>();

		public static Graphic GhostGraphicFor(this VehicleDef vehicleDef, VehicleTurret cannon, Color ghostColor)
		{
			int num = 0;
			num = Gen.HashCombine(num, vehicleDef);
			num = Gen.HashCombine(num, cannon);
			num = Gen.HashCombineStruct(num, ghostColor);
			if (!cachedGhostGraphics.TryGetValue(num, out Graphic graphic))
			{
				cannon.ResolveCannonGraphics(vehicleDef, true);
				graphic = cannon.CannonGraphic;

				GraphicData graphicData = new GraphicData();
				AccessTools.Method(typeof(GraphicData), "Init").Invoke(graphicData, new object[] { });
				graphicData.CopyFrom(graphic.data);
				graphicData.shadowData = null;

				graphic = GraphicDatabase.Get(graphic.GetType(), graphic.path, ShaderTypeDefOf.EdgeDetect.Shader, graphic.drawSize, ghostColor, Color.white, graphicData, null);
				
				cachedGhostGraphics.Add(num, graphic);
			}
			return graphic;
		}

		public static void DrawGhostCannonTextures(this VehicleDef vehicleDef, Vector3 loc, Rot8 rot, Color ghostCol)
		{
			if (vehicleDef.GetCompProperties<CompProperties_Cannons>() is CompProperties_Cannons props)
			{
				foreach (VehicleTurret cannon in props.turrets)
				{
					if (cannon.NoGraphic)
					{
						continue;
					}

					cannon.ResolveCannonGraphics(vehicleDef);

					try
					{
						Graphic graphic = vehicleDef.GhostGraphicFor(cannon, ghostCol);
						
						Vector3 topVectorRotation = new Vector3(loc.x, 1f, loc.y).RotatedBy(0f);
						float locationRotation = cannon.defaultAngleRotated + rot.AsAngle;
						if(cannon.attachedTo != null)
						{
							locationRotation += cannon.attachedTo.defaultAngleRotated + rot.AsAngle;
						}
						Pair<float, float> drawOffset = RenderHelper.ShipDrawOffset(Rot8.North, cannon.turretRenderLocation.x, cannon.turretRenderLocation.y, out Pair<float, float> rotOffset1, locationRotation, cannon.attachedTo);

						Vector3 topVectorLocation = new Vector3(loc.x + drawOffset.First + rotOffset1.First, loc.y + cannon.drawLayer, loc.z + drawOffset.Second + rotOffset1.Second);
						Mesh cannonMesh = graphic.MeshAt(Rot4.North);
						
						Graphics.DrawMesh(cannonMesh, topVectorLocation, locationRotation.ToQuat(), graphic.MatAt(Rot4.North), 0);

						if(cannon.CannonBaseMaterial != null)
						{
							Matrix4x4 baseMatrix = default;
							Pair<float, float> baseDrawOffset = RenderHelper.ShipDrawOffset(Rot8.North, cannon.baseCannonRenderLocation.x, cannon.baseCannonRenderLocation.y, out Pair<float, float> rotOffset2);
							Vector3 baseVectorLocation = new Vector3(loc.x + baseDrawOffset.First, loc.y, loc.z + baseDrawOffset.Second);
							baseMatrix.SetTRS(baseVectorLocation + Altitudes.AltIncVect, rot.AsQuat, new Vector3(cannon.baseCannonDrawSize.x, 1f, cannon.baseCannonDrawSize.y));
							Graphics.DrawMesh(MeshPool.plane10, baseMatrix, cannon.CannonBaseMaterial, 0);
						}
					}
					catch(Exception ex)
					{
						Log.Error($"Failed to render Cannon=\"{cannon.turretDef.defName}\" for VehicleDef=\"{vehicleDef.defName}\", Exception: {ex.Message}");
					}
				}
			}
		}
	}
}
