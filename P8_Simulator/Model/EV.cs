using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8_Simulator.Model
{
    internal class EV
    {
        public struct Charge
        {
            public Charge(int max, int target, int lower, int current)
            {
                Max = max;
                Target = target;
                Lower = lower;
                Current = current;
            }

            public int Max { get;  init; }
            public int Target { get; init; }
            public int Lower { get; init; }
            public int Current { get; init; }
        }

        public struct TimeInterval
        {
            public TimeInterval(int start, int end)
            {
                Start = start;
                End = end;
            }

            public int Start { get; init; }
            public int End { get; init; }
        }

        public Charge Charges { get; private set; }

        public int ChargeRate { get; private set; }

        public TimeInterval Time { get; private set; }


        public EV SetCharges(Charge c)
        {
            Charges = c;
            return this;
        }

        public EV SetChargeRate(int rate)
        {
            ChargeRate = rate;
            return this;
        }

        public EV SetTime(TimeInterval time)
        {
            Time = time;
            return this;
        }

        
    }
}
