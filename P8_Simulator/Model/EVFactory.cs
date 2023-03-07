using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8_Simulator.Model
{
    internal class EVFactory
    {
        public struct EvParams
        {
            public EvParams(int maxCharge, int chargeRateMin, int chargeRateMax, int timeMin, int timeMax)
            {
                MaxCharge = maxCharge;
                ChargeRateMin = chargeRateMin;
                ChargeRateMax = chargeRateMax;
                TimeMin = timeMin;
                TimeMax = timeMax;
            }

            public int MaxCharge { get; set; } = 100;
            public int ChargeRateMin { get; set; } = 0;
            public int ChargeRateMax { get; set; } = 100;
            public int TimeMin { get; set; } = 0;
            public int TimeMax { get; set; } = 24 * 7;
        }

        public EvParams Parameters { get; init; }

        public EVFactory(EvParams evParams)
        {
            Parameters = evParams;
        }


        public List<EV> GetN(int n)
        {
            var rand = new Random();

            var evs = new List<EV>();
            for (var i = 0; i < n; i++)
            {
                var ev = new EV();


                var max = rand.Next(Parameters.MaxCharge);
                var current = rand.Next(max);
                var target = rand.Next(current, max);
                var lower = rand.Next(target);

                var start = rand.Next(Parameters.TimeMin, Parameters.TimeMax);
                var end = rand.Next(start, Parameters.TimeMax);

                ev
                    .SetCharges(new EV.Charge(max, target, lower, current))
                    .SetChargeRate(rand.Next(Parameters.ChargeRateMin, Parameters.ChargeRateMax))
                    .SetTime(new EV.TimeInterval(start, end));

                evs.Add(ev);
            }

            return evs;
        }
    }
}
