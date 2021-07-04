using SubnetApi.Models;
using System;
using System.Security.Cryptography;

namespace SubnetApi.Objects
{
	public class Subnet
	{
        private string rawIpBin;
		public string IP { get; set; }
		public string IPBinary { get; set; }
        private string rawSnmBin;
		public string Subnetmask { get; set; }
		public string SubnetmaskBinary { get; set; }
        private string rawNetAddrBin;
		public string NetworkAdress { get; set; }
		public string NetworkAdressBinary { get; set; }
        private string rawBroadBin;
		public string Broadcast { get; set; }
		public string BroadcastBinary { get; set; }
		public string FirstFreeIP { get; set; }
		public string FirstFreeIPBinary { get; set; }
		public string LastFreeIP { get; set; }
		public string LastFreeIPBinary { get; set; }
		public string FreeIPsCalculation { get; set; }
		public int FreeIPsValue { get; set; }
		public string FreeSubnetsCalculation { get; set; }
		public int FreeSubnetsValue { get; set; }

		public void SetRawBinIP(string oc1, string oc2, string oc3, string oc4)
		{
            rawIpBin = "";
            if (oc1 == "0")
            {
                rawIpBin += "00000000";
            }
            else
            {
                rawIpBin += Convert.ToString(int.Parse(oc1), 2);
                while (rawIpBin.Length != 8)
                    rawIpBin = "0" + rawIpBin;
            }
            if (oc2 == "0")
            {
                rawIpBin += "00000000";
            }
            else
            {
                string temp = Convert.ToString(int.Parse(oc2), 2);
                while (temp.Length != 8)
                    temp = "0" + temp;
                rawIpBin += temp;
            }
            if (oc3 == "0")
            {
                rawIpBin += "00000000";
            }
            else
            {
                string temp = Convert.ToString(int.Parse(oc3), 2);
                while (temp.Length != 8)
                    temp = "0" + temp;
                rawIpBin += temp;
            }
            if (oc4 == "0")
            {
                rawIpBin += "00000000";
            }
            else
            {
                string temp = Convert.ToString(int.Parse(oc4), 2);
                while (temp.Length != 8)
                    temp = "0" + temp;
                rawIpBin += temp;
            }
        }

        public void SetIP(string oc1, string oc2, string oc3, string oc4)
		{
            IP = oc1 + "." + oc2 + "." + oc3 + "." + oc4;
            IPBinary = rawIpBin.Substring(0, 8) + "."
                + rawIpBin.Substring(8, 8) + "."
                + rawIpBin.Substring(16, 8) + "."
                + rawIpBin[24..];
        }

        public void SetSnm(int cidr)
		{
            string subnetmask = "";
            for (int bit = 0; bit < 32; bit++)
            {
                if (cidr > 0)
                {
                    subnetmask += "1";
                    cidr--;
                }
                else
                {
                    subnetmask += "0";
                }
            }

            Subnetmask = Convert.ToInt32(subnetmask.Substring(0, 8), 2) + "."
                + Convert.ToInt32(subnetmask.Substring(8, 8), 2) + "."
                + Convert.ToInt32(subnetmask.Substring(16, 8), 2) + "."
                + Convert.ToInt32(subnetmask.Substring(24), 2);
            rawSnmBin = subnetmask;
            SubnetmaskBinary = subnetmask.Substring(0, 8) + "."
                + subnetmask.Substring(8, 8) + "."
                + subnetmask.Substring(16, 8) + "."
                + subnetmask[24..];
        }

        public void SetNetAdress()
		{
            string netaddress = "";
            for (int bit = 0; bit < 32; bit++)
            {
                if (rawSnmBin[bit] == '1')
                    netaddress += rawIpBin[bit];
                else
                    netaddress += "0";
            }
            NetworkAdress = Convert.ToInt32(netaddress.Substring(0, 8), 2) + "."
                + Convert.ToInt32(netaddress.Substring(8, 8), 2) + "."
                + Convert.ToInt32(netaddress.Substring(16, 8), 2) + "."
                + Convert.ToInt32(netaddress[24..], 2);
            rawNetAddrBin = netaddress.Substring(0, 8) + netaddress.Substring(8, 8) + netaddress.Substring(16, 8) + netaddress[24..];
            NetworkAdressBinary = netaddress.Substring(0, 8) + "."
                + netaddress.Substring(8, 8) + "."
                + netaddress.Substring(16, 8) + "."
                + netaddress[24..];
        }

