using System;
using static System.Console;
using System.Globalization;
class GreenvilleRevenue
{
    static void Main()
    {
        const int ENTRANCE_FEE = 25;
        const int MIN_CONTESTANTS = 0;
        const int MAX_CONTESTANTS = 30;
        int num;
        int revenue;

        //Instantiate an array of Contestant class objects
        Contestant[] contestants = new Contestant[MAX_CONTESTANTS];

        num = getContestantNumber(MIN_CONTESTANTS, MAX_CONTESTANTS);

        //Calculate the expected revenue and write out
        revenue = num * ENTRANCE_FEE;
        WriteLine("Revenue expected this year is {0}", revenue.ToString("C", CultureInfo.GetCultureInfo("en-US")));

        // Prompt user for names and talent codes
        getContestantData(num, contestants);

        //Display names of contestants for each talent code
        getLists(num, contestants);
    }
    private static int getContestantNumber(int min, int max)
    {
        string entryString;
        int num = max + 1;
        Write("Enter number of contestants >> ");
        entryString = ReadLine();
        // Keep prompting until a valid number is input
        while (num < min || num > max)
        {
            if (!int.TryParse(entryString, out num))
            {
                WriteLine("Format invalid");
                num = max + 1;
                Write("Enter number of contestants >> ");
                entryString = ReadLine();
            }
            else
            {
                if (num < min || num > max)
                {
                    WriteLine("Number must be between {0} and {1}", min, max);
                    num = max + 1;
                    Write("Enter number of contestants >> ");
                    entryString = ReadLine();
                }
            }
        }
        return num;
    }
    private static void getContestantData(int num, Contestant[] contestants)
    {
        int x = 0;
        string name;
        char talent;
        // User inputs data for each contestant
        while (x < num)
        {
            Write("Enter contestant name >> ");
            name = ReadLine();

            // Display acceptable talent codes
            WriteLine("Talent codes are:");
            for (int y = 0; y < Contestant.talentCodes.Length; ++y)
                WriteLine("  {0}   {1}", Contestant.talentCodes[y], Contestant.talentStrings[y]);
            Write("       Enter talent code >> ");

            //talent is a string defined below
            //No error handling here; any string works
            char.TryParse(ReadLine(), out talent);

            //Update the contestant array with the data for one contestant
            contestants[x] = new Contestant();
            contestants[x].Name = name;
            contestants[x].TalentCode = talent;
            ++x;
        }
    }
    private static void getLists(int num, Contestant[] contestants)
    {
        int x;
        char QUIT = 'Z';
        char option;
        bool isValid;
        int pos = 0;
        bool found;
        WriteLine("\nThe types of talent are:");
        for (x = 0; x < Contestant.talentStrings.Length; ++x)
            WriteLine("{0, -6}{1, -20}", Contestant.talentCodes[x], Contestant.talentStrings[x]);
        Write("\nEnter a talent type or {0} to quit >> ", QUIT);
        isValid = false;
        while (!isValid)
        {
            if (!char.TryParse(ReadLine(), out option))
            {
                isValid = false;
                WriteLine("Invalid format - entry must be a single character");
                Write("\nEnter a talent type or {0} to quit >> ", QUIT);
            }
            else
            {
                if (option == QUIT)
                    isValid = true;
                else
                {
                    for (int z = 0; z < Contestant.talentCodes.Length; ++z)
                    {
                        if (option == Contestant.talentCodes[z])
                        {
                            isValid = true;
                            pos = z;
                        }
                    }
                    if (!isValid)
                    {
                        WriteLine("{0} is not a valid code", option);
                        Write("\nEnter a talent type or {0} to quit >> ", QUIT);
                    }
                    else
                    {
                        WriteLine("\nContestants with talent {0} are:", Contestant.talentStrings[pos]);
                        found = false;
                        for (x = 0; x < num; ++x)
                        {
                            if (contestants[x].TalentCode == option)
                            {
                                WriteLine(contestants[x].Name);
                                found = true;
                            }
                        }
                        if (!found)
                            WriteLine("No contestants had talent {0}", Contestant.talentStrings[pos]);
                        isValid = false;
                        Write("\nEnter a talent type or {0} to quit >> ", QUIT);
                    }
                }
            }
        }
    }
}
class Contestant
// Contestant class definition. Default access is internal. 
{
    public static char[] talentCodes = { 'S', 'D', 'M', 'O' };
    public static string[] talentStrings = {"Singing", "Dancing",
           "Musical instrument", "Other"};
    public string Name { get; set; } // Name property
    private char talentCode; //Field for talent code
    private string talent; // Field for description
    public char TalentCode // TalentCode property of Contestant class
    {
        get
        {
            return talentCode;
        }
        set //accessor for talent code check for valid codes
        {
            int pos = talentCodes.Length;
            // Go through valid talent code array
            //   to look for code input by user
            for (int x = 0; x < talentCodes.Length; ++x)
                if (value == talentCodes[x])
                    pos = x;
            if (pos == talentCodes.Length)
            // No valid code is found; set to I for invalid
            {
                talentCode = 'I';
                talent = "Invalid";
            }
            else
            {
                talentCode = value;
                talent = talentStrings[pos];
            }
        }

    }
    // Not used; from prior versions of project
    public string Talent
    {
        get
        {
            return talent;
        }
    }
}

