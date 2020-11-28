using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository {
    public enum AccessType {
        no,
        yes
    }
    public class Developer {
        public DevTeam Team { get; set; }
        public int BadgeNumber { get; set; }
        public string Name { get; set; }
        public AccessType TypeOfAccess { get; set; }
        public string TeamAffiliation { get; set; }

        public Developer() { }
        public Developer(string name, int num, AccessType yesOrNo, string team) {
            Name = name;
            BadgeNumber = num;
            TypeOfAccess = yesOrNo;
            TeamAffiliation = team;
        }
        public Developer(string name, int num, AccessType yesOrNo) { 
            Name = name;
            BadgeNumber = num;
            TypeOfAccess = yesOrNo;
           
        }
    }
}
