using System;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace UnityPlugins.Benchmark.Tests
{
	/// <summary>
	/// Vector3 constructor seems to be significantly slower than manually assigning x,y,z fields in both debug and release mode in editor.
	/// With Vector3Int it's different. While in debug mode x,y,z is slower due to these fields being properties (not a case in vector3).
	/// In release mode inlining makes it on part with constructor.
	/// </summary>
	public class Vector_Constructor
	{
		private const int REPEATS = 10000000;

		[Test, Performance]
		public void Vector3_Constructor()
		{
			RunTest(() =>
			{
				Vector3 vec;
				for(int x = 0; x < REPEATS; ++x)
				{
					vec = new Vector3(x, 0, 0);
				}
			});
		}

		[Test, Performance]
		public void Vector3_EmptyConstructor()
		{
			RunTest(() =>
			{
				Vector3 vec;
				for(int x = 0; x < REPEATS; ++x)
				{
					vec = new Vector3();
					vec.x = x;
					vec.y = 0;
					vec.z = 0;
				}
			});
		}

		[Test, Performance]
		public void Vector3_Manual()
		{
			RunTest(() =>
			{
				Vector3 vec;
				for(int x = 0; x < REPEATS; ++x)
				{
					vec = default;
					vec.x = x;
					vec.y = 0;
					vec.z = 0;
				}
			});
		}

		[Test, Performance]
		public void Vector3Int_Constructor()
		{
			RunTest(() =>
			{
				Vector3Int vec;
				for(int x = 0; x < REPEATS; ++x)
				{
					vec = new Vector3Int(x, 0, 0);
				}
			});
		}

		[Test, Performance]
		public void Vector3Int_EmptyConstructor()
		{
			RunTest(() =>
			{
				Vector3Int vec;
				for(int x = 0; x < REPEATS; ++x)
				{
					vec = new Vector3Int();
					vec.x = x;
					vec.y = 0;
					vec.z = 0;
				}
			});
		}

		[Test, Performance]
		public void Vector3Int_Manual()
		{
			RunTest(() =>
			{
				Vector3Int vec;
				for(int x = 0; x < REPEATS; ++x)
				{
					vec = default;
					vec.x = x;
					vec.y = 0;
					vec.z = 0;
				}
			});
		}

		private void RunTest(Action method)
		{
			Measure.Method(method)
				.WarmupCount(5)
				.MeasurementCount(5)
				.IterationsPerMeasurement(5)
				.GC()
				.Run();
		}
	}
}
