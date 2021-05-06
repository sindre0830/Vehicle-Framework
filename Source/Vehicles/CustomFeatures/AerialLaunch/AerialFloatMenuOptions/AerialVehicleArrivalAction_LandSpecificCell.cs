﻿using Verse;
using RimWorld;
using RimWorld.Planet;
using SmashTools;

namespace Vehicles
{
	public class AerialVehicleArrivalAction_LandSpecificCell : AerialVehicleArrivalAction_LandInMap
	{
		protected IntVec3 landingCell;
		protected Rot4 landingRot;

		public AerialVehicleArrivalAction_LandSpecificCell()
		{
		}

		public AerialVehicleArrivalAction_LandSpecificCell(VehiclePawn vehicle, MapParent mapParent, int tile, LaunchProtocol launchProtocol, IntVec3 landingCell, Rot4 landingRot) : base(vehicle, mapParent, tile, launchProtocol)
		{
			this.tile = tile;
			this.mapParent = mapParent;
			this.launchProtocol = launchProtocol;
			this.landingCell = landingCell;
			this.landingRot = landingRot;
		}

		public override FloatMenuAcceptanceReport StillValid(int destinationTile)
		{
			return Find.World.GetCachedWorldComponent<WorldVehiclePathGrid>().Passable(tile, vehicle.VehicleDef);
		}

		public override void Arrived(int tile)
		{
			VehicleSkyfaller_Arriving skyfaller = (VehicleSkyfaller_Arriving)ThingMaker.MakeThing(vehicle.CompVehicleLauncher.Props.skyfallerIncoming);
			skyfaller.vehicle = vehicle;
			skyfaller.launchProtocol = launchProtocol;
			GenSpawn.Spawn(skyfaller, landingCell, mapParent.Map, landingRot);
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref landingCell, "landingCell");
			Scribe_Values.Look(ref landingRot, "landingRot");
		}
	}
}
