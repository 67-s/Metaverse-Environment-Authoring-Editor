using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data Structure for Build Algorithm
public class AreaData
{
	public int FstRow { get; set; }
	public int FstCol { get; set; }
	public int SndRow { get; set; }
	public int SndCol { get; set; }
	public int BuildIndex { get; set; }

	public AreaData(int fR, int fC, int sR, int sC, int buildIndex)
	{
		FstRow = fR;
		FstCol = fC;
		SndRow = sR;
		SndCol = sC;
		BuildIndex = buildIndex;
	}

	public AreaData(Area area, int buildIndex)
	{
		FstRow = area.FstRow;
		FstCol = area.FstCol;
		SndRow = area.SndRow;
		SndCol = area.SndCol;
		BuildIndex = buildIndex;
	}
}