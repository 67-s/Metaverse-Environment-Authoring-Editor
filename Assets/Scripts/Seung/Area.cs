using System;

public class Area
{
	public int FstRow { get; set; }
	public int FstCol { get; set; }
	public int SndRow { get; set; }
	public int SndCol { get; set; }

	public Area(int fR, int fC, int sR, int sC)
	{
		FstRow = fR;
		FstCol = fC;
		SndRow = sR;
		SndCol = sC;
	}

	public override bool Equals(object obj)
	{
		var other = obj as Area;
		if (other == null)
			throw new ArgumentException();
		return FstRow == other.FstRow && FstCol == other.FstCol && SndRow == other.SndRow && SndCol == other.SndCol;
	}

	public override int GetHashCode()
	{
		return FstRow.GetHashCode() + FstCol.GetHashCode() * 31 + SndRow.GetHashCode() * 37 + SndCol.GetHashCode() * 41;
	}
}