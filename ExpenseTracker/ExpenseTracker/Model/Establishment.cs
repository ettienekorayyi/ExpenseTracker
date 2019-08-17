using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    public class Establishment
    {
        public int EstablishmentId { get; set; }
        public string establishments { get; set; }

        public Establishment() { }

        public Establishment(int pkId, string eName)
        {
            EstablishmentId = pkId;
            establishments = eName;
        }

        public int HiddenValue
        {
            get { return EstablishmentId; }
        }

        public override string ToString()
        {
            return establishments;
        }
    }
}