        public void SetBroadcast()
		{
            string broadcast = "";
            for (int bit = 0; bit < 32; bit++)
            {
                if (rawSnmBin[bit] == '1')
                    broadcast += rawIpBin[bit];
                else
                    broadcast += "1";
            }
            Broadcast = Convert.ToInt32(broadcast.Substring(0, 8), 2) + "."
                + Convert.ToInt32(broadcast.Substring(8, 8), 2) + "."
                + Convert.ToInt32(broadcast.Substring(16, 8), 2) + "."
                + Convert.ToInt32(broadcast[24..], 2);
            rawBroadBin = broadcast.Substring(0, 8) + broadcast.Substring(8, 8) + broadcast.Substring(16, 8) + broadcast[24..];
            BroadcastBinary = broadcast.Substring(0, 8) + "."
                + broadcast.Substring(8, 8) + "."
                + broadcast.Substring(16, 8) + "."
                + broadcast[24..];
        }

        public void SetFirstFree()
		{
            string rawFirstFree = "";
            for (int bit = 31; bit > -1; bit--)
            {
                if (rawNetAddrBin[bit] == '0')
                {
                    rawFirstFree = rawNetAddrBin.Substring(0, bit) + "1";
                    break;
                }
            }

            while (rawFirstFree.Length < 32)
                rawFirstFree += "0";

            FirstFreeIP = Convert.ToInt32(rawFirstFree.Substring(0, 8), 2) + "."
                + Convert.ToInt32(rawFirstFree.Substring(8, 8), 2) + "."
                + Convert.ToInt32(rawFirstFree.Substring(16, 8), 2) + "."
                + Convert.ToInt32(rawFirstFree[24..], 2);
            FirstFreeIPBinary = rawFirstFree.Substring(0, 8) + "."
                + rawFirstFree.Substring(8, 8) + "."
                + rawFirstFree.Substring(18, 8) + "."
                + rawFirstFree[24..];
        }

        public void SetLastFree()
		{
            string rawLastFree = "";
            for (int bit = 31; bit > -1; bit--)
            {
                if (rawBroadBin[bit] == '1')
                {
                    rawLastFree = rawBroadBin.Substring(0, bit) + "0";
                    break;
                }
            }

            while (rawLastFree.Length < 32)
                rawLastFree += "1";

            LastFreeIP = Convert.ToInt32(rawLastFree.Substring(0, 8), 2) + "."
                + Convert.ToInt32(rawLastFree.Substring(8, 8), 2) + "."
                + Convert.ToInt32(rawLastFree.Substring(16, 8), 2) + "."
                + Convert.ToInt32(rawLastFree[24..], 2);
            LastFreeIPBinary = rawLastFree.Substring(0, 8) + "."
                + rawLastFree.Substring(8, 8) + "."
                + rawLastFree.Substring(18, 8) + "."
                + rawLastFree[24..];
        }

        public void SetAllFree(int cidr)
		{
            FreeIPsCalculation = cidr == 32 ? "-" : $"2^{32 - cidr} - 2";
            FreeIPsValue = cidr == 32 ? 0 : (int)Math.Pow(2, 32 - cidr) - 2;
            FreeSubnetsCalculation = cidr == 0 ? "-" : $"2^{cidr}";
            FreeSubnetsValue = cidr == 0 ? 0 : (int)Math.Pow(2, cidr);
        }

        public SubnetOut ToModel()
		{
            SubnetOut output = new()
			{
                IP = IP,
                IPBin = IPBinary,
                SNM = Subnetmask,
                SNMBin = SubnetmaskBinary,
                NWA = NetworkAdress,
                NWABin = NetworkAdressBinary,
                BRD = Broadcast,
                BRDBin = BroadcastBinary,
                FFIP = FirstFreeIP,
                FFIPBin = FirstFreeIPBinary,
                LFIP = LastFreeIP,
                LFIPBin = LastFreeIPBinary,
                FreeIP = new Free
                {
                    Calc = FreeIPsCalculation,
                    Val = FreeIPsValue
                },
                FreeSub = new Free
                {
                    Calc = FreeSubnetsCalculation,
                    Val = FreeSubnetsValue
                }
            };
            return output;
		}
	}
}
