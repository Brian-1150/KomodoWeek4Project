using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository {
    public class DevTeam {

        public string TeamName { get; set; }
        public List<Developer> TeamMembers { get; set; }
        public int TeamID { get; set; }
        

        public DevTeam() { }
        public DevTeam(string name) {
            TeamName = name;
        }
        public DevTeam(string name, int id) {
            TeamName = name;
            TeamID = id;
        }
        public DevTeam(string name, int id, List<Developer> developers) {
            TeamName = name;
            TeamID = id;
            TeamMembers = developers;
        }

    }
}
