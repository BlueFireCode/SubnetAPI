namespace SubnetApi.Models
{
	public class SupernetOut
	{
		public Net[] Nets { get; set; }
		public Free FreeIP { get; set; }
	}

	public class Net
	{
		public int Index { get; set; }
		public int OCT1 { get; set; }
		public int OCT2 { get; set; }
		public int OCT3 { get; set; }
		public int OCT4 { get; set; }
		public string NWA { get; set; }
		public string BRD { get; set; }
		public string FFIP { get; set; }
		public string LFIP { get; set; }
	}

}
