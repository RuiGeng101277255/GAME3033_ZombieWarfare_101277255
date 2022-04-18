using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private static char saveSignifier = '`';

    private static SaveLoadManager _Instance;

    //Singleton
    public static SaveLoadManager Instance()
    {
        if (_Instance == null)
        {
            _Instance = new SaveLoadManager();
        }

        return _Instance;
    }

    //Saves the content of calendar based on the specific year, month and the list of days within that month
    public void SaveContents(Vector3 playerPosition, float playerHealth, int zombieWave, WeaponComponentScript equippedWeapon, InventoryComponent inventory)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + Path.DirectorySeparatorChar + "/Data/SavedGameData/saveContents.txt");
        string contentsToSave = "";

        contentsToSave += playerPosition.x + '`' + playerPosition.y + '`' + playerPosition.z + '`' + playerHealth + '`' + zombieWave;

        //foreach (DayScript d in days)
        //{
        //    AllDetailsFromMonth += (SaveLoadSignifiers.DaySeparator + d.dayNumber);
        //
        //    //Special icon events
        //    AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + d.isDayHoliday);
        //    AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + d.isDayBirthday);
        //    AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + d.isDayLesson);
        //
        //    //Actual details
        //    foreach (string s in d.DayDetails)
        //    {
        //        AllDetailsFromMonth += (SaveLoadSignifiers.DetailSeparator + s);
        //    }
        //}
        //
        //writer.WriteLine(AllDetailsFromMonth);
        writer.Close();
    }

    public string LoadStringContentsByMonthAndYear()
    {
        //Loads the content of the files if it exist

        string loadedString = "";

        if (File.Exists(Application.dataPath + Path.DirectorySeparatorChar + "/Data/SavedGameData/saveContents.txt"))
        {
            StreamReader reader = new StreamReader(Application.dataPath + Path.DirectorySeparatorChar + "/Data/SavedGameData/saveContents.txt");
            loadedString = reader.ReadLine();
        }

        return loadedString;
    }
}
