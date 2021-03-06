﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository {
    public class DeveloperRepository {

        public List<Developer> _listOfDevelopers = new List<Developer>();

        //Create
        public void AddToList(Developer developer) {
            _listOfDevelopers.Add(developer);

        }
        //Read
        public List<Developer> GetList() {
            return _listOfDevelopers;
        }
        //Update
        public bool UpdateDeveloper(int index, Developer updatedInfo) {
            //Find the developer
            Developer previousInfo = GetDeveloper(index);
            //Update the developer info
            if (previousInfo == null) {
                return false;
            }
            else {
                previousInfo.Name = updatedInfo.Name;
                previousInfo.BadgeNumber = updatedInfo.BadgeNumber;
                previousInfo.TypeOfAccess = updatedInfo.TypeOfAccess;
                previousInfo.Team = updatedInfo.Team;
                return true;
            }
        }
        //Delete
        public bool DeleteDeveloper(int number) {

            int count = _listOfDevelopers.Count;
            Developer dev = _listOfDevelopers.ElementAt(number - 1);
            if (dev.Team != null) {
                DevTeam team = dev.Team;
                team.TeamMembers.Remove(dev);
                
            }

            _listOfDevelopers.RemoveAt(number - 1);
            if (count > _listOfDevelopers.Count) {

                return true;
            }
            else {
                return false;
            }
        }



        //Helper Methods
        public Developer GetDeveloper(string name) {
            foreach (Developer developer in _listOfDevelopers) {
                if (developer.Name == name) {
                    return developer;
                }
            }
            return null;
        }
        public Developer GetDeveloper(int number) {
            if (_listOfDevelopers.ElementAt(number - 1) != null) {
                return _listOfDevelopers.ElementAt(number - 1);

            }
            else
                return null;
        }



    }

}




