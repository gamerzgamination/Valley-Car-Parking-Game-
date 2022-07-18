using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Missionstatementcontroler
{
    private static string statement;
    public static string Selectstatement(int level)
    {

        switch (level)
        {
            // Even though you have reached a safe corner but be alert, you're being chased.
            // Main gate of our garrison is breached. Ready your guns, a swamp of enemies is coming.
            // Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher.
            case 0:
                switch (Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode)
                {
                    case 0:
                        statement = "An Enemy Helicopter is Approaching, Beware of Para-Troopers.";
                        break;
                    case 1:
                        statement = " Even though you have reached a safe corner but be alert, you're being chased.";
                        break;
                }
                break;
            case 1:
                switch (Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode)
                {
                    case 0:
                        statement = "Our comrade scientist is being held captive by the enemies, eliminate them to free him. ";
                        break;
                    case 1:
                        statement = "Main gate of our garrison is breached. Ready your guns, a swamp of enemies is coming.";
                        break;
                }
                break;
            case 2:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 3:
                statement = "City is under attack, defend the city and people";
                break;
            case 4:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 5:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 6:
                statement = "Fort is under attack, defend your people";
                break;
            case 7:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 8:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 9:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 10:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 11:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 12:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 13:
                statement = "City is under attack, defend the city and people";
                break;
            case 14:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 15:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 16:
                statement = "Fort is under attack, defend your people";
                break;
            case 17:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 18:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 19:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 20:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 21:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 22:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 23:
                statement = "City is under attack, defend the city and people";
                break;
            case 24:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 25:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 26:
                statement = "Fort is under attack, defend your people";
                break;
            case 27:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 28:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 29:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 30:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 31:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 32:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 33:
                statement = "City is under attack, defend the city and people";
                break;
            case 34:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 35:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 36:
                statement = "Fort is under attack, defend your people";
                break;
            case 37:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 38:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 39:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 40:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 41:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 42:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 43:
                statement = "City is under attack, defend the city and people";
                break;
            case 44:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 45:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 46:
                statement = "Fort is under attack, defend your people";
                break;
            case 47:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 48:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 49:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 50:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 51:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 52:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 53:
                statement = "City is under attack, defend the city and people";
                break;
            case 54:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 55:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 56:
                statement = "Fort is under attack, defend your people";
                break;
            case 57:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 58:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 59:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 60:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 61:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 62:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 63:
                statement = "City is under attack, defend the city and people";
                break;
            case 64:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 65:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 66:
                statement = "Fort is under attack, defend your people";
                break;
            case 67:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 68:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 69:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 70:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 71:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 72:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 73:
                statement = "City is under attack, defend the city and people";
                break;
            case 74:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 75:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 76:
                statement = "Fort is under attack, defend your people";
                break;
            case 77:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 78:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 79:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 80:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 81:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 82:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 83:
                statement = "City is under attack, defend the city and people";
                break;
            case 84:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 85:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 86:
                statement = "Fort is under attack, defend your people";
                break;
            case 87:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 88:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 89:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
            case 90:
                statement = "Enemies are patrolling around the station, clear the area";
                break;
            case 91:
                statement = "A scientist is guarded by soldiers, eliminate them to free scientist";
                break;
            case 92:
                statement = "Terrorists are going to attack the city, attack them first and eliminate them";
                break;
            case 93:
                statement = "City is under attack, defend the city and people";
                break;
            case 94:
                statement = "Clear the base and make sure no one makes it out alive";
                break;
            case 95:
                statement = "Patrolling guards are approaching you, kill them all";
                break;
            case 96:
                statement = "Fort is under attack, defend your people";
                break;
            case 97:
                statement = "Terrorists killed the army major, avenge his death";
                break;
            case 98:
                statement = "A family is threatened by few goons, eliminate them to protect the family";
                break;
            case 99:
                statement = "Red Alert!!! a group of enemy helicopters is spotted coming towards us. Ready your rocket launcher";
                break;
        }
   
        return statement;
    }

}
