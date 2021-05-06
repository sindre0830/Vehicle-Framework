﻿using System.Linq;
using Verse;
using SmashTools;
using Vehicles.AI;

namespace Vehicles
{
	public class WaterRegionLink
	{
		public WaterRegion[] regions = new WaterRegion[2];

		public EdgeSpan span;

		public WaterRegion RegionA
		{
			get
			{
				return regions[0];
			}
			set
			{
				regions[0] = value;
			}
		}

		public WaterRegion RegionB
		{
			get
			{
				return regions[1];
			}
			set
			{
				regions[1] = value;
			}
		}

		public void Register(WaterRegion reg)
		{
			if (regions[0] == reg || regions[1] == reg)
			{
				Log.Error(string.Concat(new object[]
				{
					"Tried to double-register water region ",
					reg.ToString(), " in ", this
				}));
				return;
			}
			if (RegionA is null || !RegionA.valid)
			{
				RegionA = reg;
			}
			else if (RegionB is null || !RegionB.valid)
			{
				RegionB = reg;
			}
			else
			{
				Log.Error(string.Concat(new object[]
				{
					"Count not register water region ",
					reg.ToString(), " in link ", this,
					": > 2 water regiosn on link!\nWaterRegionA: ", RegionA.DebugString,
					"\nRegionB: ", RegionB.DebugString
				}));
			}
		}

		public void Deregister(WaterRegion reg)
		{
			if(RegionA == reg)
			{
				RegionA = null;
				if(RegionB is null)
				{
					reg.Map.GetCachedMapComponent<WaterMap>().WaterRegionLinkDatabase.Notify_LinkHasNoRegions(this);
				}
			}
			else if (RegionB == reg)
			{
				RegionB = null;
				if(RegionA is null)
				{
					reg.Map.GetCachedMapComponent<WaterMap>().WaterRegionLinkDatabase.Notify_LinkHasNoRegions(this);
				}
			}
		}

		public WaterRegion GetOtherRegion(WaterRegion reg)
		{
			return (reg != RegionA) ? RegionA : RegionB;
		}

		public ulong UniqueHashCode()
		{
			return span.UniqueHashCode();
		}

		public override string ToString()
		{
			string text = (from r in regions
						   where !(r is null)
						   select r.id.ToString()).ToCommaList(false);
			string text2 = string.Concat(new object[]
			{
				"spawn=", span.ToString(), " hash=", UniqueHashCode()
			});
			return string.Concat(new string[]
			{
				"(", text2, ", water regions=", text, ")"
			});
		}
	}
}
