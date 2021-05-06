﻿using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld;

namespace Vehicles
{
	public class VehiclePositionManager : MapComponent
	{
		private readonly Dictionary<VehiclePawn, HashSet<IntVec3>> occupiedRect = new Dictionary<VehiclePawn, HashSet<IntVec3>>();

		public VehiclePositionManager(Map map) : base(map)
		{
			occupiedRect = new Dictionary<VehiclePawn, HashSet<IntVec3>>();
		}

		public bool PositionClaimed(IntVec3 cell) => occupiedRect.Values.Any(h => h.Contains(cell));

		public VehiclePawn ClaimedBy(IntVec3 cell) => occupiedRect.FirstOrDefault(v => v.Value.Contains(cell)).Key;

		public void ClaimPosition(VehiclePawn vehicle)
		{
			int x = vehicle.def.Size.x;
			int z = vehicle.def.Size.z;
			if (vehicle.Rotation.IsHorizontal)
			{
				int tmp = x;
				x = z;
				z = tmp;
			}
			CellRect newRect = CellRect.CenteredOn(vehicle.Position, x, z);
			var hash = newRect.Cells.ToHashSet();
			if (occupiedRect.TryGetValue(vehicle, out HashSet<IntVec3> _))
			{
				occupiedRect[vehicle] = hash;
			}
			else
			{
				occupiedRect.Add(vehicle, hash);
			}
		}

		public bool ReleaseClaimed(VehiclePawn vehicle)
		{
			return occupiedRect.Remove(vehicle);
		}
	}
}
