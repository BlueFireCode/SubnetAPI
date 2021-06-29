namespace SubnetApi.Models
{
	public class SubnetOut
	{
		public string IP { get; set; }
		public string IPBin { get; set; }
		public string SNM { get; set; }
		public string SNMBin { get; set; }
		public string NWA { get; set; }
		public string NWABin { get; set; }
		public string BRD { get; set; }
		public string BRDBin { get; set; }
		public string FFIP { get; set; }
		public string FFIPBin { get; set; }
		public string LFIP { get; set; }
		public Free FreeIP { get; set; }
		public Free FreeSub { get; set; }
	}

	public class Free
	{
		public string Calc { get; set; }
		public int Val { get; set; }
	}
}
