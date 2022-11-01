using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaData
{
	public int FstRow { get; set; }
	public int FstCol { get; set; }
	public int SndRow { get; set; }
	public int SndCol { get; set; }

	public AreaData(int fR, int fC, int sR, int sC)
	{
		FstRow = fR;
		FstCol = fC;
		SndRow = sR;
		SndCol = sC;
	}
}