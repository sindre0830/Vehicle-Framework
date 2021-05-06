﻿using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;

namespace Vehicles
{
	[DefOf]
	public static class AerialVehicleArrivalModeDefOf
	{
		public static AerialVehicleArrivalModeDef EdgeDrop;

		public static AerialVehicleArrivalModeDef CenterDrop;

		public static AerialVehicleArrivalModeDef TargetedLanding;

		static AerialVehicleArrivalModeDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(AerialVehicleArrivalModeDefOf));
		}
	}
}
