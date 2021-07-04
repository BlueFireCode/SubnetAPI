using Microsoft.AspNetCore.Mvc;
using SubnetApi.Models;
using SubnetApi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SubnetApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SubnetController : ControllerBase
	{
		[HttpPost]
		public string Post([FromBody]SubnetInput obj)
		{
            try
            {
                if (obj.OCT1 > 255 || obj.OCT1 < 0 ||
                    obj.OCT2 > 255 || obj.OCT2 < 0 ||
                    obj.OCT3 > 255 || obj.OCT3 < 0 ||
                    obj.OCT4 > 255 || obj.OCT4 < 0 ||
                    obj.SNM> 32 || obj.SNM < 0)
                    return "Only numbers between 0 and 255 (inclusive) are allowed for each octet, or between 0 and 32 for the subnetmask.";
            }
            catch
            {
                return "Only numbers allowed!";
			}

            Subnet subnet = new();

            subnet.SetRawBinIP(obj.OCT1.ToString(), obj.OCT2.ToString(), obj.OCT3.ToString(), obj.OCT4.ToString());
            subnet.SetIP(obj.OCT1.ToString(), obj.OCT2.ToString(), obj.OCT3.ToString(), obj.OCT4.ToString());
            subnet.SetSnm(obj.SNM);
            subnet.SetNetAdress();
            subnet.SetBroadcast();
			if (obj.SNM != 32 && obj.SNM != 0)
			{
                subnet.SetFirstFree();
                subnet.SetLastFree();
			}
            subnet.SetAllFree(obj.SNM);

            return JsonSerializer.Serialize<SubnetOut>(subnet.ToModel());
        }
	}
}
